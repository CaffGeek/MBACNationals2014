using Edument.CQRS;
using Events.Participant;
using NDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ParticipantQueries : AReadModel,
        IParticipantQueries,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantAssignedToTeam>,
        ISubscribeTo<CoachAssignedToTeam>,
        ISubscribeTo<ParticipantGenderReassigned>,
        ISubscribeTo<ParticipantDelegateStatusGranted>,
        ISubscribeTo<ParticipantDelegateStatusRevoked>,
        ISubscribeTo<ParticipantYearsQualifyingChanged>,
        ISubscribeTo<ParticipantAverageChanged>
    {
        public class Participant : IEntity
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public string Gender { get; internal set; }
            public Guid TeamId { get; internal set; }
            public string TeamName { get; internal set; }
            public bool IsDelegate { get; internal set; }
            public bool IsCoach { get; internal set; }
            public int YearsQualifying { get; internal set; }
            public int LeaguePinfall { get; internal set; }
            public int LeagueGames { get; internal set; }
            public int TournamentPinfall { get; internal set; }
            public int TournamentGames { get; internal set; }
            public int Average { get; internal set; }
        }

        public List<Participant> GetParticipants()
        {
            return Read<Participant>().ToList();
        }

        public Participant GetParticipant(Guid id)
        {
            return Read<Participant>(x => x.Id.Equals(id)).FirstOrDefault();
        }


        public void Handle(ParticipantCreated e)
        {
            Create(new Participant
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Gender = e.Gender
                        });
        }

        public void Handle(ParticipantRenamed e)
        {
            Update<Participant>(e.Id, x => { x.Name = e.Name; });
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            Update<Participant>(e.Id, x => { x.TeamId = e.TeamId; });
        }

        public void Handle(CoachAssignedToTeam e)
        {
            Update<Participant>(e.Id, x => { 
                x.TeamId = e.TeamId; 
                x.IsCoach = true; 
            });
        }

        public void Handle(ParticipantGenderReassigned e)
        {
            Update<Participant>(e.Id, x => { x.Gender = e.Gender; });
        }

        public void Handle(ParticipantDelegateStatusGranted e)
        {
            Update<Participant>(e.Id, x => { x.IsDelegate = true; });
        }

        public void Handle(ParticipantDelegateStatusRevoked e)
        {
            Update<Participant>(e.Id, x => { x.IsDelegate = false; });
        }

        public void Handle(ParticipantYearsQualifyingChanged e)
        {
            Update<Participant>(e.Id, x => { x.YearsQualifying = e.YearsQualifying; });
        }

        public void Handle(ParticipantAverageChanged e)
        {
            Update<Participant>(e.Id, x => {
                x.LeagueGames = e.LeagueGames;
                x.LeaguePinfall = e.LeaguePinfall;
                x.TournamentGames = e.TournamentGames;
                x.TournamentPinfall = e.TournamentPinfall;
                x.Average = e.Average;
            });
        }
    }
}
