using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA_API
{
    /// <summary>
    /// An object that represents a single Fact from the AISA API Server
    /// </summary>
    public class Fact
    {
        public string fact { get; set; }
    }

    /// <summary>
    /// Connects to the AISA API Server and obtains facts from there
    /// </summary>
    public static class FactualEngine
    {
        /// <summary>
        /// Get a single Fact object selected randomly from the AISA servers.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static Fact GetFact(IFactualEngineHelper helper)
        {
            return new RequestHandler<Fact>(helper.GenerateRoute()).Send();
        }
    }

}
