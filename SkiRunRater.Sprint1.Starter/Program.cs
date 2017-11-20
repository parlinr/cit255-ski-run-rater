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
            //array for storing repository objects
            SkiRunRepositoryCSV[] csvArray = new SkiRunRepositoryCSV[1];
            SkiRunRepositoryXML_DS[] xmlArray = new SkiRunRepositoryXML_DS[1];
            SkiRunRepositoryJSON[] jsonArray = new SkiRunRepositoryJSON[1];
            //instantiate the correct classes based on the user's choice
            switch (userResponse)
            {
                case "1":
                    DataSettings.dataFilePath = DataSettings.dataFilePathCSV;
                    length = new FileInfo(DataSettings.dataFilePath).Length;
                    // add test data to the data file but only if there is no data there already
                    // and instantiate the right repository
                    if (length == 0)
                    {
                        InitializeDataFileCSV.AddTestData();
                        
                    }
                    
                    break;
                case "2":
                    DataSettings.dataFilePath = DataSettings.dataFilePathXML;
                    length = new FileInfo(DataSettings.dataFilePath).Length;
                    // add test data to the data file but only if there is no data there already
                    // and instantiate the right repository
                    if (length == 0)
                    {
                        InitializeDataFileXML_DS.AddTestData();
                        
                    }
                    
                    break;
                case "3":
                    DataSettings.dataFilePath = DataSettings.dataFilePathJSON;
                    length = new FileInfo(DataSettings.dataFilePath).Length;
                    // add test data to the data file but only if there is no data there already
                    // and instantiate the right repository
                    if (length == 0)
                    {
                        InitializeDataFileJSON.AddTestData();
                        
                    }
                    
                    break;
                default:
                    break;
            }

            
            // instantiate the controller based on persistence choice
            if (DataSettings.dataFilePath == "Data\\Data.csv")
            {
                Controller csvAppController = new Controller(csvArray);
            }
            else if (DataSettings.dataFilePath == "Data\\Data.xml")
            {
                Controller xmlAppContoller = new Controller(xmlArray);
            }
            else if (DataSettings.dataFilePath == "Data\\Data.json")
            {
                Controller jsonAppContoller = new Controller(jsonArray);
            }
            
        }
    }
}
