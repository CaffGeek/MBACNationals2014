using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edument.CQRS;
using Events.Participant;

namespace MBACNationals.Participant
{
    public class ParticipantAggregate : Aggregate,
        IApplyEvent<ParticipantCreated>
    {
        public bool AlreadyHappened { get; private set; }

        public void Apply(ParticipantCreated e)
        {
            AlreadyHappened = true;
        }
    }
}
