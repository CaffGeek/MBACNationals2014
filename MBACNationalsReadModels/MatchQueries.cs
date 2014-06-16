using Edument.CQRS;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class MatchQueries : AReadModel,
        IMatchQueries,
        ISubscribeTo<MatchCreated>,
        ISubscribeTo<TeamGameCompleted>,
        ISubscribeTo<MatchCompleted>
    {
        public class Match : AEntity
        {
            public Match(Guid id) : base(id) { }
            public string Division { get; internal set; }
            public bool IsPOA { get; internal set; }
            public int Number { get; internal set; }
            public string Away { get; internal set; }
            public string Home { get; internal set; }
            public int Lane { get; internal set; }
            public BowlingCentre Centre { get; internal set; }
            public string CentreName { get; internal set; }
            public bool IsComplete { get; internal set; }
        }

        public Match GetMatch(string division)
        {
            return Read<Match>(x => x.Division == division).FirstOrDefault();
        }

        public void Handle(MatchCreated e)
        {
            var schedule = Read<Match>(x => x.Id == e.Id).FirstOrDefault();
            if (schedule != null)
                return;

            Create(new Match(e.Id) { 
                                
            });
        }

        public void Handle(TeamGameCompleted e)
        {
            Update<Match>(e.Id, x =>
            {

            });
        }

        public void Handle(MatchCompleted e)
        {
            Update<Match>(e.Id, x => x.IsComplete = true);
        }
    }
}
