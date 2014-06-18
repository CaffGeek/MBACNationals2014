using Edument.CQRS;
using Events.Contingent;
using Events.Scores;
using System;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class MatchQueries : AReadModel,
        IMatchQueries,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<TeamRemoved>,
        ISubscribeTo<MatchCreated>,
        ISubscribeTo<TeamGameCompleted>,
        ISubscribeTo<MatchCompleted>
    {
        public MatchQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }

        public class Contingent : AEntity
        {
            public Contingent(Guid id) : base(id) { }
            public string Province { get; internal set; }
        }

        public class Team : AEntity
        {
            public Team(Guid id) : base(id) { }
            public string Name { get; internal set; }
            public string Province { get; internal set; }
            public int Score { get; internal set; }
            public int POA { get; internal set; }
            public decimal Points { get; internal set; }
            public decimal TotalPoints { get; internal set; }
        }

        public class Match : AEntity
        {
            public Match(Guid id) : base(id) { }
            public string Division { get; internal set; }
            public bool IsPOA { get; internal set; }
            public int Number { get; internal set; }
            public Team Away { get; internal set; }
            public string AwayProvince { get; internal set; }
            public Team Home { get; internal set; }
            public string HomeProvince { get; internal set; }
            public int Lane { get; internal set; }
            public BowlingCentre Centre { get; internal set; }
            public string CentreName { get; internal set; }
            public bool IsComplete { get; internal set; }
        }

        public Match GetMatch(string division)
        {
            return Read<Match>(x => x.Division == division).FirstOrDefault();
        }

        public void Handle(ContingentCreated e)
        {
            Create<Contingent>(new Contingent(e.Id) { Province = e.Province });
        }

        public void Handle(TeamCreated e)
        {
            var contingent = Read<Contingent>(x => x.Id == e.Id).FirstOrDefault();

            Create<Team>(new Team(e.TeamId)
            {
                Name = e.Name,
                Province = contingent.Province
            });

            var team = Read<Team>(t => t.Id == e.TeamId).FirstOrDefault();
            foreach (var match in Read<Match>(x => x.Home == null && x.Division == e.Name && x.HomeProvince == contingent.Province).ToList())
                Update<Match>(match.Id, (x, odb) => x.Home = team);
            foreach (var match in Read<Match>(x => x.Away == null && x.Division == e.Name && x.AwayProvince == contingent.Province).ToList())
                Update<Match>(match.Id, (x, odb) => x.Away = team);
        }

        public void Handle(TeamRemoved e)
        {
            Delete<Team>(e.TeamId);
        }

        public void Handle(MatchCreated e)
        {
            var match = Read<Match>(x => x.Id == e.Id).FirstOrDefault();
            if (match != null)
                return;

            var homeTeam = Read<Team>(x => x.Name == e.Division && x.Province == e.Home).FirstOrDefault();
            var awayTeam = Read<Team>(x => x.Name == e.Division && x.Province == e.Away).FirstOrDefault();

            Create(new Match(e.Id)
            {
                Away = awayTeam,
                AwayProvince = e.Away,
                Home = homeTeam,
                HomeProvince = e.Home,
                Centre = e.Centre,
                CentreName = e.CentreName,
                Division = e.Division,
                IsComplete = false,
                IsPOA = e.IsPOA,
                Lane = e.Lane,
                Number = e.Number
            });
        }

        public void Handle(TeamGameCompleted e)
        {
            Update<Team>(e.TeamId, x =>
            {
                x.Score = e.Score;
                x.POA = e.POA;
                x.Points = e.Points;
                x.TotalPoints = e.TotalPoints;
            });
        }

        public void Handle(MatchCompleted e)
        {
            Update<Match>(e.Id, x => x.IsComplete = true);
        }
    }
}
