using Edument.CQRS;
using Events.Contingent;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class MatchQueries : AReadModel,
        IMatchQueries,
        ISubscribeTo<MatchCreated>,
        ISubscribeTo<ParticipantGameCompleted>,
        ISubscribeTo<TeamGameCompleted>,
        ISubscribeTo<MatchCompleted>
    {
        public MatchQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }
        
        public class Match : AEntity
        {
            public Match(Guid id) : base(id) { }
            public string Division { get; internal set; }
            public bool IsPOA { get; internal set; }
            public int Number { get; internal set; }
            public Team Away { get; internal set; }
            public Team Home { get; internal set; }
            public int Lane { get; internal set; }
            public string Centre { get; internal set; }
            public bool IsComplete { get; internal set; }
        }

        public class Team
        {
            public string Id { get; internal set; }
            public string Province { get; internal set; }
            public List<Bowler> Bowlers { get; internal set; }
            public int Score { get; internal set; }
            public int POA { get; internal set; }
            public decimal Points { get; internal set; }
            public decimal TotalPoints { get; internal set; }
        }

        public class Bowler
        {
            public string Id { get; internal set; }
            public string Name { get; internal set; }
            public int Position { get; internal set; }
            public int Score { get; internal set; }
            public int POA { get; internal set; }
            public decimal Points { get; internal set; }
        }
        
        public Match GetMatch(Guid matchId)
        {
            return Read<Match>(x => x.Id == matchId).FirstOrDefault();
        }
        
        public void Handle(MatchCreated e)
        {
            var match = Read<Match>(x => x.Id == e.Id).FirstOrDefault();
            if (match != null)
                return;

            Create(new Match(e.Id)
            {
                Division = e.Division,
                IsPOA = e.IsPOA,
                Number = e.Number,
                Away = new Team { Province = e.Away, Bowlers = new List<Bowler>() },
                Home = new Team { Province = e.Home, Bowlers = new List<Bowler>() },
                Lane = e.Lane,
                Centre = e.CentreName,
                IsComplete = false,
            });
        }

        public void Handle(ParticipantGameCompleted e)
        {
            Update<Match>(e.Id, x =>
            {
                var team = x.Away.Province == e.Contingent ? x.Away : x.Home;
                team.Bowlers.Add(new Bowler
                {
                    Id = e.ParticipantId.ToString(),
                    Name = e.Name,
                    Position = e.Position,
                    Score = e.Score,
                    POA = e.POA,
                    Points = e.Points
                });
            });
        }

        public void Handle(TeamGameCompleted e)
        {
            //var match = Read<Match>(x => x.Id == e.Id).FirstOrDefault();
            //if (match == null)
            //    return;

            //var teamId = (match.Away.Province == e.Contingent)
            //    ? match.Away.Id
            //    : match.Home.Id;

            //Update<Match.Team>(e.Id, x =>
            //{
            //    x.TeamId = e.TeamId;
            //    x.Score = e.Score;
            //    x.POA = e.POA;
            //    x.Points = e.Points;
            //    x.TotalPoints = e.TotalPoints;
            //});
        }

        public void Handle(MatchCompleted e)
        {
            Update<Match>(e.Id, x => x.IsComplete = true);
        }
    }
}
