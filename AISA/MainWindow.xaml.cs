using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using AISA.SpeechRecognition;
using System.Windows.Media.Animation;
using AISA.Core;
using System.Diagnostics;
using System.Speech.Synthesis;
using System.Threading;

namespace AISA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Creates a new MainWindow object
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //Set Dimensions of the Window
            Height = 600;
            Width = 350;
            Left = SystemParameters.FullPrimaryScreenWidth - Width;
            Top = SystemParameters.FullPrimaryScreenHeight;

            //Animate from Bottom
            var da = new DoubleAnimation(SystemParameters.FullPrimaryScreenHeight, SystemParameters.WorkArea.Height - Height, TimeSpan.FromSeconds(1));
            da.EasingFunction = new QuinticEase();
            da.BeginTime = TimeSpan.FromSeconds(2);
            BeginAnimation(TopProperty, da);

            //Set the greeting text
            HelloUser.Content = Greetings.Greet();

            //Attach the exiting code to the viewControllerConnector
            ViewControllerConnector.Exit += ExitAISA;

            //Initialize the OpenSpeak Engine
            OpenSpeak.Init();

            //Change the color of the Animating Lines according to Windows Accent Color.
            grid1.Background = SystemParameters.WindowGlassBrush;
            grid2.Background = SystemParameters.WindowGlassBrush;
            grid3.Background = SystemParameters.WindowGlassBrush;
            grid4.Background = SystemParameters.WindowGlassBrush;

            //Color the extraContent Topics
            Topic1.Foreground = SystemParameters.WindowGlassBrush;
            Topic2.Foreground = SystemParameters.WindowGlassBrush;
            Topic3.Foreground = SystemParameters.WindowGlassBrush;

            //Get the tips from the server
            var threadstart = new ThreadStart(() =>
           {
               try
               {
                   var healthtip = AISA_API.Tips.Get(new AISA_API.HealthTip()).tip;
                   var weathertip = AISA_API.Tips.Get(new AISA_API.WeatherTip()).tip;
                   var studytip = AISA_API.Tips.Get(new AISA_API.StudyTip()).tip;

                   Application.Current.Dispatcher.Invoke(() =>
                  {
                      health_tip.Text = healthtip;
                      weather_tip.Text = weathertip;
                      study_tip.Text = studytip;
                  });
               }
               catch (Exception)
               {

               }
           });
            var thread = new Thread(threadstart);
            thread.Start();

            //Add the news items to the list in another thread
            var newsthreadstart = new ThreadStart(
                () =>
                {
                    try
                    {
                        var newsItems = AISA_API.News.GetAllNews();

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            foreach (var newsItem in newsItems.news)
                            {
                                //Add them to the newsContainer
                                var maingrid = new Grid();
                                maingrid.Cursor = Cursors.Hand;
                                maingrid.MouseUp += (a, b) =>
                                {
                                    Process.Start(newsItem.url);
                                };

                                maingrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
                                maingrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200, GridUnitType.Star) });

                                var newsImage = new Image();
                                newsImage.Source = new BitmapImage(new Uri(newsItem.thumbnail));
                                newsImage.Margin = new Thickness(0, 0, 10, 0);


                                var stackPanel = new StackPanel();

                                var newstitle = new TextBlock();
                                newstitle.Text = newsItem.title;
                                newstitle.TextTrimming = TextTrimming.CharacterEllipsis;
                                newstitle.Foreground = new SolidColorBrush(Colors.White);

                                var newsdescription = new TextBlock();
                                newsdescription.Text = newsItem.description;
                                newsdescription.TextTrimming = TextTrimming.CharacterEllipsis;
                                newsdescription.Foreground = new SolidColorBrush(Color.FromRgb(170, 170, 170));

                                stackPanel.Children.Add(newstitle);
                                stackPanel.Children.Add(newsdescription);

                                stackPanel.VerticalAlignment = VerticalAlignment.Center;

                                maingrid.Children.Add(newsImage);
                                maingrid.Children.Add(stackPanel);

                                Grid.SetColumn(newsImage, 0);
                                Grid.SetColumn(stackPanel, 1);

                                maingrid.Margin = new Thickness(0, 0, 0, 15);
                                NewsContainer.Children.Add(maingrid);
                            }
                        });
                    }
                    catch (Exception)
                    {
                        // Error occurred while getting news information
                        Application.Current.Dispatcher.Invoke(() =>
                       {
                           var errorContainer = new Grid();
                           //Add the column definitions to this grid
                           errorContainer.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2,GridUnitType.Pixel) });
                           errorContainer.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(248, GridUnitType.Star) });

                           //Create the before of the label
                           var LabelBefore = new Grid();
                           LabelBefore.Background = SystemParameters.WindowGlassBrush;
                        
                           var errorlbl = new Label();
                           errorlbl.Content = "I'm having a trouble connecting to our servers.";
                           errorlbl.Foreground = new SolidColorBrush(Colors.White);
                           errorlbl.Margin = new Thickness(5, 0, 0, 0);

                           errorContainer.Children.Add(errorlbl);
                           errorContainer.Children.Add(LabelBefore);

                           Grid.SetColumn(errorlbl, 1);
                           Grid.SetColumn(LabelBefore, 0);

                           NewsContainer.Children.Add(errorContainer);
                       }
                       );
                    }
                });

            var newsThread = new Thread(newsthreadstart);
            newsThread.Start();
        }

        private bool Maximized = false;

        /// <summary>
        /// Occurs when the user clicks the maximize button
        /// </summary>
        private void Maximize()
        {
            Maximized = !Maximized;

            if (!Maximized)
            {
                //TODO:Implement MainWindow Maximization Abilities.
                //Animate from Bottom
                var da = new DoubleAnimation(SystemParameters.FullPrimaryScreenHeight, SystemParameters.WorkArea.Height - Height, TimeSpan.FromSeconds(1));
                da.EasingFunction = new QuinticEase();
                BeginAnimation(TopProperty, da);

                var wi = new DoubleAnimation(350, TimeSpan.FromSeconds(1));
                var hi = new DoubleAnimation(600, TimeSpan.FromSeconds(1));
                da.EasingFunction = new QuinticEase();
                wi.EasingFunction = new QuinticEase();
                hi.EasingFunction = new QuinticEase();

                wi.BeginTime = TimeSpan.FromSeconds(1);
                hi.BeginTime = TimeSpan.FromSeconds(1);

                BeginAnimation(WidthProperty, wi); BeginAnimation(HeightProperty, hi);

                //Set Dimensions of the Window
                var xa = new DoubleAnimation(SystemParameters.FullPrimaryScreenWidth - 350, TimeSpan.FromSeconds(1));
                var ya = new DoubleAnimation(SystemParameters.FullPrimaryScreenHeight - 600, TimeSpan.FromSeconds(1));

                //Begin the top and left animation
                BeginAnimation(LeftProperty, xa);
                BeginAnimation(TopProperty, ya);
            }
            else
            {
                //Maximize the window
                var da = new DoubleAnimation(0, TimeSpan.FromSeconds(1));
                BeginAnimation(TopProperty, da); BeginAnimation(LeftProperty, da);
                var wi = new DoubleAnimation(SystemParameters.PrimaryScreenWidth, TimeSpan.FromSeconds(1));
                var hi = new DoubleAnimation(SystemParameters.PrimaryScreenHeight, TimeSpan.FromSeconds(1));
                da.EasingFunction = new QuinticEase();
                wi.EasingFunction = new QuinticEase();
                hi.EasingFunction = new QuinticEase();

                BeginAnimation(WidthProperty, wi); BeginAnimation(HeightProperty, hi);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Start AISA Command Recognition
            try
            {
                AISAHandler.Initialize(() =>
                {
                    Spinner.Opacity = 1d;
                    Speech.Activate();
                    AskSheet.Visibility = Visibility.Hidden;
                    Spinner.Visibility = Visibility.Visible;
                    Hypothesis.Visibility = Visibility.Visible;
                },
                () =>
                {
                    Speech.Deactivate();
                }, HandleResult);

            }
            catch (Exception) { }

            AISAHandler.Start();

            //Set advanced result controllers
            ViewControllerConnector.Connect += ConnectionHandler;
            ViewControllerConnector.None += NoneHandler;
            ViewControllerConnector.ChangeHypothesis += ChangeHypothesisHandler;
            ViewControllerConnector.AsyncResult += AsyncResultChanged;
            ViewControllerConnector.MainWindowShow += ShowWindow;
            ViewControllerConnector.startPaper += StartPaper;
            ViewControllerConnector.Opaque += Opaque;
        }

        public void Opaque()
        {
            var da = new DoubleAnimation(1, TimeSpan.FromSeconds(1));
            da.EasingFunction = new QuinticEase();

            //Set the opacity of the MainWindows to 1 in 1 second
            BeginAnimation(OpacityProperty, da);
        }

        public void StartPaper(string _class, string paper_index)
        {
            //Pause the AISA Handler 
            AISAHandler.Pause();
            ViewControllerConnector.PaperStarted = true;

            var da = new DoubleAnimation(0.5, TimeSpan.FromSeconds(1));
            da.EasingFunction = new QuinticEase();
            
            //Set the opacity of the MainWindows to 0.5 in 1 second
            BeginAnimation(OpacityProperty, da);            
            
            //Start a new MCQ
            var mcq = new MCQ(_class, paper_index);
            mcq.Show();
        }

        /// <summary>
        /// This occurs on asynchronous basis
        /// </summary>
        /// <param name="Q"></param>
        /// <param name="A"></param>
        private void AsyncResultChanged(string Q, string A)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (A.StartsWith("SUDO:") == false)
                {

                    //Play results audio
                    AudioHandler.Results();

                    //Hide the spinner
                    Spinner.Hide();

                    //Hide the hypothesis
                    var da = new DoubleAnimation(0, TimeSpan.FromMilliseconds(500));
                    Hypothesis.BeginAnimation(OpacityProperty, da);

                    ResultSheet.Visibility = Visibility.Visible;
                    var sa = FindResource("ResultsAnimation") as Storyboard;
                    sa.Begin();

                    //Speak the answer
                    OpenSpeak.Speak(A);

                    q_label.Content = "\"" + Q + "\"";
                    a_label.Text = A;
                }
                else
                {
                    //Play results audio
                    AudioHandler.Results();

                    //Hide the spinner
                    Spinner.Hide();

                    //Hide the hypothesis
                    var da = new DoubleAnimation(0, TimeSpan.FromMilliseconds(500));
                    Hypothesis.BeginAnimation(OpacityProperty, da);

                    ResultSheet.Visibility = Visibility.Visible;
                    var sa = FindResource("ResultsAnimation") as Storyboard;
                    sa.Begin();

                    //Speak the answer
                    OpenSpeak.Speak(A.Replace("SUDO:", ""));

                    q_label.Content = "\"" + Q + "\"";
                    a_label.Text = "Here's what I've got";
                }
            });
        }


        /// <summary>
        /// Fired when the Recognizer has released a hypothesis of the word
        /// </summary>
        /// <param name="obj">Hypothesis as a string</param>
        private void ChangeHypothesisHandler(string obj)
        {
            Hypothesis.Content = obj; // Change the hypothesis
        }

        /// <summary>
        /// Fired when user has called a command with no link
        /// </summary>
        private void NoneHandler()
        {
            //Disappear the linkContainer
            var da = new DoubleAnimation(0, TimeSpan.FromSeconds(1));
            da.EasingFunction = new QuinticEase();
            linkContainer.BeginAnimation(OpacityProperty, da);

            book1.BeginAnimation(OpacityProperty, da);
            book2.BeginAnimation(OpacityProperty, da);
            book3.BeginAnimation(OpacityProperty, da);
        }

        private void ConnectionHandler(ViewControllerConnector.ConnectionMethod method, string[] args)
        {
            switch (method)
            {
                case ViewControllerConnector.ConnectionMethod.URL:
                    LinkName.Content = args[0]; // Set the name of the link
                    LinkURL.Content = args[1]; // Set the URL of the link

                    var da = new DoubleAnimation(1, TimeSpan.FromSeconds(1));
                    da.EasingFunction = new QuinticEase();
                    linkContainer.BeginAnimation(OpacityProperty, da);
                    break;
                case ViewControllerConnector.ConnectionMethod.Book:
                    book1Name.Content = args[0];
                    book1Author.Content = args[1];
                    book2Name.Content = args[2];
                    book2Author.Content = args[3];
                    book3Name.Content = args[4];
                    book3Author.Content = args[5];

                    //Set the images
                    book1Image.Source = new BitmapImage(new Uri(args[6]));
                    book2Image.Source = new BitmapImage(new Uri(args[7]));
                    book3Image.Source = new BitmapImage(new Uri(args[8]));

                    //Set the links to the book stores
                    book1link = args[9];
                    book2link = args[10];
                    book3link = args[11];

                    //Animate the 3 books
                    var da2 = new DoubleAnimation(1, TimeSpan.FromSeconds(1));
                    da2.EasingFunction = new QuinticEase();

                    book1.BeginAnimation(OpacityProperty, da2);
                    da2.BeginTime = TimeSpan.FromSeconds(1);
                    book2.BeginAnimation(OpacityProperty, da2);
                    da2.BeginTime = TimeSpan.FromSeconds(2);
                    book3.BeginAnimation(OpacityProperty, da2);

                    break;
            }
        }

        /// <summary>
        /// Occurs when the result has come back to MainWindow
        /// </summary>
        /// <param name="Q">The Question / Query asked by the user</param>
        /// <param name="A">The result for it</param>
        private void HandleResult(string Q, string A, bool _async = false)
        {
            if (!_async)
            {
                //Hide the spinner
                Spinner.Hide();

                //Hide the hypothesis
                var da = new DoubleAnimation(0, TimeSpan.FromMilliseconds(500));
                Hypothesis.BeginAnimation(OpacityProperty, da);

                ResultSheet.Visibility = Visibility.Visible;
                var sa = FindResource("ResultsAnimation") as Storyboard;
                sa.Begin();

                q_label.Content = "\"" + Q + "\"";
                a_label.Text = A;
            }
        }

        /// <summary>
        /// Fades the GetHelp object in
        /// </summary>
        private void Help()
        {
            if (!ViewControllerConnector.StartedCommandRecognition)
            {
                //Stop AISA recognition
                AISAHandler.Pause();

                //Set the visibility to true
                GetHelp.Visibility = Visibility.Visible;
                GetHelp.Opacity = 0;

                //Animate the fading in of the object
                var da = new DoubleAnimation(1, TimeSpan.FromMilliseconds(500));
                da.EasingFunction = new QuinticEase();
                GetHelp.BeginAnimation(OpacityProperty, da);
            }
        }

        /// <summary>
        /// Fades the GetHelp object out
        /// </summary>
        private void CloseHelp()
        {
            //Start the AISA recognition
            AISAHandler.Start();

            //Animate the fading out of the object
            var da = new DoubleAnimation(0, TimeSpan.FromMilliseconds(500));
            da.EasingFunction = new QuinticEase();
            da.Completed += (a, b) =>
            {
                //Actually inactivate GetHelp object from the view
                GetHelp.Visibility = Visibility.Hidden;
            };

            GetHelp.BeginAnimation(OpacityProperty, da);
        }

        private void StartRecognition()
        {
            AudioHandler.Start();
            Speech.Activate();

            var rec = new Recognizer(() => { Speech.Deactivate(); }, HandleResult);
        }

        /// <summary>
        /// Exits AISA after greeting the user
        /// </summary>
        private void ExitAISA()
        {
            //Stop the AISA Recognition


            //Say the end greeting
            OpenSpeak.Speak(Greetings.End());

            AISAHandler.Pause();

            //Disappear the program
            var da = new DoubleAnimation(SystemParameters.PrimaryScreenHeight, TimeSpan.FromSeconds(1));
            da.EasingFunction = new QuinticEase();
            BeginAnimation(TopProperty, da);

            //Close the program in 2 seconds
            var dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(2);
            dt.Tick += (a, b) =>
            {
                Application.Current.Shutdown();
            };
            dt.Start();
        }

        /// <summary>
        /// What happens when the user clicks on the Speech button himself without saying 'AISA'
        /// </summary>
        private void SpeechClicked()
        {

        }

        private void linkContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (linkContainer.Opacity == 1)
            {
                Process.Start(LinkURL.Content.ToString());
            }
        }

        private string book1link, book2link, book3link;

        public void window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HideWindow();
        }

        public void HideWindow()
        {
            //Disappear the program
            var da = new DoubleAnimation(SystemParameters.PrimaryScreenHeight, TimeSpan.FromMilliseconds(500));
            da.EasingFunction = new QuinticEase();
            BeginAnimation(TopProperty, da);

            AISAHandler.Pause();

            ViewControllerConnector.MainWindowHide();
        }

        public void ShowWindow()
        {
            AISAHandler.Start();

            //Disappear the program
            var da = new DoubleAnimation(SystemParameters.WorkArea.Height - Height, TimeSpan.FromSeconds(1));
            da.EasingFunction = new QuinticEase();
            BeginAnimation(TopProperty, da);

            ResultSheet.Visibility = Visibility.Hidden;
            AskSheet.Visibility = Visibility.Visible;

            //Start that thingy animation
            var sa = FindResource("AskSheetToUp") as Storyboard;
            sa.Begin();
        }

        private void book1MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process.Start(book1link);
            }
            catch (Exception) { }
        }

        private void book2MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process.Start(book2link);
            }
            catch (Exception) { }
        }

        private void book3MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process.Start(book3link);
            }
            catch (Exception) { }
        }
    }
}
