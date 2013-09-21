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
        ISubscribeTo<TeamCreated>
    {
        public class Team
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
        }

        public List<Teams.Team> GetTeams()
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var participants = odb.QueryAndExecute<Team>();
                return participants.ToList();
            }
        }

        public Teams.Team GetTeam(Guid id)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var participants = odb.QueryAndExecute<Team>().Where(p => p.Id.Equals(id));
                return participants.FirstOrDefault();
            }
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
                        Name = e.Id.ToString(),
                    });
            }
        }
    }
}
