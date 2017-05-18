using AISA.DataEntities;
using AISA.Core;
using Newtonsoft.Json;
using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Collections;

namespace AISA
{
    /// <summary>
    /// Class handling the commands sent by the Recognizer class. See also <seealso cref="SpeechRecognition.Recognizer"></seealso>
    /// </summary>
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
                ViewControllerConnector.Connect(ViewControllerConnector.ConnectionMethod.URL, new string[] { "Chamuth/AISA - GitHub", "https://github.com/Chamuth/AISA/tree/master" });

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
                ViewControllerConnector.Connect(ViewControllerConnector.ConnectionMethod.URL, new string[] { "Chamuth/AISA - GitHub", "https://github.com/Chamuth/AISA/tree/master" });

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
                ViewControllerConnector.Connect(ViewControllerConnector.ConnectionMethod.URL, new string[] { "Chamuth/AISA - GitHub", "https://github.com/Chamuth/AISA/tree/master" });

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

            #region STUDENT ASSISTANT COMMAND HANDLING
            else if (input.Contains("a class") || input.Contains("a tuition class"))
            {
                if (Properties.Settings.Default.scholarUsername == "")
                {
                    //User is not signed into Scholar
                    return random(new string[]
                    {
                        "Please enter your scholar credentials first", "Please enter your scholar credentials here"
                    });
                }
                else
                {
                    //Verify login
                    var username = Properties.Settings.Default.scholarUsername;
                    var password = Properties.Settings.Default.scholarPassword;

                    var client = new RestClient("localhost/Scholar/api/u=" + username + "&p" + password);
                    var request = new RestRequest(Method.GET);
                    var response = client.Execute(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        try
                        {
                            Student.Verify verify = JsonConvert.DeserializeObject<Student.Verify>(response.Content);

                            switch (verify.error)
                            {
                                case 0:
                                    //User exists and the password is correct

                                    //Start finding the class

                                    break;
                                case 1:
                                    //User does not exist
                                    return random(new string[]
                                    {
                                        "Please make sure your credentials are correct", "Please re-enter your credentials"
                                    });
                                case 2:
                                    //User exist but the password is incorrect
                                    return random(new string[]
                                    {
                                        "Please make sure your password is correct", "Please re-enter your password"
                                    });
                            }
                        }
                        catch (Exception)
                        {
                            return random(new string[]
                    {
                        "Sorry, I'm having some connection issues", "My apologies, make sure you're connected to the Internet", "I cannot connect to my servers, sorry."
                    });
                        }
                    }
                }
            }
            else if (input.Contains("science") && input.Contains("book"))
            {
                //User is searching for a science book
                return searchBooks("science");
            }
            else if ((input.Contains("mathematics") || input.Contains("maths") ) && input.Contains("book") )
            {
                return searchBooks("mathematics");
            }
            else if (input.Contains("nature") && input.Contains("book"))
            {
                return searchBooks("nature");
            }
            else if (input.Contains("a book"))
            {
                //User is searching for a book, but does not specify a specific category of book
                return random(new string[]
                {
                    "What type of a book you need?", "What kind of a book you need?", "Please tell me a category to search for", "What type of book?"
                });
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

        private static GoodreadsResponseSearchWork[] random(GoodreadsResponseSearchWork[] i)
        {
            var rand = new Random().Next(1, i.Length);

            return new GoodreadsResponseSearchWork[]
            {
                i[rand], (rand + 1 < i.Length)?i[rand + 1]:i[rand - 2], (rand - 1 > 0)?i[rand - 1]:i[rand + 2]
            };
        }

        /// <summary>
        /// Quick function to search books
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private static string searchBooks(string v)
        {
            var key = "iBDvQ3HOYOPOaFboICTtrw";
            var restClient = new RestClient("https://www.goodreads.com/search.xml?key=" + key + "&q=" + v);
            var restRequest = new RestRequest(Method.GET);

            var restresponse = restClient.Execute(restRequest);

            if (restresponse.Content != "" && restresponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(GoodreadsResponse));
                    GoodreadsResponse result;
                    //Copy
                    Clipboard.SetText(restresponse.Content);

                    using (TextReader xmlreader = new StringReader(restresponse.Content))
                    {
                        result = (GoodreadsResponse)serializer.Deserialize(xmlreader);
                    }

                    var sending_string = "I found ";
                    var results = random(result.search.results);

                    for (int i = 0; i < 3; i++)
                    {
                        if (i < 2)
                            sending_string += results[i].best_book.title + " by " + results[i].best_book.author.name + ", ";
                        else
                            sending_string += " and " +  results[i].best_book.title + " by " + results[i].best_book.author.name;
                    }

                    Context.BuyThatBook = new string[]
                    {
                        results[0].best_book.title,
                        results[1].best_book.title,
                        results[2].best_book.title,
                    };

                    ViewControllerConnector.Connect(ViewControllerConnector.ConnectionMethod.Book, new string[]
                    {
                        results[0].best_book.title, results[0].best_book.author.name,
                        results[1].best_book.title, results[1].best_book.author.name,
                        results[2].best_book.title, results[2].best_book.author.name,
                        results[0].best_book.small_image_url, results[1].best_book.small_image_url, results[2].best_book.small_image_url,
                        "http://www.amazon.com/s/ref=nb_sb_noss_2?field-keywords=" + results[0].best_book.title,
                        "http://www.amazon.com/s/ref=nb_sb_noss_2?field-keywords=" + results[1].best_book.title,
                        "http://www.amazon.com/s/ref=nb_sb_noss_2?field-keywords=" + results[2].best_book.title,
                    });

                    return "SUDO:" + sending_string;
                }
                catch (IOException)
                {
                    return random(new string[]
                    {
                        "Sorry, I'm having some connection issues", "My apologies, make sure you're connected to the Internet", "I cannot connect to my servers, sorry."
                    });
                }
            }else
            {
                return random(new string[]
                {
                    "Sorry, I'm having some connection issues", "My apologies, make sure you're connected to the Internet", "I cannot connect to my servers, sorry."
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

                #region STUDENT ORIENTED COMMANDS
                    #region CLASS SEARCH
                    "Find a class", "Find me a class", "Find me a tuition class", "Find a tuition class",
                    #endregion

                    #region BOOK SEARCH
                    //Finding a book
                    "Find me a book", "Find a book", "I want a book",
                    //Science books
                    "Find me a science book", "Find me a book about science", "Find me a mathematics book",
                    "Find me a maths book", "Find me a book about mathematics", "Find me a book about maths",
                    "Find me a book about nature"
                    #endregion
                #endregion
            };
        }
    }
}
