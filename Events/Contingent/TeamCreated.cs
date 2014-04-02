using System;

namespace Events.Contingent
{
    public class TeamCreated
    {
        public Guid Id;
        public Guid TeamId;
        public string Name;
        public string Gender;
        public int SizeLimit;
    }
}
