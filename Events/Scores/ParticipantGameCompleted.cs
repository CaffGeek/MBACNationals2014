﻿ namespace Events.Scores
{
    public class ParticipantGameCompleted
    {
        public System.Guid Id;
        public System.Guid ParticipantId;
        public string Division;
        public string Name;
        public string Gender;
        public int Score;
        public int Position;
        public int POA;
        public decimal Points;
    }
}
