using Edument.CQRS;
using Events.Participant;
using MBACNationals.Enums;

namespace MBACNationals.Participant
{
    public class ParticipantAggregate : Aggregate,
        IApplyEvent<ParticipantCreated>,
        IApplyEvent<ParticipantRenamed>
    {
        private string name;
        private Gender gender;

        public void Apply(ParticipantCreated e)
        {
            name = e.Name;
            gender = e.Gender;
        }

        public void Apply(ParticipantRenamed e)
        {
            name = e.Name;
        }
    }
}
