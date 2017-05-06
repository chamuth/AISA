using AISA.Core;
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
            //Deprecate the current context
            Context.Previous = Context.Current;
            //Update the current context
            Context.Current = input;

            var s = GetCommands();

            Console.WriteLine("Command Started");

            //Handle commands by index
            if (input.Contains("Good Morning"))
            {
                if (DateTime.Now.Hour < 12)
                    return "Good morning " + Environment.UserName;
                else if (DateTime.Now.Hour < 15)
                    return "It's not morning, Good Afternoon";
                else
                    return "It's not morning, Good Evening";
            }
            else if (input.Contains("Good Afternoon"))
            {
                if (DateTime.Now.Hour < 12)
                    return "It's not afternoon, Good Morning";
                else if (DateTime.Now.Hour < 15)
                    return "Good Afternoon " + Environment.UserName;
                else
                    return "It's not afternoon, Good Evening";
            }
            else if (input.Contains("Good Evening"))
            {
                if (DateTime.Now.Hour < 12)
                    return "It's not evening, Good Morning";
                else if (DateTime.Now.Hour < 15)
                    return "It's not evening, Good Afternoon";
                else
                    return "Good Evening " + Environment.UserName;
            }
            else if (input.Contains("Good Night"))
            {
                if (DateTime.Now.Hour > 15)
                    return "Good night, make sure you have done your chores";
                else
                    return "It's not yet night, make sure you have done your chores";
            }
            else if (input.Contains("Hello") || input == "Hi")
            {
                //If the user has done something else or the user has just started the application
                if (Context.Previous == "")
                {
                    return new string[]
                    {
                        "Hi! It's a pleasure to see you", "Hello " + Environment.UserName +  "!", "Hi there! What can I do for you?", "Hello there, what can I do for you?"
                    }[new Random().Next(0, 4)];
                }
                else
                {
                    return new string[] {
                        "Well hello to you too", "Hello, let me know if you have something to ask", "Hi, what can I do for you?"
                    }[new Random().Next(0, 3)];
                }
            }
            else if (input.Contains("What's up") || input.Contains("What's going on"))
            {
                //Inform the user with some news about the current situation
                return "Well, Sri Lankan Rupee worth " + (1 / 150).ToString() + " United States Dollars, How does that sound?"; //:V
            }
            else if (input.Contains("What's the time"))
            {
                #region MANUAL FORMAT TIME
                var ampm = "";
                var hour = "";

                if (DateTime.Now.Hour > 12)
                {
                    ampm = "PM";
                    hour = (DateTime.Now.Hour - 12).ToString();
                }
                else
                {
                    ampm = "AM";
                    hour = DateTime.Now.Hour.ToString();

                    if (DateTime.Now.Hour == 0)
                    {
                        hour = "12";
                    }
                }
                #endregion

                return new string[] {
                    "It's " + hour + ":" + DateTime.Now.Minute + " " + ampm, "The time is " + hour + ":" + DateTime.Now.Minute + " " + ampm
                }[new Random().Next()];
            }


            if (Context.Previous == "")
            {
                //Not know how to use?
                return new string[]
                {
                    "Ask me about Classes, I'm pretty good at it", "Ask me about weather", "Ask me for General Knowledge Facts", "Ask me to find a book, I'm sure you'll not be disappointed"
                }[new Random().Next(0, 4)];
            }
            else
            {
                //Asking something else
                return new string[] {
                    "I don't know what to say, Please ask me something else", "Oops, please tell me what you need?"
                }[new Random().Next(0, 2)];
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
                "Good Morning",
                "Good Afternoon",
                "Good Evening",
                "Good Night",
                "Hello",
                "Hi",
                "What's up",
                "What's going on",
                "What's the time"
            };
        }
    }
}
