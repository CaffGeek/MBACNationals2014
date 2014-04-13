using Edument.CQRS;
using Events.Contingent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentQueries : AReadModel,
        IContingentQueries,
        ISubscribeTo<ContingentCreated>
    {
        public ContingentQueries(string readModelFilePath)
            : base(readModelFilePath)
        {

        }

        public class Contingent : IEntity
        {
            public Guid Id { get; internal set; }
            public string Province { get; internal set; }
        }

        public List<Contingent> GetContingents()
        {
            return Read<Contingent>().ToList();
        }
        
        public void Handle(ContingentCreated e)
        {
            Create(new Contingent
            {
                Id = e.Id,
                Province = e.Province,
            });
        }
    }
}
