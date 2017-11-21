using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;


namespace SkiRunRater
{
    /// <summary>
    /// method to write all ski run information to the date file
    /// </summary>
    public class SkiRunRepositoryJSON : IDisposable, ISkiRunRepository
    {
        #region FIELDS

        private List<SkiRun> _skiRuns;

        #endregion

        #region CONSTRUCTOR

        public SkiRunRepositoryJSON()
        {
            _skiRuns = ReadSkiRunsData(DataSettings.dataFilePath);
        }

        #endregion 

        #region METHODS 

        /// <summary>
        /// method to delete a ski run by ski run ID
        /// </summary>
        /// <param name="ID"></param>
        public void Delete(int ID)
        {
            if (IsSkiRunIDTaken(ID) == true)
            {
                //If the ID already exists...

                //Delete the record with the ID value entered by the user.
                for (int index = 0; index < _skiRuns.Count(); index++)
                {
                    if (_skiRuns[index].ID == ID)
                    {
                        _skiRuns.RemoveAt(index);
                    }
                }

                Save();
            }
            else
            {
                //If the ID does not exist...

                //Throw a new exception.
                throw new ArgumentException($"The ID value of {ID} does not exist.  Please re-ente a new ID.");
            }
        }

        /// <summary>
        /// method to handle the IDisposable interface contract
        /// </summary>
        public void Dispose()
        {
            _skiRuns = null;
        }

        /// <summary>
        /// method to return a list of ski run objects
        /// </summary>
        /// <returns>list of ski run objects</returns>
        public List<SkiRun> SelectAll()
        {
            return _skiRuns;
        }

        /// <summary>
        /// method to return a ski run object given the ID
        /// </summary>
        /// <param name="ID">int ID</param>
        /// <returns>ski run object</returns>
        public SkiRun SelectById(int ID)
        {
            //Variable Declarations.
            SkiRun skiRunDetail = null;

            if (IsSkiRunIDTaken(ID) == true)
            {
                //If the ID value entered exists...

                //Get the ski run information for the ID entered by the user.
                foreach (SkiRun skiRun in _skiRuns)
                {
                    if (skiRun.ID == ID)
                    {
                        skiRunDetail = skiRun;
                        break;
                    }
                }
            }
            else
            {
                //If the ID value entered does not exist...

                //Throw a new exception.
                throw new ArgumentException($"The ID value of {ID} does not exist.  Please re-enter a new ID.");
            }

            return skiRunDetail;
        }

        /// <summary>
        /// method to add a new ski run
        /// </summary>
        /// <param name="skiRun"></param>
        public void Insert(SkiRun skiRun)
        {
            //Variale Declarations.
            string skiRunString;

            skiRunString = skiRun.ID + "," + skiRun.Name + "," + skiRun.Vertical;

            //Check to make sure the ID and Name values have not been taken.
            if (IsSkiRunIDTaken(skiRun.ID) == true)
            {
                throw new ArgumentException($"The ID value of {skiRun.ID} has already been used for another ski run.  Please re-enter the ski run information with a different ID value.");
                //return;
            }

            if (IsSkiRunNameTaken(skiRun.Name) == true)
            {
                throw new ArgumentException($"The name {skiRun.Name} has already been used for another ski run.  Please re-enter the ski run information with a different Name.");
            }


            _skiRuns.Add(skiRun);

            Save();

        }

        

        /// <summary>
        /// method to read all ski run information from the data file and return it as a list of SkiRun objects
        /// </summary>
        /// <param name="dataFilePath">path the data file</param>
        /// <returns>list of SkiRun objects</returns>
        public static List<SkiRun> ReadSkiRunsData(string dataFilePath)
        {
            string jsonText;
            List<SkiRun> skiRuns = new List<SkiRun>();

            //initialize streamreader
            StreamReader sReader = new StreamReader(DataSettings.dataFilePath);

            //read all of the JSON text file into a string
            using (sReader)
            {
                jsonText = sReader.ReadToEnd();
            }

            //deserialize the json string into a list of ski runs
            skiRuns = JsonConvert.DeserializeObject<List<SkiRun>>(jsonText);
            return skiRuns;
        }

        /// <summary>
        /// method to update an existing ski run
        /// </summary>
        /// <param name="skiRun">ski run object</param>
        public void Update(SkiRun skiRun)
        {
            foreach (SkiRun skiRunToUpdate in _skiRuns)
            {
                if (skiRunToUpdate.ID == skiRun.ID)
                {
                    skiRunToUpdate.Name = skiRun.Name;
                    skiRunToUpdate.Vertical = skiRun.Vertical;
                    break;
                }

                Save();
            }
        }

        /// <summary>
        /// Checks the existing ID value to see if the ID to be saved has already been used.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>bool value: True indicates the value has already been used, False indicates the value is available for use.</returns>
        private bool IsSkiRunIDTaken(int ID)
        {
            //Check the ID values of the existing ski runs.
            foreach (SkiRun skirun in _skiRuns)
            {
                if (skirun.ID == ID)
                {
                    //If the ID value has already been used, return true.
                    return true;
                }
            }

            //If the ID value has already been used, return false.
            return false;
        }

        /// <summary>
        /// Checks the existing Name value to see if the Name to be saved has already been used.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>bool value: True indicates the value has already been used, False indicates the value is available for use.</returns>
        private bool IsSkiRunNameTaken(string name)
        {
            //Check the Name values of the existing ski runs.
            foreach (SkiRun skirun in _skiRuns)
            {
                if (skirun.Name == name)
                {
                    //If the Name value has already been used, return true.
                    return true;
                }
            }

            //If the Name value has already been used, return false.
            return false;
        }

        /// <summary>
        /// method to write all of the list of ski runs to the text file
        /// </summary>
        public void Save()
        {
            //initialize streamwriter
            StreamWriter sWriter = new StreamWriter(DataSettings.dataFilePath);

            //generate json string from list of ski runs
            string jsonText = JsonConvert.SerializeObject(_skiRuns, Formatting.Indented);

            using (sWriter)
            {
                sWriter.Write(jsonText);
            }
        }

        #endregion
    }
}
