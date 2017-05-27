using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA_API
{
    public class NewsItem
    {
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
    }

    public static class News
    {
        public static NewsItem GetNews()
        {
            //Pull data from the AISA Servers
            var item = new NewsItem();

            var restclient = new RestClient(Endpoint.EndpointString + "/news");
            var restrequest = new RestRequest(Method.GET);
            var response = restclient.Execute(restrequest);

            if (response.Content != "")
            {
                try
                {
                    item = Newtonsoft.Json.JsonConvert.DeserializeObject<NewsItem>(response.Content);
                }
                catch (Exception ex)
                {
                    throw new RestResponseException(ex.Message);
                }
            }
            
            return item;
        }
    }
}
