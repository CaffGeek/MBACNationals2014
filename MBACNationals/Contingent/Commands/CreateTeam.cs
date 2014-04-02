using System;

namespace MBACNationals.Contingent.Commands
{
    public class CreateTeam
    {
        public Guid ContingentId { get; set; }
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int SizeLimit { get; set; }
    }
}
