using Edument.CQRS;
using Events.Contingent;
using Events.Team;
using NDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentViewQueries : AReadModel,
        IContingentViewQueries,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TeamAssignedToContingent>
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

        public void Handle(TeamAssignedToContingent e)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var contingent = GetContingent(e.ContingentId, odb);
                if (contingent == null)
                    return; //Contingent does not exist!

                contingent.Teams.Add(
                    new Team
                    {
                        Id = e.Id,
                        Name = e.Name,
                        ContingentId = e.ContingentId
                    });

                odb.Store(contingent);
            }
        }
    }
}
