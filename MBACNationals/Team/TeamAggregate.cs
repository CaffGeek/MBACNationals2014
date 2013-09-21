using Edument.CQRS;
using Events.Team;

namespace MBACNationals.Team
{
    public class TeamAggregate : Aggregate,
        IApplyEvent<TeamCreated>
    {
        public void Apply(TeamCreated e)
        {
        }
    }
}
