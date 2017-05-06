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
            da.EasingFunction = new QuadraticEase();
            BeginAnimation(TopProperty, da);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start AISA Command Recognition
                AISAHandler.Initialize(() =>
                {
                    Speech.Activate();
                },
                () =>
                {
                    Speech.Deactivate();
                });


                AISAHandler.Start();
            }
            catch (AISAHandler.MicrophoneNotWorkingException)
            {
                //Microphone not working
            }

            MessageThread.AddMessage("HEllo World", false);
            MessageThread.AddMessage("HEllo World", false);
            MessageThread.AddMessage("HEllo World", false);
        }

        private void StartRecognition()
        {
            AudioHandler.Start();
            Speech.Activate();
            var rec = new Recognizer(() => { Speech.Deactivate(); });
        }

        private void Speech_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
