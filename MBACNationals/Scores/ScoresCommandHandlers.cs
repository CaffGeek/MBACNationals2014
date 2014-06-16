using Edument.CQRS;
using Events.Scores;
using MBACNationals.Participant;
using MBACNationals.Scores.Commands;
using System;
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

            var awayTeamScore = 0;
            var homeTeamScore = 0;
            var awayTeamPOA = 0;
            var homeTeamPOA = 0;
            var awayTeamPoints = 0m;
            var homeTeamPoints = 0m;
            
            for (var i = 0; i < command.Away.Bowlers.Length; i++)
            {
                var awayBowler = command.Away.Bowlers[i];
                var awayParticipant = _dispatcher.Load<ParticipantAggregate>(awayBowler.Id);
                
                var homeBowler = command.Home.Bowlers[i];
                var homeParticipant = _dispatcher.Load<ParticipantAggregate>(homeBowler.Id);

                var awayPOA = awayBowler.Score - awayParticipant.Average;
                var homePOA = homeBowler.Score - homeParticipant.Average;

                awayTeamScore += awayBowler.Score;
                homeTeamScore += homeBowler.Score;

                awayTeamPOA += awayPOA;
                homeTeamPOA += homePOA;

                var awayBowlerPoint = agg.IsPOA
                        ? (awayPOA > homePOA ? 1m 
                            : awayPOA == homePOA ? .5m
                            : 0m)
                        : (awayBowler.Score > homeBowler.Score ? 1m
                            : awayBowler.Score == homeBowler.Score ? .5m
                            :0m);

                var homeBowlerPoint = agg.IsPOA
                        ? (homePOA > awayPOA ? 1m 
                            : homePOA == awayPOA ? .5m
                            : 0m)
                        : (homeBowler.Score > awayBowler.Score ? 1m
                            : homeBowler.Score == awayBowler.Score ? .5m
                            : 0m);

                awayTeamPoints += awayBowlerPoint;
                homeTeamPoints += homeBowlerPoint;

                //Away
                yield return new ParticipantGameCompleted
                {
                    Id = command.Id,
                    ParticipantId = awayBowler.Id,
                    Name = awayParticipant.Name,
                    Gender = awayParticipant.Gender,
                    Division = agg.Division,
                    Score = awayBowler.Score,
                    Position = awayBowler.Position,
                    POA = awayPOA,
                    Points = awayBowlerPoint
                };

                //Home
                yield return new ParticipantGameCompleted
                {
                    Id = command.Id,
                    ParticipantId = homeBowler.Id,
                    Name = homeParticipant.Name,
                    Gender = homeParticipant.Gender,
                    Division = agg.Division,
                    Score = homeBowler.Score,
                    Position = homeBowler.Position,
                    POA = homePOA,
                    Points = homeBowlerPoint
                };
            }

            var awayTeamPoint = agg.IsPOA
                        ? (awayTeamPOA > homeTeamPOA ? 3m 
                            : awayTeamPOA == homeTeamPOA ? 1.5m
                            : 0m)
                        : (awayTeamScore > homeTeamScore ? 1m
                            : awayTeamScore == homeTeamScore ? .5m
                            : 0m);
            var homeTeamPoint = agg.IsPOA
                        ? (homeTeamPOA > awayTeamPOA ? 3m 
                            : homeTeamPOA == awayTeamPOA ? 1.5m
                            : 0m)
                        : (homeTeamScore > awayTeamScore ? 1m
                            : homeTeamScore == awayTeamScore ? .5m
                            : 0m);

            //Away
            yield return new TeamGameCompleted
            {
                Id = command.Id,
                TeamId = command.Away.Id,
                Score = awayTeamScore,
                POA = awayTeamPOA,
                Points = awayTeamPoint,
                TotalPoints = awayTeamPoints + awayTeamPoint
            };

            //Home
            yield return new TeamGameCompleted
            {
                Id = command.Id,
                TeamId = command.Home.Id,
                Score = homeTeamScore,
                POA = homeTeamPOA,
                Points = homeTeamPoint,
                TotalPoints = homeTeamPoints + homeTeamPoint
            };

            yield return new MatchCompleted
            {
                Id = command.Id,
                //TODO:
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
    }
}
