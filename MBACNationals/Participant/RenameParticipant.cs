using MBACNationals.Enums;
using System;

namespace MBACNationals.Participant
{
    public class RenameParticipant
    {
        public Guid Id;
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
