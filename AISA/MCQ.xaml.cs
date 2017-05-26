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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AISA
{
    /// <summary>
    /// Interaction logic for MCQ.xaml
    /// </summary>
    public partial class MCQ : Window
    {
        private string Class, PaperIndex;

        public MCQ(string _class, string paper_index)
        {
            InitializeComponent();
            Class = _class;
            PaperIndex = paper_index;
            
            //Set the paper name
            PaperName.Content = Context.currentPaper.name;
        }
    }
}
