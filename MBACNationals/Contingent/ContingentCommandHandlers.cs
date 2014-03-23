using Edument.CQRS;
using Events.Contingent;
using MBACNationals.Contingent.Commands;
using System;
using System.Collections;

namespace MBACNationals.Contingent
{
    public class ContingentCommandHandlers :
        IHandleCommand<CreateContingent, ContingentAggregate>
    {
        public IEnumerable Handle(Func<Guid, ContingentAggregate> al, CreateContingent command)
        {
            var agg = al(command.Id);

            if (agg.EventsLoaded > 0)
                throw new ContingentAlreadyExists();

            yield return new ContingentCreated
            {
                Id = command.Id,
                Province = command.Province
            };
        }
    }
}
