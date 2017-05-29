using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Reflection;

namespace AISA
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //if (AISA.Properties.Settings.Default.setStartup == false)
            //    System.Diagnostics.Process.Start("SetStartup.exe");

            //Initialize the endpoints
            InitializeEndpoints();

            var Badge = new Badge();
            Badge.Show();

            var Window = new MainWindow();
            Window.Show();
            Window.HideWindow();
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
