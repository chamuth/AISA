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
            Image,
            Book
        }

        public static Action None;
        public static Action<ConnectionMethod, string[]> Connect;
        public static Action<string> ChangeHypothesis;

        public static Action Exit;

        public static bool StartedCommandRecognition = false;

        public static Action<string, string> AsyncResult;

        public static Action MainWindowHide;
        public static Action MainWindowShow;

    }
}
