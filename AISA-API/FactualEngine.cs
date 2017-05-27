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
        public static Fact GetFact(IFactualEngineHelper helper)
        {
            var fact = new Fact();

            var restclient = new RestClient(helper.GenerateRoute());
            var restrequest = new RestRequest(Method.GET);

            var restresponse = restclient.Execute(restrequest);

            if (restresponse.Content != "")
            {
                try
                {
                    fact = JsonConvert.DeserializeObject<Fact>(restresponse.Content);
                }
                catch (JsonException ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new RestResponseException("Response is invalid");
            }

            return fact;
        }
    }

}
