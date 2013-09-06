using System;
using Edument.CQRS;
using System.Collections;
using Events.Participant;

namespace MBACNationals.Participant
{
    public class ParticipantCommandHandlers :
        IHandleCommand<CreateParticipant, ParticipantAggregate>
    {
        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, CreateParticipant command)
        {
            var agg = al(command.Id);

            if (agg.AlreadyHappened)
                throw new ParticipantAlreadyExists();
            
            yield return new ParticipantCreated
            {
                Id = command.Id,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Gender = command.Gender
            };
        }
    }
}
