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

        private static string[] EndGreetings =
        {
            "Good bye!", "Bye", "Good Night"
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

        /// <summary>
        /// Get a random personalized greeting text when the user leaves the program
        /// </summary>
        /// <returns></returns>
        public static string End()
        {
            string new_greet = "";
            if (DateTime.Now.Hour > 15)
            {
                new_greet = EndGreetings[2];
            }
            else
            {
                new_greet = EndGreetings[new Random().Next(0, 2)];
            }

            return new_greet;
        }
    }
}
