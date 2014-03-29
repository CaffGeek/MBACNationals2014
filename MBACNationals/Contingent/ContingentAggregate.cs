using Edument.CQRS;
using Events.Contingent;
using MBACNationals.Enums;
using System;
using System.Collections.Generic;

namespace MBACNationals.Contingent
{
    public class ContingentAggregate : Aggregate,
        IApplyEvent<ContingentCreated>,
        IApplyEvent<TeamCreated>
    {
        public string Province { get; private set; }
        public List<Team> Teams { get; private set; }

        public ContingentAggregate()
        {
            Teams = new List<Team>();
        }

        public void Apply(ContingentCreated e)
        {
            Province = e.Province;
        }

        public void Apply(TeamCreated e)
        {
            Teams.Add(new Team(e.Id, Id.Value, e.Name));
        }
    }

    public class Team
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid ContingentId { get; private set; }

        public Team(Guid id, Guid contingentId, string name)
        {
            Id = id;
            Name = name;
            ContingentId = contingentId;
        }
    }
}
