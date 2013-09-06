using MBACNationals.Enums;
using System;

namespace Events.Participant
{
    public class ParticipantCreated
    {
        public Guid Id;
        public string FirstName;
        public string LastName;
        public Gender Gender;
    }
}
