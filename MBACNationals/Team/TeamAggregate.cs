using Edument.CQRS;
using Events.Team;
using System;

namespace MBACNationals.Team
{
    public class TeamAggregate : Aggregate,
        IApplyEvent<TeamCreated>,
        IApplyEvent<TeamAssignedToContingent>
    {
        public string Name { get; private set; }
        public Guid ContingentId { get; private set; }

        public void Apply(TeamCreated e)
        {
            Name = e.Name;
        }

        public void Apply(TeamAssignedToContingent e)
        {
            ContingentId = e.ContingentId;
        }
    }
}
