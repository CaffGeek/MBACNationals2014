using Edument.CQRS;
using Events.Contingent;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MBACNationals.Contingent
{
    public class ContingentAggregate : Aggregate,
        IApplyEvent<ContingentCreated>,
        IApplyEvent<TeamCreated>,
        IApplyEvent<TeamRemoved>
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
            var team = new Team(e, Id.Value);
            Teams.Add(team);
        }

        public void Apply(TeamRemoved e)
        {
            Teams.RemoveAll(x => x.Id.Equals(e.TeamId));
        }
    }

    public class Team
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid ContingentId { get; private set; }
        public string Gender { get; private set; }
        public int SizeLimit { get; private set; }
        public bool RequiresShirtSize { get; private set; }
        public bool RequiresCoach { get; private set; }
        public bool RequiresAverage { get; private set; }
        public bool RequiresBio { get; private set; }
        public bool RequiresGender { get; private set; }
        public bool IncludesSinglesRep { get; private set; }

        public Team(TeamCreated e, Guid contingentId)
        {
            ContingentId = contingentId;
            Apply(e);
        }

        public void Apply(TeamCreated e)
        {
            Id = e.TeamId;
            Name = e.Name;
            Gender = e.Gender;
            SizeLimit = e.SizeLimit;
            RequiresShirtSize = e.RequiresShirtSize;
            RequiresCoach = e.RequiresCoach;
            RequiresAverage = e.RequiresAverage;
            RequiresBio = e.RequiresBio;
            RequiresGender = e.RequiresGender;
            IncludesSinglesRep = e.IncludesSinglesRep;
        }
    }
}
