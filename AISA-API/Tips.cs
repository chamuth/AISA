using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA_API
{
    public class Tip
    {
        public string tip { get; set; }
    }

    public static class Tips
    {

        public static Tip Get (ITipType type)
        {
            return new RequestHandler<Tip>(type.GetCategory()).Send();
        }
    }

}
