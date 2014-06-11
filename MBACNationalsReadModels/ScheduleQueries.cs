using Edument.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ScheduleQueries : AReadModel,
        IScheduleQueries
    {
        public ScheduleQueries(string readModelFilePath)
            : base(readModelFilePath)
        {
            Create(ScheduleBuilder.Singles("Tournament Men Single"));
            Create(ScheduleBuilder.Singles("Tournament Ladies Single"));
            Create(ScheduleBuilder.TournamentLadies());
            Create(ScheduleBuilder.TournamentMen());
            Create(ScheduleBuilder.TeachingLadies());
            Create(ScheduleBuilder.TeachingMen());
            Create(ScheduleBuilder.Seniors());
        }

        public enum BowlingCentre
        {
            Academy,
            Rossmere,
            Coronation,
        }

        public class Schedule : AEntity
        {
            public Schedule(Guid id) : base(id) { }
            public string Division { get; internal set; }
            public List<Game> Games { get; internal set; }
        }

        public class Game : AEntity
        {
            public int Number { get; private set; }
            public string Away { get; private set; }
            public string Home { get; private set; }
            public int Lane { get; private set; }
            public BowlingCentre Centre { get; private set; }
            public string CentreName { get; private set; }

            public Game(int number, string home, string away, int lane, BowlingCentre centre)
                : base(Guid.NewGuid())
            {
                Number = number;
                Home = home;
                Away = away;
                Lane = lane;
                Centre = centre;
                CentreName = centre.ToString();
            }
        }

        public ScheduleQueries.Schedule GetSchedule(string division)
        {
            return Read<Schedule>(x => x.Division.Equals(division, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
    }
}
