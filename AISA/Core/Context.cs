using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA.Core
{
    public static class Context
    {
        public static string Current { get; set; }
        public static string Previous { get; set; }
        public static string LastURL { get; set; }

        public static string[] BuyThatBook { get; set; }
        public static string[] previousPaper { get; set; }


        public static Scholar.MCQPaper currentPaper { get; set; }
    }
}
