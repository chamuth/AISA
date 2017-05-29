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
    /// Interaction logic for Spinner.xaml
    /// </summary>
    public partial class Spinner : UserControl
    {
        public Brush SpinnerBrush
        {
            get
            {
                return arc.Fill;
            }
            set
            {
                arc.Fill = value;
                arc.Stroke = value;
            }
        }

        public Spinner()
        {
            InitializeComponent();

            //Color the SpinnerBrush
            SpinnerBrush = SystemParameters.WindowGlassBrush;
        }

        /// <summary>
        /// Fades the Spinner Element out
        /// </summary>
        public void Hide()
        {
            var da = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(500));
            arc.BeginAnimation(OpacityProperty, da);
        }
    }
}
