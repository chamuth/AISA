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
        /// Finds a Scholarship
        /// </summary>
        /// <returns>Returns a single Chance object that contains details about the competition</returns>
        public static Chance FindCompetition()
        {
            return new RequestHandler<Chance>("/competitions").Send();
        }

        /// <summary>
        /// Finds a Scholarship
        /// </summary>
        /// <returns>Returns a single Chance object that contians details about the scholarship</returns>
        public static Chance FindScholarship()
        {
            return new RequestHandler<Chance>("/scholarhips").Send();
        }

    }
}
