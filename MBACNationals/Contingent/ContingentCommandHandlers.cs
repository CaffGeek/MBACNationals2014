﻿using Edument.CQRS;
using Events.Contingent;
using MBACNationals.Contingent.Commands;
using System;
using System.Collections;
using System.Linq;

namespace MBACNationals.Contingent
{
    public class ContingentCommandHandlers :
        IHandleCommand<CreateContingent, ContingentAggregate>,
        IHandleCommand<CreateTeam, ContingentAggregate>
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

        public IEnumerable Handle(Func<Guid, ContingentAggregate> al, CreateTeam command)
        {
            var contingentAggregate = al(command.ContingentId);

            if (!contingentAggregate.Teams.Any(t => t.Name.Equals(command.Name)))
                yield return new TeamCreated
                {
                    Id = command.ContingentId,
                    TeamId = command.TeamId,
                    Name = command.Name,
                    Gender = command.Gender,
                    SizeLimit = command.SizeLimit
                };
        }
    }
}
