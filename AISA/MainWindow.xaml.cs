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

namespace AISA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Set Dimensions of the Window
            Height = 600;
            Width = 350;
            Left = SystemParameters.FullPrimaryScreenWidth - Width;
            Top = SystemParameters.FullPrimaryScreenHeight;

            //Animate from Bottom
            var da = new DoubleAnimation(SystemParameters.FullPrimaryScreenHeight, SystemParameters.FullPrimaryScreenHeight - Height, TimeSpan.FromSeconds(1));
            da.EasingFunction = new QuinticEase();
            da.BeginTime = TimeSpan.FromSeconds(2);
            BeginAnimation(TopProperty, da);

            //Set the greeting text
            HelloUser.Content = Greetings.Greet();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Start AISA Command Recognition
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

            AISAHandler.Start();

            //Set advanced result controllers
            ViewControllerConnector.Connect += ConnectionHandler;
            ViewControllerConnector.None += NoneHandler;
            ViewControllerConnector.ChangeHypothesis += ChangeHypothesisHandler;
        }

        private void ChangeHypothesisHandler(string obj)
        {
            Hypothesis.Content = obj; // Change the hypothesis
        }

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
            var sa = this.FindResource("ResultsAnimation") as Storyboard;
            sa.Begin();

            q_label.Content = "\"" + Q + "\"";
            a_label.Text = A;
        }

        private void StartRecognition()
        {
            AudioHandler.Start();
            Speech.Activate();

            var rec = new Recognizer(() => { Speech.Deactivate(); }, HandleResult);
        }

        private void SpeechClicked()
        {
            StartRecognition();
        }

        private void linkContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(LinkURL.Content.ToString());
        }
    }
}
