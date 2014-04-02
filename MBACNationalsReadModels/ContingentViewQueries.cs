using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using NDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentViewQueries : AReadModel,
        IContingentViewQueries,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<ParticipantAssignedToTeam>
    {
        public class Contingent
        {
            public Guid Id { get; internal set; }
            public string Province { get; internal set; }
            public IList<Team> Teams { get; internal set; }
        }

        public class Team
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public Guid ContingentId { get; internal set; }
            public IList<Participant> Bowlers { get; internal set; }
            public int SizeLimit { get; internal set; }
        }

        public class Participant
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public Guid TeamId { get; internal set; }
        }

        public Contingent GetContingent(Guid id)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                return GetContingent(id, odb);
            }
        }

        public Contingent GetContingent(string province)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                return GetContingent(province, odb);
            }
        }

        private Contingent GetContingent(Guid id, NDatabase.Api.IOdb odb)
        {
            if (odb == null) return GetContingent(id);

            var contingents = odb.QueryAndExecute<Contingent>().Where(p => p.Id.Equals(id));
            return contingents.FirstOrDefault();
        }

        private Contingent GetContingent(string province, NDatabase.Api.IOdb odb)
        {
            if (odb == null) return GetContingent(province);

            var contingents = odb.QueryAndExecute<Contingent>().Where(p => p.Province.Equals(province));
            return contingents.FirstOrDefault();
        }

        public void Handle(ContingentCreated e)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                if (GetContingent(e.Id, odb) != null)
                    return; //Already created

                odb.Store(
                    new Contingent
                    {
                        Id = e.Id,
                        Province = e.Province,
                        Teams = new List<Team>()
                    });
            }
        }

        public void Handle(TeamCreated e)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var contingent = GetContingent(e.Id, odb);
                if (contingent == null)
                    return; //Contingent does not exist!

                contingent.Teams.Add(
                    new Team
                    {
                        Id = e.TeamId,
                        Name = e.Name,
                        ContingentId = e.Id,
                        SizeLimit = e.SizeLimit,
                        Bowlers = new List<Participant>()
                    });

                odb.Store(contingent);
            }
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var contingent = odb.QueryAndExecute<Contingent>().FirstOrDefault(c => c.Teams.Any(t => t.Id.Equals(e.TeamId)));
                if (contingent == null)
                    return;

                var team = contingent.Teams.FirstOrDefault(t => t.Id.Equals(e.TeamId));
                if (team == null)
                    return;

                team.Bowlers.Add(new Participant { 
                    Id = e.Id,
                    TeamId = e.TeamId,
                    Name = e.Name,
                });

                odb.Store(contingent);
            }
        }
    }
}
