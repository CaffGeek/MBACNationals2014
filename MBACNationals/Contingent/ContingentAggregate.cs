using Edument.CQRS;
using Events.Contingent;
using MBACNationals.Enums;

namespace MBACNationals.Contingent
{
    public class ContingentAggregate : Aggregate,
        IApplyEvent<ContingentCreated>
    {
        private string province;

        public void Apply(ContingentCreated e)
        {
            province = e.Province;
        }
    }
}
