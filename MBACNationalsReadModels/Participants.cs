using Edument.CQRS;
using Events.Participant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class Participants : IParticipantQueries,
        ISubscribeTo<ParticipantCreated>
    {
        public class Participant
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Enums.Gender Gender { get; set; }
        }

        private Dictionary<Guid, Participant> participants =
            new Dictionary<Guid, Participant>();

        public List<Participant> GetParticipants()
        {
            return participants.Select(x => x.Value).ToList();
        }

        public void Handle(ParticipantCreated e)
        {
            lock (participants)
                participants.Add(
                        e.Id,
                        new Participant
                        {
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            Gender = e.Gender
                        }
                    );
        }
    }
}
