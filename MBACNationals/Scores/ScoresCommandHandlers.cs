using Edument.CQRS;
using Events.Scores;
using MBACNationals.Participant;
using MBACNationals.Scores.Commands;
using System;
using System.Linq;
using System.Collections;

namespace MBACNationals.Scores
{
    public class ScoresCommandHandlers :
        IHandleCommand<SaveMatchResult, MatchAggregate>,
        IHandleCommand<SaveMatch, MatchAggregate>
    {
        private MessageDispatcher _dispatcher;

        public ScoresCommandHandlers(MessageDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public IEnumerable Handle(Func<Guid, MatchAggregate> al, SaveMatchResult command)
        {
            var agg = al(command.Id);
            
            var match = _dispatcher.Load<MatchAggregate>(command.Id);

            if (match.IsPOA)
            {
                var awayBowler = command.Away.Bowlers.First();
                var homeBowler = command.Home.Bowlers.First();

                var awayParticipant = _dispatcher.Load<ParticipantAggregate>(awayBowler.Id);
                var homeParticipant = _dispatcher.Load<ParticipantAggregate>(homeBowler.Id);

                var awayPOA = awayBowler.Score - awayParticipant.Average;
                var homePOA = homeBowler.Score - homeParticipant.Average;

                //Away
                var awaySinglePoints = CalculatePoint(awayPOA, homePOA, 2);
                yield return new TeamGameCompleted
                {
                    Id = command.Id,
                    Number = match.Number,
                    Division = agg.Division + " Single",
                    Contingent = match.Away,
                    Opponent = match.Home,
                    TeamId = command.Away.Id,
                    Score = awayBowler.Score,
                    POA = awayPOA,
                    Points = awaySinglePoints,
                    TotalPoints = awaySinglePoints,
                    Lane = match.Lane + 1,
                    Centre = match.CentreName
                };

                //Home
                var homeSinglePoints = CalculatePoint(homePOA, awayPOA, 2);
                yield return new TeamGameCompleted
                {
                    Id = command.Id,
                    Number = match.Number,
                    Division = agg.Division + " Single",
                    Contingent = match.Home,
                    Opponent = match.Away,
                    TeamId = command.Home.Id,
                    Score = homeBowler.Score,
                    POA = homePOA,
                    Points = homeSinglePoints,
                    TotalPoints = homeSinglePoints,
                    Lane = match.Lane + 1,
                    Centre = match.CentreName
                };
            }

            var awayTeamScore = 0;
            var homeTeamScore = 0;
            var awayTeamPOA = 0;
            var homeTeamPOA = 0;
            var awayTeamPoints = 0m;
            var homeTeamPoints = 0m;

            for (var i = 1; i <= command.Away.Bowlers.Length; i++)
            {
                var awayBowler = command.Away.Bowlers.Single(x=>x.Position == i);
                var awayParticipant = _dispatcher.Load<ParticipantAggregate>(awayBowler.Id);

                var homeBowler = command.Home.Bowlers.Single(x => x.Position == i);
                var homeParticipant = _dispatcher.Load<ParticipantAggregate>(homeBowler.Id);

                var awayPOA = awayBowler.Score - awayParticipant.Average;
                var homePOA = homeBowler.Score - homeParticipant.Average;

                awayTeamScore += awayBowler.Score;
                homeTeamScore += homeBowler.Score;

                awayTeamPOA += awayPOA;
                homeTeamPOA += homePOA;

                var awayBowlerPoint = agg.IsPOA
                    ? CalculatePoint(awayPOA, homePOA, 3)
                    : CalculatePoint(awayBowler.Score, homeBowler.Score, 3);

                var homeBowlerPoint = agg.IsPOA
                    ? CalculatePoint(homePOA, awayPOA, 3)
                    : CalculatePoint(homeBowler.Score, awayBowler.Score, 3);
                
                awayTeamPoints += awayBowlerPoint;
                homeTeamPoints += homeBowlerPoint;

                //Away
                yield return new ParticipantGameCompleted
                {
                    Id = command.Id,
                    ParticipantId = awayBowler.Id,
                    Number = match.Number,
                    Name = awayParticipant.Name,
                    Gender = awayParticipant.Gender,
                    Division = agg.Division,
                    Contingent = match.Away,
                    Opponent = match.Home,
                    OpponentName = homeParticipant.Name,
                    Score = awayBowler.Score,
                    Position = awayBowler.Position,
                    POA = awayPOA,
                    Points = awayBowlerPoint,
                    Lane = match.Lane,
                    Centre = match.CentreName
                };

                //Home
                yield return new ParticipantGameCompleted
                {
                    Id = command.Id,
                    ParticipantId = homeBowler.Id,
                    Number = match.Number,
                    Name = homeParticipant.Name,
                    Gender = homeParticipant.Gender,
                    Division = agg.Division,
                    Contingent = match.Home,
                    Opponent = match.Away,
                    OpponentName = awayParticipant.Name,
                    Score = homeBowler.Score,
                    Position = homeBowler.Position,
                    POA = homePOA,
                    Points = homeBowlerPoint,
                    Lane = match.Lane + 1,
                    Centre = match.CentreName
                };
            }

            var awayTeamPoint = agg.IsPOA
                ? CalculatePoint(awayTeamPOA, homeTeamPOA, 3)
                : CalculatePoint(awayTeamScore, homeTeamScore, 3);

            var homeTeamPoint = agg.IsPOA
                ? CalculatePoint(homeTeamPOA, awayTeamPOA, 3)
                : CalculatePoint(homeTeamScore, awayTeamScore, 3);

            //Away
            yield return new TeamGameCompleted
            {
                Id = command.Id,
                Number = match.Number,
                Division = agg.Division,
                Contingent = match.Away,
                Opponent = match.Home,
                TeamId = command.Away.Id,
                Score = awayTeamScore,
                POA = awayTeamPOA,
                Points = awayTeamPoint,
                TotalPoints = awayTeamPoints + awayTeamPoint,
                Lane = match.Lane + 1,
                Centre = match.CentreName
            };

            //Home
            yield return new TeamGameCompleted
            {
                Id = command.Id,
                Number = match.Number,
                Division = agg.Division,
                Contingent = match.Home,
                Opponent = match.Away,
                TeamId = command.Home.Id,
                Score = homeTeamScore,
                POA = homeTeamPOA,
                Points = homeTeamPoint,
                TotalPoints = homeTeamPoints + homeTeamPoint,
                Lane = match.Lane + 1,
                Centre = match.CentreName
            };

            yield return new MatchCompleted
            {
                Id = command.Id,
                Number = match.Number,
                Division = agg.Division,
                Home = match.Home,
                Away = match.Away
            };
        }

        public IEnumerable Handle(Func<Guid, MatchAggregate> al, SaveMatch command)
        {
            var agg = al(command.Id);

            if (agg.EventsLoaded > 0)
                throw new MatchAlreadyCreated();

            yield return new MatchCreated
            {
                Id = command.Id,
                Division = command.Division,
                IsPOA = command.IsPOA,
                Home = command.Home,
                Away = command.Away,
                Centre = command.Centre,
                CentreName = command.CentreName,
                Lane = command.Lane,
                Number = command.Number
            };            
        }

        public decimal CalculatePoint(int score, int opponentScore, decimal maxPoint)
        {
            return score > opponentScore ? maxPoint
                        : score == opponentScore ? maxPoint / 2m
                        : 0m;
        }
    }
}
