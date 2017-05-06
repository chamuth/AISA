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

namespace AISA.Custom_Controls.Messages
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class Message : UserControl
    {
        public Message(string message = "")
        {
            InitializeComponent();

      
            if (message != "")
            MessageContent.Text = message;

            Height = MessageContent.Height;
            Width = MessageContent.Width;
        }
    }
}
