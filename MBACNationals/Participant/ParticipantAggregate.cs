using Edument.CQRS;
using Events.Participant;
using System;

namespace MBACNationals.Participant
{
    public class ParticipantAggregate : Aggregate,
        IApplyEvent<ParticipantCreated>,
        IApplyEvent<ParticipantRenamed>,
        IApplyEvent<ParticipantAssignedToContingent>,
        IApplyEvent<ParticipantAssignedToTeam>,
        IApplyEvent<ParticipantDesignatedAsAlternate>,
        IApplyEvent<CoachAssignedToTeam>,
        IApplyEvent<ParticipantGenderReassigned>,
        IApplyEvent<ParticipantDelegateStatusGranted>,
        IApplyEvent<ParticipantDelegateStatusRevoked>,
        IApplyEvent<ParticipantYearsQualifyingChanged>,
        IApplyEvent<ParticipantAverageChanged>,
        IApplyEvent<ParticipantAssignedToRoom>,
        IApplyEvent<ParticipantRemovedFromRoom>,
        IApplyEvent<ParticipantGuestPackageChanged>
    {
        public Guid TeamId { get; private set; }
        public Guid ContingentId { get; private set; }
        public string Name { get; private set; }
        public string Gender { get; private set; }
        public bool IsDelegate { get; private set; }
        public bool IsGuest { get; private set; }
        public bool IsCoach { get; private set; }
        public bool IsAlternate { get; private set; }
        public int YearsQualifying { get; private set; }
        public int LeaguePinfall { get; private set; }
        public int LeagueGames { get; private set; }
        public int TournamentPinfall { get; private set; }
        public int TournamentGames { get; private set; }
        public int Average { get; private set; }
        public int RoomNumber { get; private set; }
        public bool IsGuestPackageRequired { get; private set; }
        public PackageInformation Package { get; private set; }

        public class PackageInformation
        {
            public bool ManitobaDinner { get; set; }
            public bool ManitobaDance { get; set; }
            public bool FinalBanquet { get; set; }
            public bool Transportation { get; set; }
        }

        public void Apply(ParticipantCreated e)
        {
            Name = e.Name;
            Gender = e.Gender;
            IsDelegate = e.IsDelegate;
            IsGuest = e.IsGuest;
            YearsQualifying = e.YearsQualifying;
            Package = new PackageInformation();
        }

        public void Apply(ParticipantRenamed e)
        {
            Name = e.Name;
        }

        public void Apply(CoachAssignedToTeam e)
        {
            IsCoach = true;
            TeamId = e.TeamId;
        }

        public void Apply(ParticipantAssignedToContingent e)
        {
            ContingentId = e.ContingentId;
        }

        public void Apply(ParticipantAssignedToTeam e)
        {
            TeamId = e.TeamId;
        }

        public void Apply(ParticipantDesignatedAsAlternate e)
        {
            TeamId = e.TeamId;
            IsAlternate = true; 
        }

        public void Apply(ParticipantGenderReassigned e)
        {
            Gender = e.Gender;
        }

        public void Apply(ParticipantDelegateStatusGranted e)
        {
            IsDelegate = true;
        }

        public void Apply(ParticipantDelegateStatusRevoked e)
        {
            IsDelegate = false;
        }

        public void Apply(ParticipantYearsQualifyingChanged e)
        {
            YearsQualifying = e.YearsQualifying;
        }

        public void Apply(ParticipantAverageChanged e)
        {
            LeaguePinfall = e.LeaguePinfall;
            LeagueGames = e.LeagueGames;
            TournamentPinfall = e.TournamentPinfall;
            TournamentGames = e.TournamentGames;
            var pinfall = LeaguePinfall + TournamentPinfall;
            var games = LeagueGames + TournamentGames;
            
            Average = games > 0 
                ? pinfall / games
                : 0;
        }

        public void Apply(ParticipantAssignedToRoom e)
        {
            RoomNumber = e.RoomNumber;
        }

        public void Apply(ParticipantRemovedFromRoom e)
        {
            RoomNumber = 0;
        }

        public void Apply(ParticipantGuestPackageChanged e)
        {
            Package.ManitobaDinner = e.ManitobaDinner;
            Package.ManitobaDance = e.ManitobaDance;
            Package.FinalBanquet = e.FinalBanquet;
            Package.Transportation = e.Transportation;
        }
    }
}
