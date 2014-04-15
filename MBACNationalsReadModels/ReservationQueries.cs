using Edument.CQRS;
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
        ISubscribeTo<ParticipantRemovedFromRoom>
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

        public List<ReservationQueries.Participant> GetParticipants(string province)
        {
            return Read<Participant>(x => x.Province == province).ToList();
        }

        public void Handle(ParticipantCreated e)
        {
            Create(new Participant
            {
                Id = e.Id,
                Name = e.Name,
                Province = "MB" //TODO: add province to Participant Created!
            });
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
