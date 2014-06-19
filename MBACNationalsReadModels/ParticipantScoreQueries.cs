using Edument.CQRS;
using Events.Participant;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ParticipantScoreQueries : AReadModel,
        IParticipantScoreQueries,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantAverageChanged>,
        ISubscribeTo<ParticipantGameCompleted>
    {
        public ParticipantScoreQueries(string readModelFilePath)
            : base(readModelFilePath)
        {

        }

        public class Participant : AEntity
        {
            public Participant(Guid id) : base(id) { }
            public string Name { get; internal set; }
            public string Province { get; internal set; }
            public string Division { get; internal set; }
            public int Average { get; internal set; }
            public int NationalGames { get; internal set; }
            public int NationalTotal { get; internal set; }
            public int NationalAverage { get; internal set; }
            public List<Score> Scores { get; internal set; }
        }

        public class Score
        {
            public string MatchId { get; internal set; }
            public string OpponentProvince { get; internal set; }
            public int Number { get; internal set; }
            public int Scratch { get; internal set; }
            public int POA { get; internal set; }
        }

        public ParticipantScoreQueries.Participant GetParticipant(Guid id)
        {
            return Read<Participant>(x => x.Id == id).FirstOrDefault();
        }

        public void Handle(ParticipantCreated e)
        {
            Create(new Participant(e.Id)
            {
                Name = e.Name,
                Scores = new List<Score>()
            });
        }

        public void Handle(ParticipantAverageChanged e)
        {
            Update<Participant>(e.Id, x =>
            {
                x.Average = e.Average;
            });
        }

        public void Handle(ParticipantGameCompleted e)
        {
            Update<Participant>(e.ParticipantId, x =>
            {
                x.Name = e.Name;
                x.Division = e.Division;
                x.Province = e.Contingent;
                x.NationalGames++;
                x.NationalTotal += e.Score;
                x.NationalAverage = x.NationalTotal / x.NationalGames;
                x.Scores.Add(new Score
                {
                    MatchId = e.Id.ToString(),
                    OpponentProvince = e.Opponent,
                    Number = e.Number,
                    Scratch = e.Score,
                    POA = e.POA
                });
            });
        }
    }
}
