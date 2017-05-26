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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AISA.Custom_Controls
{
    /// <summary>
    /// Interaction logic for Speech.xaml
    /// </summary>
    public partial class Collapse : UserControl
    {
        public Action Clicked { get; set; }

        public Collapse()
        {
            InitializeComponent();
        }

        private bool clicked = false;

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ellipse.Fill = new SolidColorBrush(Color.FromArgb(200, 0, 150, 255));
            clicked = true;
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ellipse.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 150, 255));
            if (clicked)
            {
                Clicked();
            }
        }
        public void Activate()
        {
            //Start Storyboard
            var visualDrop = this.FindResource("VisualDropAnimation") as Storyboard;
            visualDrop.Begin();

            //Make the topDrawer visible
            var da = new DoubleAnimation(0, 65, TimeSpan.FromMilliseconds(500));
            topDrawer.BeginAnimation(WidthProperty, da);
            topDrawer.BeginAnimation(HeightProperty, da);

            var opa = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(500));
            topDrawer.BeginAnimation(OpacityProperty, opa);
        }

        public void Deactivate()
        {
            //Stop the Storyboard
            var visualDrop = this.FindResource("VisualDropAnimation") as Storyboard;
            visualDrop.Stop();

            //Add a smoothing animation
            var da = new DoubleAnimation(50, TimeSpan.FromMilliseconds(500));
            ellipse.BeginAnimation(WidthProperty, da);
            ellipse.BeginAnimation(HeightProperty, da);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ellipse.Fill = SystemParameters.WindowGlassBrush;
        }
    }
}
