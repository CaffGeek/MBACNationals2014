using Edument.CQRS;
using Events.Team;
using NDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class Teams : ITeamQueries,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<TeamMembersAssigned>
    {
        private string dbFileName = MBACNationalsReadModels.Properties.Settings.Default.ReadModelConnection;

        public class Team
        {
            public Guid Id { get; internal set; }
        }

        public List<Teams.Team> GetTeams()
        {
            using (var odb = OdbFactory.Open(dbFileName))
            {
                var participants = odb.QueryAndExecute<Team>();
                return participants.ToList();
            }
        }

        public Teams.Team GetTeam(Guid id)
        {
            using (var odb = OdbFactory.Open(dbFileName))
            {
                var participants = odb.QueryAndExecute<Team>().Where(p => p.Id.Equals(id));
                return participants.FirstOrDefault();
            }
        }
        
        public void Handle(TeamCreated e)
        {
            if (GetTeam(e.Id) != null)
                return; //Already created

            using (var odb = OdbFactory.Open(dbFileName))
            {
                odb.Store(
                    new Team
                    {
                        Id = e.Id,
                    });
            }
        }

        public void Handle(TeamMembersAssigned e)
        {
            throw new NotImplementedException();
        }
    }
}
