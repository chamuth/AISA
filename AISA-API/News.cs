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
        public string thumbnail { get; set; }
    }

    public class NewsItems
    {
        public NewsItem[] news { get; set; }
    }

    public static class News
    {
        public static NewsItem GetNews()
        {
            return new RequestHandler<NewsItem>("/news").Send();
        }

        public static NewsItems GetAllNews()
        {
            return new RequestHandler<NewsItems>("/news/all").Send();
        }
    }
}
