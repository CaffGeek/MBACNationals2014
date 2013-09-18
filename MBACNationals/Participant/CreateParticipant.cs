using MBACNationals.Enums;
using System;

namespace MBACNationals.Participant
{
    public class CreateParticipant
    {
        public Guid Id;
        public string Name { get; set; }
        public Gender Gender { get; set; }
    }
}
