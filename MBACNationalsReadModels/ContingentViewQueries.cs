using Edument.CQRS;
using Events.Contingent;
using Events.Participant;
using NDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentViewQueries : AReadModel,
        IContingentViewQueries,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TeamCreated>,
        ISubscribeTo<ParticipantCreated>,
        ISubscribeTo<ParticipantAssignedToTeam>,
        ISubscribeTo<ParticipantRenamed>,
        ISubscribeTo<ParticipantDelegateStatusGranted>,
        ISubscribeTo<ParticipantDelegateStatusRevoked>,
        ISubscribeTo<ParticipantYearsQualifyingChanged>
    {
        public class Contingent : IEntity
        {
            public Guid Id { get; internal set; }
            public string Province { get; internal set; }
            public IList<Team> Teams { get; internal set; }
        }

        public class Team : IEntity
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public Guid ContingentId { get; internal set; }
            public IList<Participant> Bowlers { get; internal set; }
            public string Gender { get; internal set; }
            public int SizeLimit { get; internal set; }
            public bool RequiresShirtSize { get; internal set; }
            public bool RequiresCoach { get; internal set; }
            public bool RequiresAverage { get; internal set; }
            public bool RequiresBio { get; internal set; }
            public bool RequiresGender { get; internal set; }
            public bool IncludesSinglesRep { get; internal set; }
        }

        public class Participant : IEntity
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public Guid TeamId { get; internal set; }
            public Guid ContingentId { get; internal set; }
            public bool IsRookie { get; internal set; }
            public bool IsDelegate { get; internal set; }
        }

        public Contingent GetContingent(Guid id)
        {
            return Read<Contingent>(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public Contingent GetContingent(string province)
        {
            return Read<Contingent>(x => x.Province.Equals(province)).FirstOrDefault();            
        }
        
        public void Handle(ContingentCreated e)
        {
            Create(new Contingent
                    {
                        Id = e.Id,
                        Province = e.Province,
                        Teams = new List<Team>()
                    });
        }

        public void Handle(TeamCreated e)
        {
            Update<Contingent>(e.Id, contingent =>
            {
                contingent.Teams.Add(
                    new Team
                    {
                        Id = e.TeamId,
                        Name = e.Name,
                        ContingentId = e.Id,
                        SizeLimit = e.SizeLimit,
                        Bowlers = new List<Participant>(),
                        Gender = e.Gender,
                        RequiresShirtSize = e.RequiresShirtSize,
                        RequiresCoach = e.RequiresCoach,
                        RequiresAverage = e.RequiresAverage,
                        RequiresBio = e.RequiresBio,
                        RequiresGender = e.RequiresGender,
                        IncludesSinglesRep = e.IncludesSinglesRep,
                    });
            });
        }

        public void Handle(ParticipantCreated e)
        {
            Create(new Participant
                    {
                        Id = e.Id,
                        Name = e.Name,
                        IsDelegate = e.IsDelegate,
                        IsRookie = e.YearsQualifying == 1
                    });
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            var cntgt = Read<Contingent>(c => c.Teams.Any(t => t.Id.Equals(e.TeamId))).FirstOrDefault();
            if (cntgt == null)
                return;

            Update<Contingent>(cntgt.Id, (contingent, odb) => {
                var team = contingent.Teams.FirstOrDefault(t => t.Id.Equals(e.TeamId));
                if (team == null)
                    return;

                var participant = Read<Participant>(x => x.Id.Equals(e.Id), odb).FirstOrDefault();
                if (participant == null)
                    return;

                team.Bowlers.Add(participant);
            });
        }

        public void Handle(ParticipantRenamed e)
        {
            Update<Participant>(e.Id, x => { x.Name = e.Name; });
        }

        public void Handle(ParticipantDelegateStatusGranted e)
        {
            Update<Participant>(e.Id, x => { x.IsDelegate = true; });
        }

        public void Handle(ParticipantDelegateStatusRevoked e)
        {
            Update<Participant>(e.Id, x => { x.IsDelegate = false; });
        }

        public void Handle(ParticipantYearsQualifyingChanged e)
        {
            Update<Participant>(e.Id, x => { x.IsRookie = e.YearsQualifying == 1; });
        }
    }
}
