using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AISA
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var Badge = new Badge();
            Badge.Show();

            var Window = new MainWindow();
            Window.Show();
            Window.HideWindow();

            //Initialize the endpoints
            InitializeEndpoints();

            //Get command LEngth
            MessageBox.Show(CommandHandler.GetCommands().Length.ToString());
        }

        /// <summary>
        /// Sets all of the Endpoint variables to their endpoint locations
        /// </summary>
        private void InitializeEndpoints()
        {
            Scholar.Connector.APIEndpoint = "http://localhost/Scholar/API/";
            AISA_API.Endpoint.EndpointString = "http://localhost/AISA";
        }
    }
}
