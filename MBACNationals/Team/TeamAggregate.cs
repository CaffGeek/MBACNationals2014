using Edument.CQRS;
using Events;
using Events.Team;
using System.Collections.Generic;

namespace MBACNationals.Team
{
    public class TeamAggregate : Aggregate,
        IApplyEvent<TeamCreated>,
        IApplyEvent<TeamMembersAssigned>
    {
        private List<TeamMember> members = new List<TeamMember>();

        public void Apply(TeamCreated e)
        {
        }

        public void Apply(TeamMembersAssigned e)
        {
            members.AddRange(e.Members);
        }
    }
}
