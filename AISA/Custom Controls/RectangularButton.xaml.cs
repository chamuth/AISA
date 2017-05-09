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
    /// Interaction logic for RectangularButton.xaml
    /// </summary>
    public partial class RectangularButton : UserControl
    {
        private bool clicked = false;
        private Color _backgroundColor;

        /// <summary>
        /// Color of the background of the button
        /// </summary>
        public Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                Background = new SolidColorBrush(value);
                _backgroundColor = value;
            }
        }

        /// <summary>
        /// Image source for the button
        /// </summary>
        public ImageSource Source
        {
            get
            {
                return ImagePlaceholder.Source;
            }
            set
            {
                ImagePlaceholder.Source = value;
            }
        } 

        /// <summary>
        /// Event rises when the user clicks on the button
        /// </summary>
        public Action Clicked { get; set; }

        public RectangularButton()
        {
            InitializeComponent();
            ImagePlaceholder.Source = Source;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            clicked = true;
            var da = new DoubleAnimation(1, 0.5, TimeSpan.FromMilliseconds(250));
            this.BeginAnimation(OpacityProperty, da);
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (clicked && Clicked != null) Clicked();

            clicked = false;

            // Start the Opacity Animation
            var da = new DoubleAnimation(0.5, 1, TimeSpan.FromMilliseconds(250));
            this.BeginAnimation(OpacityProperty, da);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            // Get the opaqueness back
            var da = new DoubleAnimation(1, TimeSpan.FromMilliseconds(250));
            BeginAnimation(OpacityProperty, da);
        }
    }
}
