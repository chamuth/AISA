using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA
{
    public static class CommandHandler
    {
        /// <summary>
        /// Handles ongoing commands sent by the system
        /// </summary>
        /// <param name="input">The command input</param>
        public static string Handle(string input)
        {
            var s = GetCommands();

            Console.WriteLine("Command Started");

            //Handle commands by index
            if (s[0] == input)
            {
                Console.WriteLine("Command Handled");
                return "Good morning to you too sir";
            }
            else if (s[1] == input)
            {
                return "It's 28 degrees and partly cloudy in Hiriwadunna";
            }
            else
            {
                return "I don't know what to say";
            }
        }

        /// <summary>
        /// Retrieve the commands
        /// </summary>
        /// <returns>An Array of strings containing commands</returns>
        public static string[] GetCommands()
        {
            return new string[]
            {
                "Good Morning", "What's the weather like"
            };
        }
    }
}
