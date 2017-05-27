using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA_API
{
    /// <summary>
    /// Specifies the Endpoint of the API Client
    /// </summary>
    public class Endpoint
    {
        public static string EndpointString { get; set; }
        
        /// <summary>
        /// Instantiate the API Client with the endpoint specification
        /// </summary>
        /// <param name="endpoint"></param>
        public void Instantiate(string endpoint)
        {
            //Set the endpoint
            EndpointString = endpoint;
        }
    }
}
