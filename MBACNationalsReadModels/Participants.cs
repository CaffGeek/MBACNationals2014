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
            public string FirstName { get; internal set; }
            public string LastName { get; internal set; }
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

        private Participant getParticipant(Guid id)
        {
            using (var odb = OdbFactory.Open(dbFileName))
            {
                var participants = odb.QueryAndExecute<Participant>().Where(p=>p.Id.Equals(id));
                return participants.FirstOrDefault();
            }
        }

        public void Handle(ParticipantCreated e)
        {
            using (var odb = OdbFactory.Open(dbFileName))
            {
                odb.Store(
                    new Participant
                        {
                            Id = e.Id,
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            Gender = e.Gender
                        });
                odb.Commit();
            }
        }

        public void Handle(ParticipantRenamed e)
        {
            var participant = getParticipant(e.Id);
            participant.FirstName = e.FirstName;
            participant.LastName = e.LastName;

            using (var odb = OdbFactory.Open(dbFileName))
            {
                odb.Store(participant);
            }
        }
    }
}
