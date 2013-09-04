using System;
using Edument.CQRS;
using System.Collections;
using Events.Bowler;

namespace MBAC.Bowler
{
    public class BowlerCommandHandlers :
        IHandleCommand<CreateBowler, BowlerAggregate>
    {
        public IEnumerable Handle(Func<Guid, BowlerAggregate> al, CreateBowler command)
        {
            var agg = al(command.Id);

            if (agg.AlreadyHappened)
                throw new BowlerAlreadyExists();
            
            yield return new BowlerCreated
            {
                Id = command.Id,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Gender = command.Gender
            };
        }
    }
}
