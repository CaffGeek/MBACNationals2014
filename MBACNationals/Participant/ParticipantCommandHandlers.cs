using Edument.CQRS;
using Events.Participant;
using MBACNationals.Participant.Commands;
using System;
using System.Collections;

namespace MBACNationals.Participant
{
    public class ParticipantCommandHandlers :
        IHandleCommand<CreateParticipant, ParticipantAggregate>,
        IHandleCommand<RenameParticipant, ParticipantAggregate>,
        IHandleCommand<AddParticipantToTeam, ParticipantAggregate>
    {
        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, CreateParticipant command)
        {
            var agg = al(command.Id);

            if (agg.EventsLoaded > 0)
                throw new ParticipantAlreadyExists();

            yield return new ParticipantCreated
            {
                Id = command.Id,
                Name = command.Name,
                Gender = command.Gender
            };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, RenameParticipant command)
        {
            var agg = al(command.Id);

            yield return new ParticipantRenamed
            {
                Id = command.Id,
                Name = command.Name,
            };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, AddParticipantToTeam command)
        {
            var agg = al(command.Id);

            yield return new ParticipantAssignedToTeam
                {
                    Id = command.Id,
                    TeamId = command.TeamId
                };
        }
    }
}
