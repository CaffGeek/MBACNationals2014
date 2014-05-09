using Edument.CQRS;
using Events.Participant;
using NDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ParticipantProfileQueries : AReadModel,
        IParticipantProfileQueries,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantProfileChanged>
    {
        public ParticipantProfileQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }

        public class Participant : AEntity
        {
            public Participant(Guid id) : base(id) { }
            public string Name { get; internal set; }
            public int Age { get; internal set; }
            public string HomeTown { get; set; }
            public string MaritalStatus { get; internal set; }
            public string SpouseName { get; internal set; }
            public string Children { get; internal set; }
            public string Occupation { get; internal set; }
            public string HomeCenter { get; internal set; }
            public int YearsBowling { get; internal set; }
            public int NumberOfLeagues { get; internal set; }
            public int HighestAverage { get; internal set; }
            public int YearsCoaching { get; internal set; }
            public string BestFinishProvincially { get; internal set; }
            public string BestFinishNationally { get; internal set; }
            public int MasterProvincialWins { get; internal set; }
            public string MastersAchievements { get; internal set; }
            public string OpenAchievements { get; internal set; }
            public int OpenYears { get; internal set; }
            public string OtherAchievements { get; internal set; }
            public string Hobbies { get; internal set; }
        }

        public Participant GetProfile(Guid id)
        {
            return Read<Participant>(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public void Handle(ParticipantCreated e)
        {
            Create(new Participant(e.Id) { Name = e.Name });
        }

        public void Handle(ParticipantRenamed e)
        {
            Update<Participant>(e.Id, x => { x.Name = e.Name; });
        }

        public void Handle(ParticipantProfileChanged e)
        {
            Update<Participant>(e.Id, x =>
            {
                x.Age = e.Age;
                x.HomeTown = e.HomeTown;
                x.MaritalStatus = e.MaritalStatus;
                x.SpouseName = e.SpouseName;
                x.Children = e.Children;
                x.Occupation = e.Occupation;
                x.HomeCenter = e.HomeCenter;
                x.YearsBowling = e.YearsBowling;
                x.NumberOfLeagues = e.NumberOfLeagues;
                x.HighestAverage = e.HighestAverage;
                x.YearsCoaching = e.YearsCoaching;
                x.BestFinishProvincially = e.BestFinishProvincially;
                x.BestFinishNationally = e.BestFinishNationally;
                x.MasterProvincialWins = e.MasterProvincialWins;
                x.MastersAchievements = e.MastersAchievements;
                x.OpenAchievements = e.OpenAchievements;
                x.OpenYears = e.OpenYears;
                x.OtherAchievements = e.OtherAchievements;
                x.Hobbies = e.Hobbies;
            });
        }
    }
}
