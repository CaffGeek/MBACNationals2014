using MBACNationals.Enums;
using System;

namespace Events.Participant
{
    public class ParticipantCreated
    {
        public Guid Id;
        public string Name;
        public Gender Gender;
        public bool IsDelegate;
        public int YearsQualifying;
    }
}
