using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IParticipantProfileQueries
    {
        ParticipantProfileQueries.Participant GetProfile(System.Guid id);
    }
}
