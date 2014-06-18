using Edument.CQRS;
using Events.Scores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class HighScoreQueries : AReadModel,
        IHighScoreQueries,
        ISubscribeTo<ParticipantGameCompleted>
    {
        public HighScoreQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }

        public void Handle(ParticipantGameCompleted e)
        {
            //TODO:
        }
    }
}
