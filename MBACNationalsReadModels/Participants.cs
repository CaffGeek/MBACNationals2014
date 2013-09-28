using Edument.CQRS;
using Events.Participant;
using Events.Team;
using NDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class Participants : AReadModel,
        IParticipantQueries,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantAssignedToTeam>
    {
        public class Participant
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public Enums.Gender Gender { get; internal set; }
            public Guid TeamId { get; internal set; }
            public string TeamName { get; internal set; }
        }

        public List<Participant> GetParticipants()
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var participants = odb.QueryAndExecute<Participant>();
                return participants.ToList();
            }
        }

        public Participant GetParticipant(Guid id)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var participants = odb.QueryAndExecute<Participant>();
                var q = participants.Where(p => p.Id.Equals(id));
                return q.FirstOrDefault();
            }
        }

        public void Handle(ParticipantCreated e)
        {
            if (GetParticipant(e.Id) != null)
                return; //Already created

            using (var odb = OdbFactory.Open(ReadModelFilePath))
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
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var participant = odb.QueryAndExecute<Participant>().Where(p => p.Id.Equals(e.Id)).FirstOrDefault();
                participant.Name = e.Name;
                odb.Store(participant);
            }
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var participant = odb.QueryAndExecute<Participant>().Where(p => p.Id.Equals(e.Id)).FirstOrDefault();
                var team = odb.QueryAndExecute<Teams.Team>().Where(t => t.Id.Equals(e.TeamId)).FirstOrDefault(); //TODO: Move the team name into the event
                participant.TeamId = team.Id;
                participant.TeamName = team.Name;
                odb.Store(participant);
            }
        }
    }
}
