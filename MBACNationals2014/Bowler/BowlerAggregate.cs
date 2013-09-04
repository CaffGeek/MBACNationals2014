using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edument.CQRS;
using Events.Bowler;

namespace MBAC.Bowler
{
    public class BowlerAggregate : Aggregate,
        IApplyEvent<BowlerCreated>
    {
        public bool AlreadyHappened { get; private set; }

        public void Apply(BowlerCreated e)
        {
            AlreadyHappened = true;
        }
    }
}
