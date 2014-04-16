using Edument.CQRS;
using Events.Contingent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public class ContingentTravelPlanQueries : AReadModel,
        IContingentTravelPlanQueries,
        ISubscribeTo<ContingentCreated>,
        ISubscribeTo<TravelPlansChanged>
    {
        public ContingentTravelPlanQueries(string readModelFilePath)
            : base(readModelFilePath) 
        {

        }

        public class ContingentTravelPlans : IEntity
        {
            public Guid Id { get; internal set; }
            public string Province { get; internal set; }
            public IList<TravelPlan> TravelPlans { get; internal set; }
        }

        public class TravelPlan
        {
            public string ModeOfTransportation { get; internal set; }
            public string When { get; internal set; }
            public string FlightNumber { get; internal set; }
            public int NumberOfPeople { get; internal set; }
            public int Type { get; internal set; }
        }

        public ContingentTravelPlans GetTravelPlans(string province)
        {
            return Read<ContingentTravelPlans>(x => x.Province.Equals(province)).FirstOrDefault();
        }

        public void Handle(ContingentCreated e)
        {
            Create(new ContingentTravelPlans
            {
                Id = e.Id,
                Province = e.Province,
                TravelPlans = new List<TravelPlan>()
            });
        }

        public void Handle(TravelPlansChanged e)
        {
            Update<ContingentTravelPlans>(e.Id, contingent =>
            {
                contingent.TravelPlans = e.TravelPlans.Select(x =>
                {
                    return new TravelPlan
                    {
                        ModeOfTransportation = x.ModeOfTransportation,
                        When = x.When.ToString("yyyy-MM-ddTHH:mm"),
                        FlightNumber = x.FlightNumber,
                        NumberOfPeople = x.NumberOfPeople,
                        Type = x.Type
                    };
                }).ToList();
            });
        }
    }
}
