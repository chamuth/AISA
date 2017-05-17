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
                //Restore the window
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
            }
            else
            {
                //Maximize the window
                var da = new DoubleAnimation(0, TimeSpan.FromSeconds(1));
                BeginAnimation(TopProperty, da); BeginAnimation(LeftProperty, da);
                var wi = new DoubleAnimation(SystemParameters.FullPrimaryScreenWidth, TimeSpan.FromSeconds(1));
                var hi = new DoubleAnimation(SystemParameters.MaximizedPrimaryScreenHeight, TimeSpan.FromSeconds(1));
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
        }

        private void ConnectionHandler(ViewControllerConnector.ConnectionMethod method, string Title, string URL)
        {
            switch (method)
            {
                case ViewControllerConnector.ConnectionMethod.URL:
                    LinkName.Content = Title; // Set the name of the link
                    LinkURL.Content = URL; // Set the URL of the link

                    var da = new DoubleAnimation(1, TimeSpan.FromSeconds(1));
                    da.EasingFunction = new QuinticEase();
                    linkContainer.BeginAnimation(OpacityProperty, da);
                    break;
            }
        }

        /// <summary>
        /// Occurs when the result has come back to MainWindow
        /// </summary>
        /// <param name="Q">The Question / Query asked by the user</param>
        /// <param name="A">The result for it</param>
        private void HandleResult(string Q, string A)
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
    }
}
