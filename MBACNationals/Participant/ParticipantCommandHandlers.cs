using Edument.CQRS;
using Events.Participant;
using MBACNationals.Participant.Commands;
using System;
using System.Collections;

namespace MBACNationals.Participant
{
    public class ParticipantCommandHandlers :
        IHandleCommand<CreateParticipant, ParticipantAggregate>,
        IHandleCommand<UpdateParticipant, ParticipantAggregate>,
        IHandleCommand<RenameParticipant, ParticipantAggregate>,
        IHandleCommand<AddParticipantToTeam, ParticipantAggregate>,
        IHandleCommand<AddCoachToTeam, ParticipantAggregate>,
        IHandleCommand<AssignParticipantToRoom, ParticipantAggregate>,
        IHandleCommand<RemoveParticipantFromRoom, ParticipantAggregate>
    {
        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, CreateParticipant command)
        {
            var agg = al(command.Id);

            if (agg.EventsLoaded > 0)
                throw new ParticipantAlreadyExists();

            yield return new ParticipantCreated
            {
                Id = command.Id,
                Name = command.Name,
                Gender = command.Gender,
                IsDelegate = command.IsDelegate,
                YearsQualifying = command.YearsQualifying
            };

            yield return new ParticipantAverageChanged
            {
                Id = command.Id,
                LeaguePinfall = command.LeaguePinfall,
                LeagueGames = command.LeagueGames,
                TournamentPinfall = command.TournamentPinfall,
                TournamentGames = command.TournamentGames,
            };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, UpdateParticipant command)
        {
            var agg = al(command.Id);

            if (agg.Name != command.Name)
                yield return new ParticipantRenamed
                {
                    Id = command.Id,
                    Name = command.Name,
                };

            if (agg.Gender != command.Gender)
                yield return new ParticipantGenderReassigned
                {
                    Id = command.Id,
                    Gender = command.Gender,
                };

            if (agg.IsDelegate != command.IsDelegate && command.IsDelegate)
                yield return new ParticipantDelegateStatusGranted
                {
                    Id = command.Id
                };

            if (agg.IsDelegate != command.IsDelegate && !command.IsDelegate)
                yield return new ParticipantDelegateStatusRevoked
                {
                    Id = command.Id
                };

            if (agg.YearsQualifying != command.YearsQualifying)
                yield return new ParticipantYearsQualifyingChanged
                {
                    Id = command.Id,
                    YearsQualifying = command.YearsQualifying,
                };

            if (agg.LeaguePinfall != command.LeaguePinfall
                || agg.LeagueGames != command.LeagueGames
                || agg.TournamentPinfall != command.TournamentPinfall
                || agg.TournamentGames != command.TournamentGames)
                yield return new ParticipantAverageChanged
                {
                    Id = command.Id,
                    LeaguePinfall = command.LeaguePinfall,
                    LeagueGames = command.LeagueGames,
                    TournamentPinfall = command.TournamentPinfall,
                    TournamentGames = command.TournamentGames,
                };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, RenameParticipant command)
        {
            var agg = al(command.Id);

            if (agg.Name != command.Name)
                yield return new ParticipantRenamed
                {
                    Id = command.Id,
                    Name = command.Name,
                };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, AddParticipantToTeam command)
        {
            var agg = al(command.Id);

            if (agg.TeamId != command.TeamId)
                yield return new ParticipantAssignedToTeam
                    {
                        Id = command.Id,
                        TeamId = command.TeamId,
                        Name = agg.Name
                    };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, AddCoachToTeam command)
        {
            var agg = al(command.Id);

            if (agg.TeamId != command.TeamId)
                yield return new CoachAssignedToTeam
                {
                    Id = command.Id,
                    TeamId = command.TeamId,
                    Name = agg.Name
                };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, AssignParticipantToRoom command)
        {
            var agg = al(command.Id);

            yield return new ParticipantAssignedToRoom
            {
                Id = command.Id,
                RoomNumber = command.RoomNumber
            };
        }

        public IEnumerable Handle(Func<Guid, ParticipantAggregate> al, RemoveParticipantFromRoom command)
        {
            var agg = al(command.Id);

            yield return new ParticipantRemovedFromRoom
            {
                Id = command.Id
            };
        }
    }
}
