using Edument.CQRS;
using Events.Participant;
using NDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class Participants : IParticipantQueries,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>
    {
        private string dbFileName = MBACNationalsReadModels.Properties.Settings.Default.ReadModelConnection;

        public class Participant
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public Enums.Gender Gender { get; internal set; }
        }

        public List<Participant> GetParticipants()
        {
            using (var odb = OdbFactory.Open(dbFileName))
            {
                var participants = odb.QueryAndExecute<Participant>();
                return participants.ToList();
            }
        }

        public Participant GetParticipant(Guid id)
        {
            using (var odb = OdbFactory.Open(dbFileName))
            {
                var participants = odb.QueryAndExecute<Participant>().Where(p=>p.Id.Equals(id));
                return participants.FirstOrDefault();
            }
        }

        public void Handle(ParticipantCreated e)
        {
            if (GetParticipant(e.Id) != null)
                return; //Already created

            using (var odb = OdbFactory.Open(dbFileName))
            {
                odb.Store(
                    new Participant
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Gender = e.Gender
                        });
            }
        }

        public void Handle(ParticipantRenamed e)
        {
            using (var odb = OdbFactory.Open(dbFileName))
            {
                var participant = odb.QueryAndExecute<Participant>().Where(p => p.Id.Equals(e.Id)).FirstOrDefault();
                participant.Name = e.Name;
                odb.Store(participant);
            }
        }
    }
}
