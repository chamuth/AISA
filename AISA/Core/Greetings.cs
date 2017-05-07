using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA.Core
{
    public static class Greetings
    {
        private static string[] greetings =
        {
            "Hello %USERNAME%!", "How are you?", "How's it going %USERNAME%", "What's cracking?", "Good to see you %USERNAME%", "What's up %USERNAME%", "Good %TIME% %USERNAME%!"
        };

        /// <summary>
        /// Get a random personalized greeting text
        /// </summary>
        /// <returns></returns>
        public static string Greet()
        {
            var number = new Random().Next(0, 7);
            var new_greet = "";

            if (number > 5)
            {
                new_greet = greetings[6];

                new_greet = new_greet.Replace("%USERNAME%", Environment.UserName);

                if (DateTime.Now.Hour < 12)
                    new_greet = new_greet.Replace("%TIME%", "morning");
                else if (DateTime.Now.Hour > 12 && 15 > DateTime.Now.Hour)
                    new_greet = new_greet.Replace("%TIME%", "afternoon");
                else if (DateTime.Now.Hour > 15)
                    new_greet = new_greet.Replace("%TIME%", "evening");

            }
            else
            {
                new_greet = greetings[number];
                new_greet = new_greet.Replace("%USERNAME%", Environment.UserName);
            }

            return new_greet;
        }
    }
}
