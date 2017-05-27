using AISA.DataEntities;
using AISA.Core;
using Newtonsoft.Json;
using RestSharp;
using Scholar;

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
using System.Threading;
using System.Windows.Threading;
using AISA_API;

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
            else if (input.Contains("Go to GitHub"))
            {
                System.Diagnostics.Process.Start("http://www.github.com");
                return "Opening web browser";
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
                if (Context.LastURL != null)
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
                var seperatethread = new ThreadStart(() =>
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

                                ViewControllerConnector.AsyncResult(input, "It's " + (responseObject.main.temp - 273.15d) + " degrees and " + responseObject.weather[0].main + " can be seen in " + responseObject.name);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        ViewControllerConnector.AsyncResult(input, "It's 28 degrees and cloudy in Colombo");
                    }
                });

                //Start a separate thread from the above
                var thread = new Thread(seperatethread);
                thread.Start();

                return "ASYNC:";
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

            #region CLASS SEARCHING MODULE
            //array(  "Agriculture", "Architecture", "Biological Science", "Biomedical Science", "Business", "Communication", "Computer Science", "Computer Engineering", "Education", "Engineering", "Mathematics", "Media and Telecommunication", "Medical and Health", "Physical Science", "Psychology", "Science", "Other" );
            else if (input.Contains("a class") || input.Contains("a tuition class"))
            {
                return random(new string[]
                {
                    "Please tell me which category", "Which type of a class you're looking for?", "What kind of a class"
                });
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("agriculture")))
            {
                return FindAClass(0);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("architecture")))
            {
                return FindAClass(1);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("biology") || input.ToLower().Contains("bio")))
            {
                return FindAClass(2);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("medical")))
            {
                return FindAClass(3);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("business") || input.ToLower().Contains("accounting") || input.ToLower().Contains("commerce") || input.ToLower().Contains("econ")))
            {
                return FindAClass(4);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("communication technology")))
            {
                return FindAClass(5);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("computer science") || input.ToLower().Contains("information technology") || input.ToLower().Contains("it") || input.ToLower().Contains("ict")))
            {
                return FindAClass(6);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("software engineering") || input.ToLower().Contains("programming")))
            {
                return FindAClass(7);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("engineering") || input.ToLower().Contains("electrical engineering")))
            {
                return FindAClass(9);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("maths") || input.ToLower().Contains("mathematics")))
            {
                return FindAClass(10);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("media")))
            {
                return FindAClass(11);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("medical")))
            {
                return FindAClass(12);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("physics") || input.ToLower().Contains("physical science")))
            {
                return FindAClass(13);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("psychology")))
            {
                return FindAClass(14);
            }
            else if ((input.Contains("class") || input.Contains("tuition class")) && (input.ToLower().Contains("science") || input.ToLower().Contains("chemistry")))
            {
                return FindAClass(15);
            }
            #endregion

            #region BOOK SEARCH MODULE
            else if (input.Contains("science") && input.Contains("book"))
            {
                return searchBooks("science");
            }
            else if ((input.Contains("mathematics") || input.Contains("maths")) && input.Contains("book"))
            {
                return searchBooks("mathematics");
            }
            else if (input.Contains("nature") && input.Contains("book"))
            {
                return searchBooks("nature");
            }
            else if (input.Contains("geography") && input.Contains("book"))
            {
                return searchBooks("geography");
            }
            else if (input.Contains("astronomy") && input.Contains("book"))
            {
                return searchBooks("astronomy");
            }
            else if (input.Contains("algebra") && input.Contains("book"))
            {
                return searchBooks("algebra");
            }
            else if (input.Contains("trigonometry") && input.Contains("book"))
            {
                return searchBooks("trigonometry");
            }
            else if ((input.Contains("information technology") || input.Contains("IT")) && input.Contains("book"))
            {
                return searchBooks("information technology");
            }
            else if (input.Contains("astronomy") && input.Contains("book"))
            {
                return searchBooks("astronomy");
            }
            else if (input.Contains("programming") && input.Contains("book"))
            {
                return searchBooks("programming");
            }
            #endregion
            else if (input.ToLower().Contains("buy that book"))
            {
                if (Context.BuyThatBook == null)
                {
                    return "Which book?";
                }
                else
                {
                    return "Please tell me, first, second or third book?";
                }
            }
            else if ((input.ToLower().Contains("first") || input.ToLower().Contains("second") || input.ToLower().Contains("third")) && input.ToLower().Contains("buy"))
            {
                if (Context.BuyThatBook != null)
                {
                    if (input.ToLower().Contains("first"))
                    {
                        System.Diagnostics.Process.Start("https://www.amazon.com/s/ref=nb_sb_noss_2?field-keywords=" + Context.BuyThatBook[0]);
                    }
                    else if (input.ToLower().Contains("second"))
                    {
                        System.Diagnostics.Process.Start("https://www.amazon.com/s/ref=nb_sb_noss_2?field-keywords=" + Context.BuyThatBook[1]);
                    }
                    else if (input.ToLower().Contains("third"))
                    {
                        System.Diagnostics.Process.Start("https://www.amazon.com/s/ref=nb_sb_noss_2?field-keywords=" + Context.BuyThatBook[2]);
                    }

                    return "Opening web browser";
                }
            }
            else if ((input.ToLower().Contains("first") || input.ToLower().Contains("second") || input.ToLower().Contains("third")) && input.ToLower().Contains("book"))
            {
                if (Context.Previous.ToLower().Contains("buy that book"))
                {
                    System.Diagnostics.Process.Start("https://www.amazon.com/s/ref=nb_sb_noss_2?field-keywords=" + Context.BuyThatBook[0]);
                    return "Opening web browser";
                }
                else
                {
                    return "Which book?";
                }
            }
            else if (input.Contains("a book"))
            {
                //User is searching for a book, but does not specify a specific category of book
                return random(new string[]
                {
                    "What type of a book you need?", "What kind of a book you need?", "Please tell me a category to search for", "What type of book?"
                });
            }

            //PAPERS
            else if (input.ToLower().Contains("let's write a paper") || input.ToLower().Contains("do i have any papers") || input.ToLower().Contains("do i have any papers to write"))
            {
                //Check for all of the classes the student attend

                var threadstart = new ThreadStart(() =>
                {

                    var classes = Student.GetClasses(Properties.Settings.Default.scholarUsername);

                    foreach (var _class in classes.classes)
                    {
                        var papers = Class.GetMCQPapers(int.Parse(_class));
                        var indexes = papers.papers;

                        indexes = indexes.Where((i) =>
                        {
                            if (Context.previousPaper != null)
                            {
                                if (_class == Context.previousPaper[0])
                                {
                                    if (indexes.Length > 1)
                                    {
                                        if (indexes[1] == i)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            if (Paper.VerifyWritten(int.Parse(_class), i, Properties.Settings.Default.scholarUsername).written == 1)
                                            {
                                                ViewControllerConnector.AsyncResult(Context.Current, "SUDO:The paper was written by you");
                                                return false;
                                            }
                                            else
                                            {
                                                return true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Paper.VerifyWritten(int.Parse(_class), i, Properties.Settings.Default.scholarUsername).written == 1)
                                        {
                                            ViewControllerConnector.AsyncResult(Context.Current, "SUDO:The paper was written by you");
                                            return false;
                                        }
                                        else
                                        {
                                            return true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (Paper.VerifyWritten(int.Parse(_class), i, Properties.Settings.Default.scholarUsername).written == 1)
                                    {
                                        ViewControllerConnector.AsyncResult(Context.Current, "SUDO:The paper was written by you");
                                        return false;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                            }
                            else
                            {
                                if (Paper.VerifyWritten(int.Parse(_class), i, Properties.Settings.Default.scholarUsername).written == 1)
                                {
                                    ViewControllerConnector.AsyncResult(Context.Current, "SUDO:The paper was written by you");
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }

                        }).Select(i => i).ToArray();

                        //Get the first paper's information
                        if (indexes == null || indexes.Length == 0)
                        {
                            ViewControllerConnector.AsyncResult(Context.Current, "You don't have any paper");
                        }
                        else
                        {
                            var thepaper = Class.GetMCQPaper(int.Parse(_class), indexes[0], Properties.Settings.Default.scholarUsername, Properties.Settings.Default.scholarPassword);
                            var currentname = thepaper.name;

                            //Get the class information from the server
                            var classinfo = Class.GetInformation(int.Parse(_class));

                            //Setup the paper that I've been working with
                            Context.previousPaper = new string[]
                            {
                                _class, indexes[0].ToString()
                            };

                            ViewControllerConnector.AsyncResult(Context.Current, "SUDO:You have a paper, " + currentname + " from " + classinfo.information.name);
                            break;
                        }

                    }
                });

                var thread = new Thread(threadstart);
                thread.Start();

                return "ASYNC:";
            }
            else if (input.ToLower().Contains("let's write it") || input.ToLower().Contains("let's write that") || input.ToLower().Contains("let's write") || input.ToLower().Contains("let's start"))
            {
                if (Context.previousPaper != null)
                {
                    //Get details about the paper
                    Context.currentPaper = Class.GetMCQPaper(int.Parse(Context.previousPaper[0]), int.Parse(Context.previousPaper[1]), Properties.Settings.Default.scholarUsername, Properties.Settings.Default.scholarPassword);

                    return "SUDO:Started the paper";
                }
                else
                {
                    return "Which paper";
                }
            }

            #region FACTS
            else if (input.ToLower().Contains("science fact"))
            {
                //Get a scientific fact from AISA Server
                var threadstart = new ThreadStart(() =>
                {
                    var fact = FactualEngine.GetFact(new ScienceFactualEngineHelper());
                    var output = fact.fact.ToString();

                    ViewControllerConnector.AsyncResult(Context.Current, output);
                });

                var thread = new Thread(threadstart);
                thread.Start();
                
                return "ASYNC:";
            }
            else if (input.ToLower().Contains("mathematics fact") || input.ToLower().Contains("maths fact")) 
            {
                //Get a mathematical fact from the AISA Server
                var threadstart = new ThreadStart(() =>
                {
                    var fact = FactualEngine.GetFact(new MathematicsFactualEngineHelper());
                    var output = fact.fact.ToString();

                    ViewControllerConnector.AsyncResult(Context.Current, output);
                });

                var thread = new Thread(threadstart);
                thread.Start();

                return "ASYNC:";
            }

            
            #endregion

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
        /// Finds a class using the Scholar Search Engine
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static string FindAClass(int category)
        {
            var threadstart = new ThreadStart(() =>
            {
                string returner = "SUDO: I found ";
                
                var username = Properties.Settings.Default.scholarUsername;
                var password = Properties.Settings.Default.scholarPassword;

                //Verify the student username and the password
                if (Student.VerifyStudent(username, password).error == 0)
                {
                    //User exists and the password is correct

                    //Search the class by category
                    var search1 = Class.SearchByGrade(Properties.Settings.Default.studentGrade);
                    var search2 = Class.SearchByCategory(category);

                    IEnumerable<string> complete = null;

                    if (search1.results == null)
                    {
                        complete = search2.results;
                    }
                    else if (search2.results == null)
                    {
                        complete = search1.results;
                    }
                    else
                    {
                        complete = search1.results.Intersect<string>(search2.results);
                    }

                    if (complete != null)
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            if (complete.Count() > i)
                            {
                                var current = complete.ToArray()[i];

                                var searchClass = Class.GetInformation(int.Parse(current));
                                var searchTeacher = Teacher.GetInformation(searchClass.information.teacher);
                                var teachername = searchTeacher.information.firstname + " " + searchTeacher.information.lastname;

                                if (searchClass.error == 0)
                                {
                                    //No error found on the index of the class
                                    if (complete.Count() != 1)
                                    {
                                        if (complete.Count() > 3)
                                        {
                                            if (i != 2)
                                            {
                                                returner += searchClass.information.name + " by " + teachername + ", ";
                                            }
                                            else
                                            {
                                                returner += "and " + searchClass.information.name + " by " + teachername;
                                            }
                                        }
                                        else
                                        {
                                            if (i != complete.Count() - 1)
                                            {
                                                returner += searchClass.information.name + " by " + teachername + ", ";
                                            }
                                            else
                                            {
                                                returner += "and " + searchClass.information.name + " by " + teachername;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        returner = "SUDO:" + searchClass.information.name + " by " + teachername;
                                    }
                                }
                                else
                                {
                                    returner = "Found zero classes";
                                }

                            }
                        }

                    }
                    else
                    {
                        returner = "Found zero classes";
                    }

                }
                else
                {
                    //User does not exist or the password is wrong
                    returner = random(new string[]
                    {
                        "Please make sure your credentials are correct", "Please re-enter your credentials"
                    });
                }

                ViewControllerConnector.AsyncResult(Context.Current, returner);
            });

            var thread = new Thread(threadstart);
            thread.Start();

            return "ASYNC:";
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
            var threadstart = new ThreadStart(() =>
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
                        GoodreadsResponse result = new GoodreadsResponse();

                        using (TextReader xmlreader = new StringReader(restresponse.Content))
                        {
                            try
                            {
                                result = (GoodreadsResponse)serializer.Deserialize(xmlreader);
                            }
                            catch (Exception)
                            {
                                ViewControllerConnector.AsyncResult(Context.Current, "SUDO:" + "Our servers are down, please retry");
                            }
                        }

                        var sending_string = "I found ";
                        var results = random(result.search.results);

                        for (int i = 0; i < 3; i++)
                        {
                            if (i < 2)
                                sending_string += results[i].best_book.title + " by " + results[i].best_book.author.name + ", ";
                            else
                                sending_string += " and " + results[i].best_book.title + " by " + results[i].best_book.author.name;
                        }

                        Context.BuyThatBook = new string[]
                        {
                        results[0].best_book.title,
                        results[1].best_book.title,
                        results[2].best_book.title,
                        };

                        Application.Current.Dispatcher.Invoke(() =>
                        {
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
                        });

                        ViewControllerConnector.AsyncResult(Context.Current, "SUDO:" + sending_string);
                    }
                    catch (IOException)
                    {
                        ViewControllerConnector.AsyncResult(Context.Current, random(new string[]
                        {
                        "Sorry, I'm having some connection issues", "My apologies, make sure you're connected to the Internet", "I cannot connect to my servers, sorry."
                        }));
                    }
                }
                else
                {
                    ViewControllerConnector.AsyncResult(Context.Previous, random(new string[]
                    {
                    "Sorry, I'm having some connection issues", "My apologies, make sure you're connected to the Internet", "I cannot connect to my servers, sorry."
                    }));
                }
            });

            var thread = new Thread(threadstart);
            thread.Start();

            return "ASYNC:";
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
                "Go to GitHub",
                #endregion

                #region STUDENT ORIENTED COMMANDS
                    #region CLASS SEARCH
                    "Find a class", "Find me a class", "Find me a tuition class", "Find a tuition class",
                    "Find me an agriculture class", "Find an agriculture class",
                    "Find me an architecture class", "Find an architecture class",
                    "Find me a Biology class", "Find me a Bio class", "Find a Biology class", "Find a bio class",
                    "Find me a medical science class", "Find a medical science class",
                    "Find me a business class", "Find me an accounting class", "Find me a commerce class", "Find me a econ class",
                    "Find a business class", "Find an accounting class", "Find a commerce class", "Find a econ class",
                    "Find me a communication technology class", "Find a communication technology class",
                    "Find a Computer Science class", "Find me a Computer Science class", "Find me an Information Technology class", "Find an Information Technology class", "Find me an IT class", "Find an IT class", "Find me an ICT class", "Find an ICT class",
                    "Find a Software Engineering class", "Find me a Software Engineering class", "Find me a programming class", "Find a programming class",
                    "Find an Engineering class", "Find me an engineering class", "Find an electrical engineering class", "Find me an electrical engineering class",
                    "Find me a maths class", "Find me a mathematics class", "Find a maths class", "Find a mathematics class",
                    "Find me a media class", "Find a media class",
                    "Find me a medical class" , "Find a medical class",
                    "Find me a physics class", "Find a physics class", "Find a physical science class", "Find me a physical science class",
                    "Find me a psychology class", "Find a psychology class",
                    "Find me a science class", "Find a science class", "Find me a chemistry class", "Find a chemistry class",
                #endregion
                #region PAPERS
                    "Let's write a paper", "Do I have any papers", "Do I have any papers to write",

                    "Let's write it" , "Let's write that", "Let's write that paper", "Let's write", "Let's start",
                #endregion

                #region FACTS
                    "Tell me a science fact", "Show me a science fact", "Science fact", "A science fact",
                    "Tell me a mathematics fact", "Show me  a mathematics fact", "Mathematics fact", "A mathematics fact",
                    "Tell me a maths fact", "Show me a maths fact", "Maths fact", "A maths fact",
                #endregion

                #region SCHOLAR SHPS AND COMPETITIONS
                    "Find me a competition", "Find a competition", "Find me a scholarship", "Find a scholarship", "Competition", "Scholarship",
                #endregion

                #region BOOK SEARCH
                //Finding a book
                "Find me a book", "Find a book", "I want a book",
                    //Science books
                    "Find me a science book", "Find me a book about science", "Find me a mathematics book",
                    "Find me a maths book", "Find me a book about mathematics", "Find me a book about maths",
                    "Find me a book abouSbook", "Find me a book about algebra", "Find me a trigonometry book", "Find me a book about trigonometry", "Find me a book about information technology",
                    "Find me a book about IT", "Find me an IT book", "Find me an information technology book", "Find me a programming book", "Find me a book about programming", "Let's buy that book", "Buy that book", "Buy the first book", "Buy the second book", "Buy the third book", "First book", "Second book", "Third Book"
                    #endregion
                #endregion
            };
        }
    }
}
