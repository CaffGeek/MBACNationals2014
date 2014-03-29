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
                return GetContingents(odb);
            }
        }

        private List<Contingent> GetContingents(NDatabase.Api.IOdb odb)
        {
            return odb.QueryAndExecute<Contingent>().ToList();
        }

        private Contingent GetContingent(Guid id, NDatabase.Api.IOdb odb)
        {
            return odb.QueryAndExecute<Contingent>().FirstOrDefault(c => c.Id == id);
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
                    });
            }
        }
    }
}
