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
        ISubscribeTo<ParticipantRenamed>
    {
        public class Contingent
        {
            public Guid Id { get; internal set; }
            public string Province { get; internal set; }
            public IList<Team> Teams { get; internal set; }
        }

        public class Team
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

        public class Participant
        {
            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public Guid TeamId { get; internal set; }
            public Guid ContingentId { get; internal set; }
        }

        public Contingent GetContingent(Guid id)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                return GetContingent(id, odb);
            }
        }

        public Contingent GetContingent(string province)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                return GetContingent(province, odb);
            }
        }

        private Contingent GetContingent(Guid id, NDatabase.Api.IOdb odb)
        {
            if (odb == null) return GetContingent(id);

            var contingents = odb.QueryAndExecute<Contingent>().Where(p => p.Id.Equals(id));
            return contingents.FirstOrDefault();
        }

        private Contingent GetContingent(string province, NDatabase.Api.IOdb odb)
        {
            if (odb == null) return GetContingent(province);

            var contingents = odb.QueryAndExecute<Contingent>().Where(p => p.Province.Equals(province));
            return contingents.FirstOrDefault();
        }

        private Participant GetParticipant(Guid id, NDatabase.Api.IOdb odb)
        {
            var participants = odb.QueryAndExecute<Participant>().Where(p => p.Id.Equals(id));
            return participants.FirstOrDefault();
        }

        public void Handle(ContingentCreated e)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                if (GetContingent(e.Id, odb) != null)
                    return; //Already created

                odb.Store(
                    new Contingent
                    {
                        Id = e.Id,
                        Province = e.Province,
                        Teams = new List<Team>()
                    });
            }
        }

        public void Handle(TeamCreated e)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var contingent = GetContingent(e.Id, odb);
                if (contingent == null)
                    return; //Contingent does not exist!

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

                odb.Store(contingent);
            }
        }

        public void Handle(ParticipantCreated e)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var participant = GetParticipant(e.Id, odb);
                if (participant != null)
                    return; //Already created

                odb.Store(
                    new Participant
                    {
                        Id = e.Id,
                        Name = e.Name
                    });
            }
        }

        public void Handle(ParticipantAssignedToTeam e)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {
                var contingent = odb.QueryAndExecute<Contingent>().FirstOrDefault(c => c.Teams.Any(t => t.Id.Equals(e.TeamId)));
                if (contingent == null)
                    return;

                var team = contingent.Teams.FirstOrDefault(t => t.Id.Equals(e.TeamId));
                if (team == null)
                    return;

                var participant = GetParticipant(e.Id, odb);
                if (participant == null)
                    return;

                team.Bowlers.Add(participant);

                odb.Store(contingent);
            }
        }

        public void Handle(ParticipantRenamed e)
        {
            using (var odb = OdbFactory.Open(ReadModelFilePath))
            {

                var participant = GetParticipant(e.Id, odb);
                if (participant == null)
                    return;

                participant.Name = e.Name;

                odb.Store(participant);
            }
        }
    }
}
