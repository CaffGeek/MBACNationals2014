using Edument.CQRS;
using Events.Team;
using NDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class Teams : AReadModel,
        ITeamQueries,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<TeamAssignedToContingent>
    {
        public class Team
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public Guid ContingentId { get; internal set; }
        }

        public List<Teams.Team> GetTeams()
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var teams = odb.QueryAndExecute<Team>();
                return teams.ToList();
            }
        }

        public Teams.Team GetTeam(Guid id)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var teams = odb.QueryAndExecute<Team>().Where(p => p.Id.Equals(id));
                return teams.FirstOrDefault();
            }
        }

        private List<Teams.Team> GetTeams(NDatabase.Api.IOdb odb)
        {
            if (odb == null) return GetTeams();

            var teams = odb.QueryAndExecute<Team>();
            return teams.ToList();
        }

        private Teams.Team GetTeam(Guid id, NDatabase.Api.IOdb odb)
        {
            if (odb == null) return GetTeam(id);

            var teams = odb.QueryAndExecute<Team>().Where(p => p.Id.Equals(id));
            return teams.FirstOrDefault();
        }
        
        public void Handle(TeamCreated e)
        {
            if (GetTeam(e.Id) != null)
                return; //Already created

            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                odb.Store(
                    new Team
                    {
                        Id = e.Id,
                        Name = e.Name
                    });
            }
        }

        public void Handle(TeamAssignedToContingent e)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var team = GetTeam(e.Id, odb);

                team.ContingentId = e.ContingentId;

                odb.Store(team);
            }
        }
    }
}
