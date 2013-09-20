using Edument.CQRS;
using Events.Team;
using MBACNationals.Team.Commands;
using System;
using System.Collections;

namespace MBACNationals.Team
{
    public class TeamCommandHandlers :
        IHandleCommand<CreateTeam, TeamAggregate>,
        IHandleCommand<AssignTeamMembers, TeamAggregate>
    {
        public IEnumerable Handle(Func<Guid, TeamAggregate> al, CreateTeam command)
        {
            var agg = al(command.Id);

            if (agg.EventsLoaded > 0)
                throw new TeamAlreadyExists();

            yield return new TeamCreated
            {
                Id = command.Id
            };
        }

        public IEnumerable Handle(Func<Guid, TeamAggregate> al, AssignTeamMembers command)
        {
            var agg = al(command.Id);

            yield return new TeamMembersAssigned
            {
                Members = command.Members
            };
        }
    }
}
