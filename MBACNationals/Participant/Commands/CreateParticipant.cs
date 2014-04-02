using MBACNationals.Enums;
using System;

namespace MBACNationals.Participant.Commands
{
    public class CreateParticipant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool IsDelegate { get; set; }
        public int YearsQualifying { get; set; }
    }
}
