using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SkiRunRater
{
    public class InitializeDataFileJSON
    {

        public static void AddTestData()
        {
            List<SkiRun> skiRuns = new List<SkiRun>();

            // initialize the IList of high scores - note: no instantiation for an interface
            skiRuns.Add(new SkiRun() { ID = 1, Name = "Buck", Vertical = 325 });
            skiRuns.Add(new SkiRun() { ID = 2, Name = "Buckaroo", Vertical = 325 });
            skiRuns.Add(new SkiRun() { ID = 3, Name = "Hoot Owl", Vertical = 325 });
            skiRuns.Add(new SkiRun() { ID = 4, Name = "Shelburg's Chute", Vertical = 325 });

            WriteAllSkiRuns(skiRuns, DataSettings.dataFilePath);
        }

        /// <summary>
        /// method to write all ski run info to the data file
        /// </summary>
        /// <param name="skiRuns">list of ski run info</param>
        /// <param name="dataFilePath">path to the data file</param>
        public static void WriteAllSkiRuns(List<SkiRun> skiRuns, string dataFilePath)
        {
            
            StreamWriter sWriter = new StreamWriter(DataSettings.dataFilePath, false);
            string jsonText = JsonConvert.SerializeObject(skiRuns, Formatting.Indented);
            using (sWriter)
            {
                sWriter.Write(jsonText);
            }
        }
    }
}
