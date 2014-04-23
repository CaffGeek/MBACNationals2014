using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ReservationQueries : AReadModel,
        IReservationQueries,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantAssignedToRoom>,
        ISubscribeTo<ParticipantRemovedFromRoom>,

        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<ParticipantAssignedToContingent>,
        ISubscribeTo<ParticipantAssignedToTeam>,
        ISubscribeTo<CoachAssignedToTeam>
    {
        public ReservationQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }

        public class Participant : IEntity
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public string Province { get; internal set; }
            public int RoomNumber { get; internal set; }
        }

        public class Contingent : IEntity
        {
            public Guid Id { get; internal set; }
            public string Province { get; internal set; }
        }

        public class Team : IEntity
        {
            public Guid Id { get; internal set; }
            public Guid ContingentId { get; internal set; }
        }

        public List<ReservationQueries.Participant> GetParticipants(string province)
        {
            return Read<Participant>(x => x.Province == province).ToList();
        }

        public void Handle(ParticipantCreated e)
        {
            Create(new Participant
            {
                Id = e.Id,
                Name = e.Name
            });
        }

        public void Handle(ContingentCreated e)
        {
            Create(new Contingent { Id = e.Id, Province = e.Province });
        }

        public void Handle(TeamCreated e)
        {
            Create(new Team { Id = e.TeamId, ContingentId = e.Id });
        }

        public void Handle(ParticipantAssignedToContingent e)
        {
            var contingent = Read<Contingent>(x => x.Id == e.ContingentId).FirstOrDefault();
            Update<Participant>(e.Id, x => x.Province = contingent.Province);
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var team = Read<Team>(x => x.Id == e.TeamId).FirstOrDefault();
            var contingent = Read<Contingent>(x => x.Id == team.ContingentId).FirstOrDefault();
            Update<Participant>(e.Id, x => x.Province = contingent.Province);
        }

        public void Handle(CoachAssignedToTeam e)
        {
            var team = Read<Team>(x => x.Id == e.TeamId).FirstOrDefault();
            var contingent = Read<Contingent>(x => x.Id == team.ContingentId).FirstOrDefault();
            Update<Participant>(e.Id, x => x.Province = contingent.Province);
        }

        public void Handle(ParticipantRenamed e)
        {
            Update<Participant>(e.Id, x => x.Name = e.Name);
        }

        public void Handle(ParticipantAssignedToRoom e)
        {
            Update<Participant>(e.Id, x => x.RoomNumber = e.RoomNumber);
        }

        public void Handle(ParticipantRemovedFromRoom e)
        {
            Update<Participant>(e.Id, x => x.RoomNumber = 0);
        }
    }
}
