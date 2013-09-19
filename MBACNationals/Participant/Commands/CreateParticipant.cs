using MBACNationals.Enums;
using System;

namespace MBACNationals.Participant.Commands
{
    public class CreateParticipant
    {
        public Guid Id;
        public string Name { get; set; }
        public Gender Gender { get; set; }
    }
}
