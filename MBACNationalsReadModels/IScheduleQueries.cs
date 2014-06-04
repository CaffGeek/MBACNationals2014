using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBACNationals.ReadModels
{
    public interface IScheduleQueries
    {
        ScheduleQueries.Schedule GetSchedule(string division);
    }
}
