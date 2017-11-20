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

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS
        //need to overload the constructor based on the user's choice of persistence
        public Controller(SkiRunRepositoryCSV[] repoArray)
        {
            ApplicationControl(repoArray);
        }

        public Controller(SkiRunRepositoryJSON[] repoArray)
        {
            ApplicationControl(repoArray);
        }

        public Controller(SkiRunRepositoryXML_DS[] repoArray)
        {
            ApplicationControl(repoArray);
        }


        #endregion

        #region METHODS
        //also need to overload the ApplicationControl method to account for the user choice
        private void ApplicationControl(SkiRunRepositoryCSV[] repoArray)
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
                            ListAllSkiRuns(repoArray);
                            break;
                        case AppEnum.ManagerAction.DisplaySkiRunDetail:
                            DisplaySkiRunDetail(repoArray);
                            break;
                        case AppEnum.ManagerAction.DeleteSkiRun:
                            DeleteSkiRun(repoArray);
                            break;
                        case AppEnum.ManagerAction.AddSkiRun:
                            AddSkiRun(repoArray);
                            break;
                        case AppEnum.ManagerAction.UpdateSkiRun:
                            UpdateSkiRun(repoArray);
                            break;
                        case AppEnum.ManagerAction.QuerySkiRunsByVertical:
                            QuerySkiRunsByVertical(repoArray);
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

        

        private void ApplicationControl(SkiRunRepositoryXML_DS[] repoArray)
        {
            //Variable Declarations.

            
            ConsoleView.DisplayWelcomeScreen();

            
                //List<SkiRun> skiRuns = repoArray[0].SelectAllRuns();

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
                            ListAllSkiRuns(repoArray);
                            break;
                        case AppEnum.ManagerAction.DisplaySkiRunDetail:
                            DisplaySkiRunDetail(repoArray);
                            break;
                        case AppEnum.ManagerAction.DeleteSkiRun:
                            DeleteSkiRun(repoArray);
                            break;
                        case AppEnum.ManagerAction.AddSkiRun:
                            AddSkiRun(repoArray);
                            break;
                        case AppEnum.ManagerAction.UpdateSkiRun:
                            UpdateSkiRun(repoArray);
                            break;
                        case AppEnum.ManagerAction.QuerySkiRunsByVertical:
                            QuerySkiRunsByVertical(repoArray);
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

        private void ListAllSkiRuns(SkiRunRepositoryXML_DS[] repoArray)
        {
            repoArray[0] = new SkiRunRepositoryXML_DS();
            List<SkiRun> skiRuns = repoArray[0].SelectAllRuns();
            ConsoleView.DisplayAllSkiRuns(skiRuns, true);
            ConsoleView.DisplayContinuePrompt();
        }

        private void ListAllSkiRuns(SkiRunRepositoryCSV[] repoArray)
        {
            SkiRunRepositoryCSV skiRunRepository = new SkiRunRepositoryCSV();
            List<SkiRun> skiRuns = skiRunRepository.GetSkiAllRuns();
            ConsoleView.DisplayAllSkiRuns(skiRuns, true);
            ConsoleView.DisplayContinuePrompt();
        }

        private void ListAllSkiRuns(SkiRunRepositoryJSON[] repoArray)
        {
            SkiRunRepositoryJSON skiRunRepository = new SkiRunRepositoryJSON();
            List<SkiRun> skiRuns = skiRunRepository.GetSkiAllRuns();
            ConsoleView.DisplayAllSkiRuns(skiRuns, true);
            ConsoleView.DisplayContinuePrompt();
        }

        private void ApplicationControl(SkiRunRepositoryJSON[] repoArray)
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
                            ListAllSkiRuns(repoArray);
                            
                            break;
                        case AppEnum.ManagerAction.DisplaySkiRunDetail:
                            DisplaySkiRunDetail(repoArray);
                            break;
                        case AppEnum.ManagerAction.DeleteSkiRun:
                            DeleteSkiRun(repoArray);
                            break;
                        case AppEnum.ManagerAction.AddSkiRun:
                            AddSkiRun(repoArray);
                            break;
                        case AppEnum.ManagerAction.UpdateSkiRun:
                            UpdateSkiRun(repoArray);
                            break;
                        case AppEnum.ManagerAction.QuerySkiRunsByVertical:
                            QuerySkiRunsByVertical(repoArray);
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

        
        /// <summary>
        /// Adds a record to the data source with information provided by the user using the CSV repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void AddSkiRun(SkiRunRepositoryCSV[] repoArray)
        {
            SkiRunRepositoryCSV skiRunRepository = new SkiRunRepositoryCSV();
            //Variable Declarations.
            SkiRun aSkiRun = new SkiRun();

            ConsoleView.DisplayReset();

            //Get the ID, Name, and Vertical feet from the user.
            aSkiRun.ID = ConsoleView.GetIntegerFromUser("Enter the ID for the Ski Run: ");
            aSkiRun.Name = ConsoleView.GetUserResponse("Enter the name for the Ski Run: ");
            aSkiRun.Vertical = ConsoleView.GetIntegerFromUser("Enter the vertical (in feet) for the Ski Run: ");

            //Insert the new ski run.
            try
            {
                //Insert the new record.
                skiRunRepository.InsertSkiRun(aSkiRun);

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

        /// <summary>
        /// Adds a record to the data source with information provided by the user using the XML_DS repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void AddSkiRun(SkiRunRepositoryXML_DS[] repoArray)
        {
            SkiRunRepositoryXML_DS skiRunRepository = new SkiRunRepositoryXML_DS();
            //Variable Declarations.
            SkiRun aSkiRun = new SkiRun();

            ConsoleView.DisplayReset();

            //Get the ID, Name, and Vertical feet from the user.
            aSkiRun.ID = ConsoleView.GetIntegerFromUser("Enter the ID for the Ski Run: ");
            aSkiRun.Name = ConsoleView.GetUserResponse("Enter the name for the Ski Run: ");
            aSkiRun.Vertical = ConsoleView.GetIntegerFromUser("Enter the vertical (in feet) for the Ski Run: ");

            //Insert the new ski run.
            try
            {
                //Insert the new record.
                skiRunRepository.InsertSkiRun(aSkiRun);

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

        

        /// <summary>
        /// Adds a record to the data source with information provided by the user using the JSON repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void AddSkiRun(SkiRunRepositoryJSON[] repoArray)
        {
            SkiRunRepositoryJSON skiRunRepository = new SkiRunRepositoryJSON();
            //Variable Declarations.
            SkiRun aSkiRun = new SkiRun();

            ConsoleView.DisplayReset();

            //Get the ID, Name, and Vertical feet from the user.
            aSkiRun.ID = ConsoleView.GetIntegerFromUser("Enter the ID for the Ski Run: ");
            aSkiRun.Name = ConsoleView.GetUserResponse("Enter the name for the Ski Run: ");
            aSkiRun.Vertical = ConsoleView.GetIntegerFromUser("Enter the vertical (in feet) for the Ski Run: ");

            //Insert the new ski run.
            try
            {
                //Insert the new record.
                skiRunRepository.InsertSkiRun(aSkiRun);

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
        /// Deletes a record from the data source using the ID value entered by the user using the CSV repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        /// <param name="skiRuns"></param>
        private static void DeleteSkiRun(SkiRunRepositoryCSV[] repoArray)
        {
            SkiRunRepositoryCSV skiRunRepository = new SkiRunRepositoryCSV();
            List<SkiRun> skiRuns = skiRunRepository.GetSkiAllRuns();
            //Variable declarations.
            int skiRunID = 0;

            //reset display
            ConsoleView.DisplayReset();

            //Display all ski runs.
            ConsoleView.DisplayAllSkiRuns(skiRuns, false);
            Console.WriteLine();
            Console.WriteLine();

            //Get the ID for the ski run from the user.
            skiRunID = ConsoleView.GetIntegerFromUser("Enter Ski Run ID to delete: ");

            try
            {
                //Delete the ski run entered.
                skiRunRepository.DeleteSkiRun(skiRunID);

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

        /// <summary>
        /// Deletes a record from the data source using the ID value entered by the user using the XML_DS repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        /// <param name="skiRuns"></param>
        private static void DeleteSkiRun(SkiRunRepositoryXML_DS[] repoArray)
        {
            SkiRunRepositoryXML_DS skiRunRepository = new SkiRunRepositoryXML_DS();
            List<SkiRun> skiRuns = skiRunRepository.SelectAllRuns();
            //Variable declarations.
            int skiRunID = 0;

            //reset display
            ConsoleView.DisplayReset();

            //Display all ski runs.
            ConsoleView.DisplayAllSkiRuns(skiRuns, false);
            Console.WriteLine();
            Console.WriteLine();

            //Get the ID for the ski run from the user.
            skiRunID = ConsoleView.GetIntegerFromUser("Enter Ski Run ID to delete: ");

            try
            {
                //Delete the ski run entered.
                skiRunRepository.DeleteSkiRun(skiRunID);

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

        

        /// <summary>
        /// Deletes a record from the data source using the ID value entered by the user using the JSON repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        /// <param name="skiRuns"></param>
        private static void DeleteSkiRun(SkiRunRepositoryJSON[] repoArray)
        {
            SkiRunRepositoryJSON skiRunRepository = new SkiRunRepositoryJSON();
            List<SkiRun> skiRuns = skiRunRepository.GetSkiAllRuns();
            //Variable declarations.
            int skiRunID = 0;

            //reset display
            ConsoleView.DisplayReset();

            //Display all ski runs.
            ConsoleView.DisplayAllSkiRuns(skiRuns, false);
            Console.WriteLine();
            Console.WriteLine();

            //Get the ID for the ski run from the user.
            skiRunID = ConsoleView.GetIntegerFromUser("Enter Ski Run ID to delete: ");

            try
            {
                //Delete the ski run entered.
                skiRunRepository.DeleteSkiRun(skiRunID);

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

        /// <summary>
        /// Displays a list of all ski runs using the CSV repository.
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void DisplaySkiRunDetail(SkiRunRepositoryCSV[] repoArray)
        {
            SkiRunRepositoryCSV skiRunRepository = new SkiRunRepositoryCSV();

            ConsoleView.DisplayReset();
            //ConsoleView.DisplayHeader("Display Ski Run Information");

            try
            {
                //Display the ski run information on the screen.
                ConsoleView.DisplaySkiRunDetail(skiRunRepository.GetSkiRunByID(ConsoleView.GetIntegerFromUser("Enter the ID for the Ski Run: ")));
                ConsoleView.DisplayContinuePrompt();
            }
            catch (Exception ex)
            {
                ConsoleView.DisplayErrorMessage(ex.Message);
                ConsoleView.DisplayContinuePrompt();
            }
        }

        private static void DisplaySkiRunDetail(SkiRunRepositoryXML_DS[] repoArray)
        {
            SkiRunRepositoryXML_DS skiRunRepository = new SkiRunRepositoryXML_DS();
            ConsoleView.DisplayReset();
            //ConsoleView.DisplayHeader("Display Ski Run Information");

            try
            {
                //Display the ski run information on the screen.
                ConsoleView.DisplaySkiRunDetail(skiRunRepository.SelectByID(ConsoleView.GetIntegerFromUser("Enter the ID for the Ski Run: ")));
                ConsoleView.DisplayContinuePrompt();
            }
            catch (Exception ex)
            {
                ConsoleView.DisplayErrorMessage(ex.Message);
                ConsoleView.DisplayContinuePrompt();
            }
        }

        

        /// <summary>
        /// Displays a list of all ski runs using the JSON repository.
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void DisplaySkiRunDetail(SkiRunRepositoryJSON[] repoArray)
        {
            SkiRunRepositoryJSON skiRunRepository = new SkiRunRepositoryJSON();
            ConsoleView.DisplayReset();
            //ConsoleView.DisplayHeader("Display Ski Run Information");

            try
            {
                //Display the ski run information on the screen.
                ConsoleView.DisplaySkiRunDetail(skiRunRepository.GetSkiRunByID(ConsoleView.GetIntegerFromUser("Enter the ID for the Ski Run: ")));
                ConsoleView.DisplayContinuePrompt();
            }
            catch (Exception ex)
            {
                ConsoleView.DisplayErrorMessage(ex.Message);
                ConsoleView.DisplayContinuePrompt();
            }
        }

        /// <summary>
        /// Allows the user to select a list of ski runs based on the vertical value for the CSV repository.
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void QuerySkiRunsByVertical(SkiRunRepositoryCSV[] repoArray)
        {
            SkiRunRepositoryCSV skiRunRepository = new SkiRunRepositoryCSV();
            int[] minMaxValues = ConsoleView.DisplayGetSkiRunQuery();
            List<SkiRun> results = skiRunRepository.QueryByVertical(minMaxValues[0], minMaxValues[1]);
            ConsoleView.DisplayQueryResults(results);
            ConsoleView.DisplayContinuePrompt();
        }

        /// <summary>
        /// Allows the user to select a list of ski runs based on the vertical value for the XML_DS repository.
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void QuerySkiRunsByVertical(SkiRunRepositoryXML_DS[] repoArray)
        {
            SkiRunRepositoryXML_DS skiRunRepository = new SkiRunRepositoryXML_DS();
            List<SkiRun> skiRuns = skiRunRepository.SelectAllRuns();
            int[] minMaxValues = ConsoleView.DisplayGetSkiRunQuery();
            List<SkiRun> results = skiRunRepository.QueryByVertical(minMaxValues[0], minMaxValues[1]);
            ConsoleView.DisplayQueryResults(results);
            ConsoleView.DisplayContinuePrompt();
        }

        
        /// <summary>
        /// Allows the user to select a list of ski runs based on the vertical value for the JSON repository.
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void QuerySkiRunsByVertical(SkiRunRepositoryJSON[] repoArray)
        {
            SkiRunRepositoryJSON skiRunRepository = new SkiRunRepositoryJSON();
            int[] minMaxValues = ConsoleView.DisplayGetSkiRunQuery();
            List<SkiRun> results = skiRunRepository.QueryByVertical(minMaxValues[0], minMaxValues[1]);
            ConsoleView.DisplayQueryResults(results);
            ConsoleView.DisplayContinuePrompt();
        }

        /// <summary>
        /// Updates a specific ski run's information in the data source with data entered by the user for the CSV repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        /// <param name="skiRuns"></param>
        private static void UpdateSkiRun(SkiRunRepositoryCSV[] repoArray)
        {
            SkiRunRepositoryCSV skiRunRepository = new SkiRunRepositoryCSV();
            List<SkiRun> skiRuns = skiRunRepository.GetSkiAllRuns();
            //Variable Declarations.
            SkiRun aSkiRun = new SkiRun();

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
                aSkiRun = skiRunRepository.GetSkiRunByID(ConsoleView.GetIntegerFromUser("Enter the ID for the Ski Run: "));
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
                skiRunRepository.UpdateSkiRun(aSkiRun);

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

        /// <summary>
        /// Updates a specific ski run's information in the data source with data entered by the user for the XML_DS repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        /// <param name="skiRuns"></param>
        private static void UpdateSkiRun(SkiRunRepositoryXML_DS[] repoArray)
        {
            SkiRunRepositoryXML_DS skiRunRepository = new SkiRunRepositoryXML_DS();
            List<SkiRun> skiRuns = skiRunRepository.SelectAllRuns();
            //Variable Declarations.
            SkiRun aSkiRun = new SkiRun();

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
                aSkiRun = skiRunRepository.SelectByID(ConsoleView.GetIntegerFromUser("Enter the ID for the Ski Run: "));
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
                skiRunRepository.UpdateSkiRun(aSkiRun);

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

        
        /// <summary>
        /// Updates a specific ski run's information in the data source with data entered by the user for the JSON repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        /// <param name="skiRuns"></param>
        private static void UpdateSkiRun(SkiRunRepositoryJSON[] repoArray)
        {
            SkiRunRepositoryJSON skiRunRepository = new SkiRunRepositoryJSON();
            List<SkiRun> skiRuns = skiRunRepository.GetSkiAllRuns();
            //Variable Declarations.
            SkiRun aSkiRun = new SkiRun();

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
                aSkiRun = skiRunRepository.GetSkiRunByID(ConsoleView.GetIntegerFromUser("Enter the ID for the Ski Run: "));
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
                skiRunRepository.UpdateSkiRun(aSkiRun);

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


        #endregion

    }
}
