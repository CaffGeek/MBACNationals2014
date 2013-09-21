using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBACNationals.ReadModels
{
    public abstract class AReadModel
    {
        protected string ReadModelFilePath = MBACNationalsReadModels.Properties.Settings.Default.ReadModelConnection;
    }
}
