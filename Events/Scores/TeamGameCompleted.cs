namespace Events.Scores
{
    public class TeamGameCompleted
    {
        public System.Guid Id;
        public System.Guid TeamId;
        public int Score;
        public int POA;
        public decimal Points;
        public decimal TotalPoints;
    }
}