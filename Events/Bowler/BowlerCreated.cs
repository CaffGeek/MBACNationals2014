using MBAC.Enums;
using System;

namespace Events.Bowler
{
    public class BowlerCreated
    {
        public Guid Id;
        public string FirstName;
        public string LastName;
        public Gender Gender;
    }
}
