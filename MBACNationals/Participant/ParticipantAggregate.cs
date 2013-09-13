using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edument.CQRS;
using Events.Participant;
using MBACNationals.Enums;

namespace MBACNationals.Participant
{
    public class ParticipantAggregate : Aggregate,
        IApplyEvent<ParticipantCreated>,
        IApplyEvent<ParticipantRenamed>
    {
        private string firstName;
        private string lastName;
        private Gender gender;

        public void Apply(ParticipantCreated e)
        {
            firstName = e.FirstName;
            lastName = e.LastName;
            gender = e.Gender;
        }

        public void Apply(ParticipantRenamed e)
        {
            firstName = e.FirstName;
            lastName = e.LastName;
        }
    }
}
