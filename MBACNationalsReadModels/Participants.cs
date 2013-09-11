using Edument.CQRS;
using Events.Participant;

namespace MBACNationalsReadModels
{
    public class Participants : IParticipantQueries,
        ISubscribeTo<ParticipantCreated>
    {
    }
}
