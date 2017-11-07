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
            long length = new System.IO.FileInfo(DataSettings.dataFilePath).Length;  
            // add test data to the data file but only if there is no data there already
            if (length == 0)
            {
                //InitializeDataFileCSV.AddTestData();
                InitializeDataFileXML.AddTestData();
                //InitializeDataFileJSON.AddTestData();
            }


            // instantiate the controller
            Controller appContoller = new Controller();
        }
    }
}
