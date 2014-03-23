using Edument.CQRS;
using Events.Contingent;
using NDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentQueries : AReadModel,
        IContingentQueries,
        ISubscribeTo<ContingentCreated>
    {
        public class Contingent
        {
            public Guid Id { get; internal set; }
            public string Province { get; internal set; }
        }

        public List<Contingent> GetContingents()
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var contingents = odb.QueryAndExecute<Contingent>();
                return contingents.ToList();
            }
        }

        private Contingent GetContingent(Guid id)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var contingents = odb.QueryAndExecute<Contingent>().Where(p => p.Id.Equals(id));
                return contingents.FirstOrDefault();
            }
        }

        public void Handle(ContingentCreated e)
        {
            if (GetContingent(e.Id) != null)
                return; //Already created

            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                odb.Store(
                    new Contingent
                    {
                        Id = e.Id,
                        Province = e.Province,
                    });
            }
        }
    }
}
