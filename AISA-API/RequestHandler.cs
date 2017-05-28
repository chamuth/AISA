using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA_API
{
    /// <summary>
    /// Handles individual requests that are sent to the AISA API and returns an object with their de-serialization type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RequestHandler<T>
    {
        public string URL { get; set; }

        public RequestHandler(string url)
        {
            URL = url;
        }

        public T Send()
        {
            var restclient = new RestClient(Endpoint.EndpointString + URL);
            var restrequest = new RestRequest(Method.GET);
            var restresponse = restclient.Execute(restrequest);

            if (restresponse.Content != "")
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(restresponse.Content);
                }
                catch (Exception ex)
                {
                    throw new RestResponseException(ex.Message);
                }
            }
            else
            {
                throw new RestResponseException();
            }
        }
    }
}
