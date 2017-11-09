using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiRunRater;

namespace SkiRunRater
{
    class Program
    {
        static void Main(string[] args)
        {
            //user picks persistence form
            long length = -1;
            string userResponse = ConsoleView.DisplayChoosePersistence();
            //instantiate the correct classes based on the user's choice
            switch (userResponse)
            {
                case "1":
                    DataSettings.dataFilePath = DataSettings.dataFilePathCSV;
                    length = new FileInfo(DataSettings.dataFilePath).Length;
                    // add test data to the data file but only if there is no data there already
                    if (length == 0)
                    {
                        InitializeDataFileCSV.AddTestData();
                    }
                    break;
                case "2":
                    DataSettings.dataFilePath = DataSettings.dataFilePathXML;
                    length = new FileInfo(DataSettings.dataFilePath).Length;
                    // add test data to the data file but only if there is no data there already
                    if (length == 0)
                    {
                        InitializeDataFileXML.AddTestData();
                    }
                    break;
                case "3":
                    DataSettings.dataFilePath = DataSettings.dataFilePathJSON;
                    length = new FileInfo(DataSettings.dataFilePath).Length;
                    // add test data to the data file but only if there is no data there already
                    if (length == 0)
                    {
                        InitializeDataFileJSON.AddTestData();
                    }
                    break;
                default:
                    break;
            }

            
            // instantiate the controller
            Controller appContoller = new Controller();
        }
    }
}
