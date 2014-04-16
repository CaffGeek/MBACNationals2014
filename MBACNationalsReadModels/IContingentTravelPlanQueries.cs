using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface IContingentTravelPlanQueries
    {
        ContingentTravelPlanQueries.ContingentTravelPlans GetTravelPlans(string province);
    }
}