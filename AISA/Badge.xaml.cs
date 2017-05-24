using AISA.Core;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AISA
{
    /// <summary>
    /// Interaction logic for Badge.xaml
    /// </summary>
    public partial class Badge : Window
    {
        public Badge()
        {
            InitializeComponent();

            //Position the window
            WindowStartupLocation = WindowStartupLocation.Manual;
            Left = SystemParameters.WorkArea.Width - Width - 10;
            Top = SystemParameters.WorkArea.Height - Height - 10;

            ViewControllerConnector.MainWindowHide += ShowMe;
        }

        public void ShowMe()
        {
            //Hide the badge
            var da = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));
            da.EasingFunction = new QuinticEase();
            BeginAnimation(OpacityProperty, da);
        }

        public void HideMe()
        {
            //Hide the badge
            var da = new DoubleAnimation(1,0,TimeSpan.FromSeconds(1));
            da.EasingFunction = new QuinticEase();
            BeginAnimation(OpacityProperty, da);

            ViewControllerConnector.MainWindowShow();
        }
    }
}
