using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA_API
{
    /// <summary>
    /// Represents a single Scholarship or Competition that is taken from the AISA Servers
    /// </summary>
    public class Chance
    {
        public string chance { get; set; }
        public string description { get; set; }
        public string website { get; set; }
    }

    /// <summary>
    /// Contains methods and properties needed in order to find details about Scholarships and Competitions available in Sri Lanka
    /// </summary>
    public class ScolsAndComps
    {
        /// <summary>
        /// Finds a Scholarship or Competitions
        /// </summary>
        /// <returns>Returns a single Chance object that contains details about the scholarship or competition</returns>
        public static Chance Find()
        {
            var chance = new Chance();
            var restclient = new RestClient(Endpoint.EndpointString + "/chances");
            var restrequest = new RestRequest(Method.GET);

            var response = restclient.Execute(restrequest);

            if (response.Content != "")
            {
                try
                {
                   chance = Newtonsoft.Json.JsonConvert.DeserializeObject<Chance>(response.Content);
                }
                catch (Exception ex)
                {
                    throw new RestResponseException(ex.Message);
                }
            }

            return chance;
        }

    }
}
