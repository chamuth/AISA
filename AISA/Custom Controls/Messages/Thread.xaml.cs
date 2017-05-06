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
using System.Windows.Media.Animation;

namespace AISA.Custom_Controls.Messages
{
    /// <summary>
    /// Interaction logic for Thread.xaml
    /// </summary>
    public partial class Thread : UserControl
    {
        public Thread()
        {
            InitializeComponent();
        }

        public void AddMessage(string Message, bool opposite)
        {
            if (opposite)
            {

            }else
            {
                ThreadContainer.Children.Add(new Message(Message));
            }
        }
    }
}
