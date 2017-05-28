using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA_API
{
    /// <summary>
    /// Represents a single joke item received from the servers
    /// </summary>
    public class Joke
    {
        public string joke { get; set; }
    }

    public static class Jokes
    {
        public static Joke Find()
        {
            return new RequestHandler<Joke>("/joke").Send();
        }
    }
}
