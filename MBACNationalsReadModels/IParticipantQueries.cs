using Events.Participant;
using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IParticipantQueries
    {
        List<Participants.Participant> GetParticipants();
    }
}
