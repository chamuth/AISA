using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using AISA_API;
using System.IO;
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

            //Get Whatis data set
            pullWhatis();

            var Badge = new Badge();
            Badge.Show();

            var Window = new MainWindow();
            Window.Show();
            Window.HideWindow();
        }

        /// <summary>
        /// Get the what is data from the AISA Servers
        /// </summary>
        private void pullWhatis()
        {
            try
            {
                var qa = Whatis.GetWhatis();

                //Put the qa to the contextual setting set
                Core.Context.qaDataset = qa;

                var qastring = Newtonsoft.Json.JsonConvert.SerializeObject(qa);

                // Write the QAString to the saving file
                var data_directory = "data/";
                Directory.CreateDirectory(data_directory);

                // Create the file
                var data_file = data_directory + "whatis.json";
                File.Create(data_file).Close();

                // Write to the file
                File.WriteAllText(data_file, qastring);

            }
            catch (Exception)
            {
                // Error writing to the file   
                // Read from the whatis.json file

                var data_file = "data/whatis.json";

                if (File.Exists(data_file))
                {
                    var file_content = File.ReadAllText(data_file);

                    try
                    {
                        // Set the deserialized text to the contextual setting set
                        Core.Context.qaDataset = Newtonsoft.Json.JsonConvert.DeserializeObject<QADataset>(file_content);
                    }
                    catch (Exception) { }
                }
            }
        }

        /// <summary>
        /// Sets all of the Endpoint variables to their endpoint locations
        /// </summary>
        public static void InitializeEndpoints()
        {
            Scholar.Connector.APIEndpoint = "http://localhost/Scholar/API/";
            Endpoint.EndpointString = "http://localhost/AISA";
        }

    }
}
