using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA_API
{
    public interface IFactualEngineHelper
    {
        string GenerateRoute();
    }

    public class ScienceFactualEngineHelper : IFactualEngineHelper
    {
        public string GenerateRoute()
        {
            return Endpoint.EndpointString + "/science/facts";
        }
    }

    public class MathematicsFactualEngineHelper : IFactualEngineHelper
    {
        public string GenerateRoute()
        {
            return Endpoint.EndpointString + "/mathematics/facts";
        }
    }
}