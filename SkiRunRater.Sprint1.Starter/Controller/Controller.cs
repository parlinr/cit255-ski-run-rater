using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SkiRunRater
{
    public class Controller
    {
        #region FIELDS

        bool active = true;
        static ISkiRunRepository skiRunRepository;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS
        
        public Controller()
        {
            if (DataSettings.dataFilePath == "Data\\Data.csv")
            {
                skiRunRepository = new SkiRunRepositoryCSV();
            }
            else if (DataSettings.dataFilePath == "Data\\Data.xml")
            {
                skiRunRepository = new SkiRunRepositoryXML_DS();
            }
            else if (DataSettings.dataFilePath == "Data\\Data.json")
            {
                skiRunRepository = new SkiRunRepositoryJSON();
            }
            else
            {
                skiRunRepository = new SkiRunRepositorySQL();
            }
            
            ApplicationControl();
        }


        #endregion

        #region METHODS
        
        private void ApplicationControl()
        {
            //Variable Declarations.
                                    
            

            ConsoleView.DisplayWelcomeScreen();

                          

                while (active)
                {
                    AppEnum.ManagerAction userActionChoice;
                    SkiRun aSkiRun = new SkiRun();

                    userActionChoice = ConsoleView.GetUserActionChoice();

                    switch (userActionChoice)
                    {
                        case AppEnum.ManagerAction.None:
                            break;
                        case AppEnum.ManagerAction.ListAllSkiRuns:
                        //Display all ski runs.
                            ListAllSkiRuns();
                            break;
                        case AppEnum.ManagerAction.DisplaySkiRunDetail:
                            DisplaySkiRunDetail();
                            break;
                        case AppEnum.ManagerAction.DeleteSkiRun:
                            DeleteSkiRun();
                            break;
                        case AppEnum.ManagerAction.AddSkiRun:
                            AddSkiRun();
                            break;
                        case AppEnum.ManagerAction.UpdateSkiRun:
                            UpdateSkiRun();
                            break;
                        case AppEnum.ManagerAction.QuerySkiRunsByVertical:
                            QuerySkiRunsByVertical();
                            break;
                        case AppEnum.ManagerAction.Quit:
                            active = false;
                            break;
                        default:
                            break;
                    }
                }

                ConsoleView.DisplayExitPrompt();
            
                
        }

        

        
        private void ListAllSkiRuns()
        {
            SkiRunBusiness skiRunBusiness = new SkiRunBusiness(skiRunRepository);
            using (skiRunBusiness)
            {
                List<SkiRun> skiRuns = skiRunRepository.SelectAll();
                ConsoleView.DisplayAllSkiRuns(skiRuns, true);
            }
                
            ConsoleView.DisplayContinuePrompt();
        }

       
           

        
        /// <summary>
        /// Adds a record to the data source with information provided by the user 
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void AddSkiRun()
        {
            SkiRunBusiness skiRunBusiness = new SkiRunBusiness(skiRunRepository);
            //Variable Declarations.
            SkiRun aSkiRun = new SkiRun();

            ConsoleView.DisplayReset();

            //Get the ID, Name, and Vertical feet from the user.
            aSkiRun.ID = ConsoleView.GetIntegerFromUser("Enter the ID for the Ski Run: ");
            aSkiRun.Name = ConsoleView.GetUserResponse("Enter the name for the Ski Run: ");
            aSkiRun.Vertical = ConsoleView.GetIntegerFromUser("Enter the vertical (in feet) for the Ski Run: ");
            using (skiRunBusiness)
            {
                //Insert the new ski run.
                try
                {
                    //Insert the new record.
                    skiRunRepository.Insert(aSkiRun);

                    //Display a message to the user that the record was inserted.
                    ConsoleView.DisplayReset();
                    ConsoleView.DisplayMessage($"The information for the {aSkiRun.Name} ski run has been saved.");
                    ConsoleView.DisplayContinuePrompt();
                }
                catch (Exception ex)
                {
                    //Display the error message for the error that occurred.
                    CatchIOExceptions(ex);
                }
            }
                
        }

        
        /// <summary>
        /// Handles errors for File I/O operations.
        /// </summary>
        /// <param name="exc"></param>
        private static void CatchIOExceptions(Exception exc)
        {
            if (exc is DriveNotFoundException)
            {
                //Display the error message on the screen.
                ConsoleView.DisplayErrorMessage(exc.Message.ToString());
                Console.ReadKey();
                return;
            }
            else if (exc is DirectoryNotFoundException)
            {
                //Display the error message on the screen.
                ConsoleView.DisplayErrorMessage(exc.Message.ToString());
                Console.ReadKey();
                return;
            }
            else if (exc is FileNotFoundException)
            {
                //Display the error message on the screen.
                ConsoleView.DisplayErrorMessage(exc.Message.ToString());
                Console.ReadKey();
                return;
            }
            else if (exc is EndOfStreamException)
            {
                //Display the error message on the screen.
                ConsoleView.DisplayErrorMessage(exc.Message.ToString());
                Console.ReadKey();
                return;
            }
            else if (exc is ArgumentException)
            {
                //Display the error message on the screen.
                ConsoleView.DisplayErrorMessage(exc.Message.ToString());
                Console.ReadKey();
                return;
            }
            else if (exc is Exception)
            {
                //Display the error message on the screen.
                ConsoleView.DisplayErrorMessage(exc.Message.ToString());
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Deletes a record from the data source using the ID value entered by the user 
        /// </summary>
        /// <param name="skiRunRepository"></param>
        /// <param name="skiRuns"></param>
        private static void DeleteSkiRun()
        {
            SkiRunBusiness skiRunBusiness = new SkiRunBusiness(skiRunRepository);
            List<SkiRun> skiRuns = skiRunRepository.SelectAll();
            //Variable declarations.
            int skiRunID = 0;

            //reset display
            ConsoleView.DisplayReset();
            using (skiRunBusiness)
            {
                //Display all ski runs.
                ConsoleView.DisplayAllSkiRuns(skiRuns, false);
                Console.WriteLine();
                Console.WriteLine();

                //Get the ID for the ski run from the user.
                skiRunID = ConsoleView.GetIntegerFromUser("Enter Ski Run ID to delete: ");

                try
                {
                    //Delete the ski run entered.
                    skiRunRepository.Delete(skiRunID);

                    //Display a message to the user that the ski run has been deleted.
                    ConsoleView.DisplayReset();
                    ConsoleView.DisplayMessage($"Ski Run ID: {skiRunID} had been deleted.");
                    ConsoleView.DisplayContinuePrompt();
                }
                catch (Exception ex)
                {
                    //Display the error message for the error that occurred.
                    CatchIOExceptions(ex);
                }
            }
                
        }

        
        /// <summary>
        /// Displays a list of all ski runs
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void DisplaySkiRunDetail()
        {
            SkiRunBusiness skiRunBusiness = new SkiRunBusiness(skiRunRepository);

            ConsoleView.DisplayReset();
            //ConsoleView.DisplayHeader("Display Ski Run Information");
            using (skiRunBusiness)
            {
                try
                {
                    //Display the ski run information on the screen.
                    ConsoleView.DisplaySkiRunDetail(skiRunRepository.SelectById(ConsoleView.GetIntegerFromUser("Enter the ID for the Ski Run: ")));
                    ConsoleView.DisplayContinuePrompt();
                }
                catch (Exception ex)
                {
                    ConsoleView.DisplayErrorMessage(ex.Message);
                    ConsoleView.DisplayContinuePrompt();
                }
            }
                
        }

        
        /// <summary>
        /// Allows the user to select a list of ski runs based on the vertical value 
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void QuerySkiRunsByVertical()
        {
            SkiRunBusiness skiRunBusiness = new SkiRunBusiness(skiRunRepository);
            int[] minMaxValues = ConsoleView.DisplayGetSkiRunQuery();
            using (skiRunBusiness)
            {
                List<SkiRun> results = skiRunBusiness.QueryByVertical(minMaxValues[0], minMaxValues[1]);
                ConsoleView.DisplayQueryResults(results);
            }
                
            ConsoleView.DisplayContinuePrompt();
        }

        
        /// <summary>
        /// Updates a specific ski run's information in the data source with data entered by the user
        /// </summary>
        /// <param name="skiRunRepository"></param>
        /// <param name="skiRuns"></param>
        private static void UpdateSkiRun()
        {
            SkiRunBusiness skiRunBusiness = new SkiRunBusiness(skiRunRepository);
            SkiRun aSkiRun = new SkiRun();
            using (skiRunBusiness)
            {
                List<SkiRun> skiRuns = skiRunRepository.SelectAll();
                //Variable Declarations.
                

                ConsoleView.DisplayReset();

                //Display all ski runs.
                ConsoleView.DisplayAllSkiRuns(skiRuns, false);
                Console.WriteLine();
                Console.WriteLine();

                //Get the information for the ski run to be updated and display it on the screen.
                try
                {
                    //Display the ski run information on the screen.
                    //ConsoleView.DisplaySkiRunDetail(skiRunRepository.GetSkiRunByID(ConsoleView.GetIntegerFromUser("Enter the ID for the Ski Run: ")));
                    aSkiRun = skiRunRepository.SelectById(ConsoleView.GetIntegerFromUser("Enter the ID for the Ski Run: "));
                    ConsoleView.DisplaySkiRunDetail(aSkiRun);
                }
                catch (Exception ex)
                {
                    //Display the error message for the error that occurred.
                    CatchIOExceptions(ex);
                    return;
                }

                //Get the new Name and Vertical feet from the user.
                Console.WriteLine();
                Console.WriteLine();
                aSkiRun.Name = ConsoleView.GetUserResponse("Enter the new name for the Ski Run: ");
                aSkiRun.Vertical = ConsoleView.GetIntegerFromUser("Enter the new vertical (in feet) for the Ski Run: ");

                //Update the ski run.
                try
                {
                    //Update the ski run information.
                    skiRunRepository.Update(aSkiRun);

                    //Display a message to the user that the record was updated.
                    ConsoleView.DisplayReset();
                    ConsoleView.DisplayMessage($"The information for the {aSkiRun.Name} ski run has been updated.");
                    ConsoleView.DisplayContinuePrompt();
                }
                catch (Exception ex)
                {
                    //Display the error message for the error that occurred.
                    CatchIOExceptions(ex);
                    return;
                }

            }

            
        }

                   

            
        


        #endregion

    }
}
