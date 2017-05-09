using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
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
    /// Interaction logic for HelpItem.xaml
    /// </summary>
    public partial class HelpItem : UserControl
    {
        private bool Detailed = false;

        public string[] SubCommands
        {
            get
            {
                string[] array = new string[CommandStack.Children.Count];

                for (int i = 0; i < CommandStack.Children.Count; i++)
                {
                    array[i] = (CommandStack.Children[i] as Label).Content.ToString();
                }

                return array;
            }
            set
            {
                foreach (var item in value)
                {
                    var lbl = new Label();
                    lbl.Content = item;
                    CommandStack.Children.Add(lbl);
                }
            }
        }

        /// <summary>
        /// The image associated with the 
        /// </summary>
        public ImageSource Source
        {
            get
            {
                return Image.Source;
            }
            set
            {
                Image.Source = value;
            }
        }

        /// <summary>
        /// Task the card is referring to
        /// </summary>
        public string Task
        {
            get
            {
                return TaskLabel.Text;
            }
            set
            {
                TaskLabel.Text = value;
            }
        }

        /// <summary>
        /// The command to activate the task
        /// </summary>
        public string Command {
            get
            {
                return CommandLabel.Text;
            }
            set
            {
                CommandLabel.Text = value;
            }
        }


        public HelpItem()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Detailed = !Detailed;

            //Show / hide the details
            if (Detailed)
            {
                var da = new DoubleAnimation(75 + (CommandStack.Margin.Top * 2) + CommandStack.ActualHeight, TimeSpan.FromMilliseconds(500));
                da.EasingFunction = new QuinticEase();
                BeginAnimation(HeightProperty, da);
            }
            else
            {
                var da = new DoubleAnimation(75, TimeSpan.FromMilliseconds(500));
                da.EasingFunction = new QuinticEase();
                BeginAnimation(HeightProperty, da);
            }
        }
    }
}
