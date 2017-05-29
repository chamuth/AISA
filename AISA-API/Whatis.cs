using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA_API
{
    public class QADataset
    {
        public string[] questions { get; set; }
        public string[] answers { get; set; }
    }

    public static class Whatis
    {
        public static QADataset GetWhatis()
        {
            return new RequestHandler<QADataset>("/whatis").Send();
        }
    }
}
