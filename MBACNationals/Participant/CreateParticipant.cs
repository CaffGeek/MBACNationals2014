﻿using MBACNationals.Enums;
using System;

namespace MBACNationals.Participant
{
    public class CreateParticipant
    {
        public Guid Id;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
    }
}
