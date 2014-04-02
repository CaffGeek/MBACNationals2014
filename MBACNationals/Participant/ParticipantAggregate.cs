using Edument.CQRS;
using Events.Participant;
using MBACNationals.Enums;
using System;

namespace MBACNationals.Participant
{
    public class ParticipantAggregate : Aggregate,
        IApplyEvent<ParticipantCreated>,
        IApplyEvent<ParticipantRenamed>,
        IApplyEvent<ParticipantAssignedToTeam>
    {
        public Guid TeamId { get; private set; }
        public string Name { get; private set; }
        public Gender Gender { get; private set; }

        public void Apply(ParticipantCreated e)
        {
            Name = e.Name;
            Gender = e.Gender;
        }

        public void Apply(ParticipantRenamed e)
        {
            Name = e.Name;
        }

        public void Apply(ParticipantAssignedToTeam e)
        {
            TeamId = e.TeamId;
        }
    }
}
