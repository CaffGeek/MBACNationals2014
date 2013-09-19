using System;
using Edument.CQRS;
using System.Collections;
using Events.Participant;
using MBACNationals.Participant.Commands;

namespace MBACNationals.Participant
{
    public class ParticipantCommandHandlers :
        IHandleCommand<CreateParticipant, ParticipantAggregate>,
        IHandleCommand<RenameParticipant, ParticipantAggregate>
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
    }
}
