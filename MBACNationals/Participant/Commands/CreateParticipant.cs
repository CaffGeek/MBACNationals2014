using System;

namespace MBACNationals.Participant.Commands
{
    public class CreateParticipant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public bool IsDelegate { get; set; }
        public int YearsQualifying { get; set; }
        public int LeagueGames { get; set; }
        public int LeaguePinfall { get; set; }
        public int TournamentGames { get; set; }
        public int TournamentPinfall { get; set; }
        public bool IsGuest { get; set; }
    }
}
