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
        /// Get random string from the input string array
        /// </summary>
        /// <param name="input">The string array</param>
        /// <returns>String selected randomly</returns>
        private static string random(string[] input)
        {
            return input[new Random().Next(0, input.Length)];
        }

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
                    return random(new string[]
                    {
                        "Hi! It's a pleasure to see you", "Hello " + Environment.UserName +  "!", "Hi there! What can I do for you?", "Hello there, what can I do for you?"
                    });
                }
                else
                {
                    return random(new string[] {
                        "Well hello to you too", "Hello, let me know if you have something to ask", "Hi, what can I do for you?"
                    });
                }
            }
            else if (input.Contains("What's up") || input.Contains("What's going on"))
            {
                //Inform the user with some news about the current situation
                //TODO: Update this from a news feed
                return "Well, Sri Lankan Rupee worth " + (1f / 150f).ToString() + " United States Dollars, How does that sound?"; //:V
            }
            else if (input.Contains("What's the time") || input.Contains("What time is it"))
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

                return random(new string[] {
                    "It's " + hour + ":" + DateTime.Now.Minute + " " + ampm, "The time is " + hour + ":" + DateTime.Now.Minute + " " + ampm
                });
            }
            else if (input.Contains("Thank you") || input.Contains("Thanks"))
            {
                if (Context.Previous == null)
                {
                    return "For doing what? Anyway welcome!";
                }
                else
                {
                    return random(new string[]
                    {
                        "Welcome!", "It's my pleasure!"
                    });
                }
            }
            else if (input.Contains("What can I say"))
            {
                return "Say " + random(GetCommands());
            }

            //HANDLE QUESTIONS ABOUT AISA
            else if (input.Contains("Who are you"))
            {
                return random(new string[] {
                    "I'm AISA, your personal assistant",
                    "AISA, Artificial Intelligent Smart Assistant of yours",
                    "I'm your personal assistant, AISA",
                    "I am AISA, what can I do for you?"
                });
            }
            else if (input.Contains("Who made you"))
            {
                //TODO:Set the URL to a correct endpoint
                Context.LastURL = "http://www.github.com";
                ViewControllerConnector.Connect(ViewControllerConnector.ConnectionMethod.URL, "AISA - GitHub", "http://www.github.com/");

                return random(new string[]
                {
                    "I was developed by Team Ninponix. Say \"Follow Link\" to visit my repository", "Team Ninponix, Say \"Go to Link\" to visit my repository"
                });
            }
            else if (input.Contains("What's your name"))
            {
                return random(new string[] {
                    "It's AISA", "AISA, Artificial Intelligent Smart Assistant"
                });
            }


            else if (input.Contains("Follow link") || input.Contains("Go to link") || input.Contains("Visit link") || input.Contains("Visit last link") || input.Contains("Follow last link") || input.Contains("Go to last link"))
            {
                if (Context.LastURL.Trim() != "")
                {
                    //Follow the last link
                    System.Diagnostics.Process.Start(Context.LastURL);
                    Context.LastURL = "";
                    return "Opening web browser";
                }
                else
                {
                    return "What link?";
                }
            }

            //Handle General Functions
            else if (input.ToLower().Contains("take a selfie") || input.ToLower().Contains("take a picture") || input.Contains("Open Camera"))
            {
                System.Diagnostics.Process.Start("microsoft.windows.camera:");
                return random(new string[] {
                    "Sure", "Okay"
                });
            }
            else if (input.ToLower().Contains("my picture") || input.ToLower().Contains("my photo"))
            {
                System.Diagnostics.Process.Start("microsoft.windows.photos:");

                return random(new string[] {
                    "Sure, Let's check them out", "Let's see your photos"
                });
            }
            else if (input.Contains("What is my name"))
            {
                return Environment.UserName; //return the user name of the computer
            }
            else if (input.ToLower().Contains("weather"))
            {
                //Get weather information
                //TODO: Add functionality from AccuWeather or any Weather API
                //TODO: Update the connection information of the AccuWeather link
                ViewControllerConnector.Connect(ViewControllerConnector.ConnectionMethod.URL, "AccuWeather Report", "http://accuweather.co");
                return "It's 28 degrees and cloudy in Hiriwadunna";
            }
            else if (input.Contains("Play some music") || input.Contains("Play music"))
            {
                System.Diagnostics.Process.Start("microsoft.windows.music:");
                return "Sure";
            }
            else if (input.Contains("Show me my inbox") || input.Contains("Open Mail"))
            {
                System.Diagnostics.Process.Start("microsoft.windows.mail:");
                return "Sure";
            }
            else if (input.Contains("Open App store"))
            {
                System.Diagnostics.Process.Start("microsoft.windows.store:");
                return "Sure";
            }

            //HANDLE CHECK IN SOCIAL MEDIA COMMANDS
            else if (input.Contains("Facebook"))
            {
                System.Diagnostics.Process.Start("http://www.facebook.com/");
                return "Checking in Facebook";
            }
            else if (input.Contains("Twitter"))
            {
                System.Diagnostics.Process.Start("http://www.twitter.com");
                return "Checking in Twitter";
            }
            else if (input.Contains("YouTube"))
            {
                System.Diagnostics.Process.Start("http://www.youtube.com");
                return "Visiting YouTube";
            }
            else if (input.Contains("Wikipedia"))
            {
                System.Diagnostics.Process.Start("http://www.wikipedia.com");
                return "Visiting Wikipedia";
            }


            //Something not recognized
            if (Context.Previous == "")
            {
                //Not know how to use?
                return random(new string[] {
                    "Ask me about Classes, I'm pretty good at it", "Ask me about weather", "Ask me for General Knowledge Facts", "Ask me to find a book, I'm sure you'll not be disappointed"
                });
            }
            else
            {
                //Asking something else
                return random(new string[] {
                    "I don't know what to say, Please ask me something else", "Oops, please tell me what you need?", "Sorry, I don't understand what you're saying", "Sorry, I didn't understand that, please retry"
                });
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
                "What is my name",
                "How's the weather like",
                "How's the weather",
                "Thank you",
                "Thanks",
                "What can I say",

                //ABOUT AISA
                "Who are you",
                "Who made you",
                "What's your name",

                //GENERAL FUNCTIONS
                "Follow link",
                "Visit link",
                "Visit last link",
                "Follow last link",
                "Go to link",
                "Go to last link",
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
                "Open App store",

                
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
