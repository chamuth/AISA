using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA_API
{
    /// <summary>
    /// Represents a set of Questions and Answers related to each other
    /// </summary>
    public class QADataset
    {
        public string[] questions { get; set; }
        public string[] answers { get; set; }
    }

    /// <summary>
    /// Has method/methods required for a client to get questions from the AISA dynamic servers
    /// </summary>
    public static class Whatis
    {
        public static QADataset GetWhatis()
        {
            return new RequestHandler<QADataset>("/whatis").Send();
        }
    }
}
