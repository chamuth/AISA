using AISA.Core;
using Newtonsoft.Json;
using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AISA
{

    #region FOR DESERIALIZING JSON
    public class LocationResult
    {
        public string status { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string region { get; set; }
        public string regionName { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string timezone { get; set; }
        public string isp { get; set; }
        public string org { get; set; }
        public string _as { get; set; }
        public string query { get; set; }
    }

    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class WeatherResult
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

    #endregion

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

            #region GENERAL COMMAND HANDLING
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
            else if (input.Contains("Exit") || input.Contains("Exit AISA") || input.Contains("Close AISA") || input.Contains("Close") || input.Contains("Bye") || input.Contains("Good Bye"))
            {
                ViewControllerConnector.Exit();
                return "";
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
            else if (input.Contains("What's up") || input.Contains("What's going on") || input.Contains("What is going on") || input.Contains("What is up"))
            {
                //Inform the user with some news about the current situation
                //TODO: Update this from a news feed
                return "Well, Sri Lankan Rupee worth " + (1f / 150f).ToString() + " United States Dollars, How does that sound?"; //:V
            }
            else if (input.Contains("What's the time") || input.Contains("What time is it") || input.Contains("What is the time") || input.Contains("Time"))
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
            else if (input.Contains("I appreciate it") || input.Contains("Appreciate it"))
            {
                return random(new string[] { "Thank you", "It's my pleasure" });
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
                Context.LastURL = "https://github.com/Chamuth/AISA/tree/master";
                ViewControllerConnector.Connect(ViewControllerConnector.ConnectionMethod.URL, "Chamuth/AISA - GitHub", "https://github.com/Chamuth/AISA/tree/master");

                return random(new string[]
                {
                    "I was developed by Team Ninponix. Say \"Follow Link\" to visit my repository", "Team Ninponix, Say \"Go to Link\" to visit my repository"
                });
            }
            else if (input.Contains("What's your name") || input.Contains("What is your name"))
            {
                return random(new string[] {
                    "It's AISA", "AISA, Artificial Intelligent Smart Assistant"
                });
            }
            else if (input.Contains("Where are you") || input.Contains("Where do you live"))
            {
                return random(new string[]
                {
                    "I'm inside your computer you silly", "I live in your computer", "I'm in your computer"
                });
            }
            else if (input.Contains("Who is your father"))
            {
                Context.LastURL = "https://github.com/Chamuth/AISA/tree/master";
                ViewControllerConnector.Connect(ViewControllerConnector.ConnectionMethod.URL, "Chamuth/AISA - GitHub", "https://github.com/Chamuth/AISA/tree/master");

                return random(new string[]
                {
                    "I was developed by Team Ninponix. Say \"Follow Link\" to visit my repository", "Team Ninponix, Say \"Go to Link\" to visit my repository"
                });
            }
            else if (input.Contains("What are you"))
            {
                return random(new string[]
                {
                    "I'm a Personal Assistant", "I'm Smart Personal Assistant of yours"
                });
            }
            else if (input.Contains("about you"))
            {
                return random(new string[]
                {
                    "I'm AISA, an Artificial Intelligent Smart Assistant developed by Ninponix"
                });
            }
            else if (input.Contains("How you were made"))
            {
                Context.LastURL = "https://github.com/Chamuth/AISA/tree/master";
                ViewControllerConnector.Connect(ViewControllerConnector.ConnectionMethod.URL, "Chamuth/AISA - GitHub", "https://github.com/Chamuth/AISA/tree/master");

                return random(new string[] {
                    "Checkout my open-source repository here", "I'm actually open source,", "You can see for yourself"
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
            else if (input.Contains("What is my name") || input.Contains("What's my name") || input.Contains("Say my name"))
            {
                return Environment.UserName; //return the user name of the computer
            }
            else if (input.ToLower().Contains("weather"))
            {
                // Get location information first
                var client = new RestClient("http://ip-api.com/json");
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                try
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // Get weather information then
                        LocationResult l = JsonConvert.DeserializeObject<LocationResult>(response.Content);
                        var weatherclient = new RestClient("http://api.openweathermap.org/data/2.5/weather?q=" + l.city + "&appid=6bc37d05c9bb515c72cd40db94325f51");
                        var weatherrequest = new RestRequest(Method.GET);
                        IRestResponse weatherresponse = weatherclient.Execute(weatherrequest);

                        if (weatherresponse.StatusCode == System.Net.HttpStatusCode.OK && weatherresponse.Content.Trim() != "")
                        {
                            var x = weatherresponse.Content;
                            WeatherResult responseObject = JsonConvert.DeserializeObject<WeatherResult>(weatherresponse.Content);

                            return "It's " + (responseObject.main.temp - 273.15d) + " degrees and " + responseObject.weather[0].main + " can be seen in " + responseObject.name;
                        }
                    }
                }
                catch (Exception)
                {
                    return "It's 28 degrees and cloudy in Colombo";
                }

                return "It's 28 degrees and cloudy in Colombo";
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
            #endregion
            
            

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
                #region GENERAL COMMANDS
                //GENERAL QUESTIONS / QUERIES
                "Exit","Exit AISA", "Close AISA", "Close", "Bye", "Good Bye",
                "Good Morning",
                "Good Afternoon",
                "Good Evening",
                "Good Night",
                "Hello",
                "Hi",
                "What's up",
                "What is up",
                "What's going on",
                "What is going on",
                "What's the time",
                "What is the time",
                "What time is it",
                "Time",
                "What's the weather like",
                "What is the weather like",
                "Weather",
                "What's my name",
                "Say my name",
                "What is my name",
                "How's the weather like",
                "How's the weather",
                "I appreciate it",
                "Appreciate it",
                "Thank you",
                "Thanks",
                "What can I say",

                //ABOUT AISA
                "Who are you", "How you were made", "Who made you", "What's your name", "What is your name", "Where are you", "Where do you live", "Who is your father", "What are you", "Tell me about you",

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
                "Go to Wikipedia",

                #endregion
            };
        }
    }
}
