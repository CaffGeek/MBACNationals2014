using Edument.CQRS;
using Events.Team;
using MBACNationals.Team.Commands;
using System;
using System.Collections;

namespace MBACNationals.Team
{
    public class TeamCommandHandlers :
        IHandleCommand<CreateTeam, TeamAggregate>,
        IHandleCommand<AddTeamToContingent, TeamAggregate>
    {
        public IEnumerable Handle(Func<Guid, TeamAggregate> al, CreateTeam command)
        {
            var teamAggregate = al(command.Id);

            if (teamAggregate.EventsLoaded > 0)
                throw new TeamAlreadyExists();

            yield return new TeamCreated
            {
                Id = command.Id,
                Name = command.Name,
            };
        }

        public IEnumerable Handle(Func<Guid, TeamAggregate> al, AddTeamToContingent command)
        {
            var teamAggregate = al(command.Id);
            
            yield return new TeamAssignedToContingent
            {
                Id = command.Id,
                ContingentId = command.ContingentId,
                Name = teamAggregate.Name,
            };
        }
    }
}