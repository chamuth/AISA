using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA.Core
{
    public static class ViewControllerConnector
    {
        public enum ConnectionMethod
        {
            URL, 
            Image
        }

        public static Action None;
        public static Action<ConnectionMethod, string, string> Connect;
        public static Action<string> ChangeHypothesis;
    }
}
