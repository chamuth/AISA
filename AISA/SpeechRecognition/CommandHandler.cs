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

            //Handle Questions / Queries
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
                //TODO: Update this from a news feed
                return "Well, Sri Lankan Rupee worth " + (1 / 150).ToString() + " United States Dollars, How does that sound?"; //:V
            }
            else if (input.Contains("What's the time") || input.Contains("What time is it?"))
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
            //Handle General Functions
            else if (input.ToLower().Contains("take a selfie") || input.ToLower().Contains("take a picture") || input.Contains("Open Camera"))
            {
                System.Diagnostics.Process.Start("microsoft.windows.camera:");
            }
            else if (input.ToLower().Contains("my picture") || input.ToLower().Contains("my photo"))
            {
                System.Diagnostics.Process.Start("microsoft.windows.photos:");
            }
            else if (input.ToLower().Contains("weather"))
            {
                //Get weather information
                //TODO: Add functionality from AccuWeather or any Weather API
                return "It's 28 degrees and cloudy in Hiriwadunna";
            }
            else if (input.Contains("Play some music") || input.Contains("Play music"))
            {
                System.Diagnostics.Process.Start("microsoft.windows.music:");
            }
            else if (input.Contains("Show me my inbox") || input.Contains("Open Mail"))
            {
                System.Diagnostics.Process.Start("microsoft.windows.mail:");
            }

            //HANDLE CHECK IN SOCIAL MEDIA COMMANDS
            else if (input.Contains("Facebook"))
            {
                System.Diagnostics.Process.Start("http://www.facebook.com/");
            }else if (input.Contains("Twitter"))
            {
                System.Diagnostics.Process.Start("http://www.twitter.com");
            }else if (input.Contains("YouTube"))
            {
                System.Diagnostics.Process.Start("http://www.youtube.com");
            }
            else if (input.Contains("Wikipedia"))
            {
                System.Diagnostics.Process.Start("http://www.wikipedia.com");
            }


            //Something not recognized
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
                //GENERAL QUESTIONS / QUERIES
                "Good Morning",
                "Good Afternoon",
                "Good Evening",
                "Good Night",
                "Hello",
                "Hi",
                "What's up",
                "What's going on",
                "What's the time",
                "What time is it",
                "What's the weather like",
                "Weather",
                "How's the weather like",
                "How's the weather",

                //GENERAL FUNCTIONS
                "Let's take a selfie",
                "Let's take a picture",
                "Take a picture",
                "Take a selfie",
                "Show my pictures",
                "Show my photos",
                "Play some music",
                "Play music",
                "Show me my inbox",
                
                //OPENING APPS RELATED
                "Open Camera",
                "Open Calendar",
                "Open Mail",
                
                //CHECKING SOCIAL MEDIA
                "Check in Facebook",
                "Go to Facebook",
                "Check in Twitter",
                "Go to Twitter",
                "Go to YouTube",
                "Go to Wikipedia"
            };
        }
    }
}
