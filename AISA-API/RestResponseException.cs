using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA_API
{
    [Serializable]
    public class RestResponseException : Exception
    {
        public RestResponseException() { }
        public RestResponseException(string message) : base(message) { }
        public RestResponseException(string message, Exception inner) : base(message, inner) { }
        protected RestResponseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
