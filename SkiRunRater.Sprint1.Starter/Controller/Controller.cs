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

        public Controller()
        {
            ApplicationControl();
        }

        #endregion

        #region METHODS

        private void ApplicationControl()
        {
            //Variable Declarations.
            //SkiRunRepositoryCSV skiRunRepository = new SkiRunRepositoryCSV();
            SkiRunRepositoryXML skiRunRepository = new SkiRunRepositoryXML();
            //SkiRunRepositoryJSON skiRunRepository = new SkiRunRepositoryJSON();
            List<SkiRun> skiRunDetail = new List<SkiRun>();

            ConsoleView.DisplayWelcomeScreen();

            using (skiRunRepository)
            {
                List<SkiRun> skiRuns = skiRunRepository.GetSkiAllRuns();

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
                            ConsoleView.DisplayAllSkiRuns(skiRuns, true);
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.DisplaySkiRunDetail:
                            DisplaySkiRunDetail(skiRunRepository);
                            break;
                        case AppEnum.ManagerAction.DeleteSkiRun:
                            DeleteSkiRun(skiRunRepository, skiRuns);
                            break;
                        case AppEnum.ManagerAction.AddSkiRun:
                            AddSkiRun(skiRunRepository);
                            break;
                        case AppEnum.ManagerAction.UpdateSkiRun:
                            UpdateSkiRun(skiRunRepository, skiRuns);
                            break;
                        case AppEnum.ManagerAction.QuerySkiRunsByVertical:
                            QuerySkiRunsByVertical(skiRunRepository);
                            break;
                        case AppEnum.ManagerAction.Quit:
                            active = false;
                            break;
                        default:
                            break;
                    }
                }
            }

            ConsoleView.DisplayExitPrompt();
        }

        /// <summary>
        /// Adds a record to the data source with information provided by the user using the CSV repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void AddSkiRun(SkiRunRepositoryCSV skiRunRepository)
        {
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
        /// Adds a record to the data source with information provided by the user using the XML repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void AddSkiRun(SkiRunRepositoryXML skiRunRepository)
        {
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
        private static void AddSkiRun(SkiRunRepositoryJSON skiRunRepository)
        {
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
        private static void DeleteSkiRun(SkiRunRepositoryCSV skiRunRepository, List<SkiRun> skiRuns)
        {
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
        /// Deletes a record from the data source using the ID value entered by the user using the XML repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        /// <param name="skiRuns"></param>
        private static void DeleteSkiRun(SkiRunRepositoryXML skiRunRepository, List<SkiRun> skiRuns)
        {
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
        private static void DeleteSkiRun(SkiRunRepositoryJSON skiRunRepository, List<SkiRun> skiRuns)
        {
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
        private static void DisplaySkiRunDetail(SkiRunRepositoryCSV skiRunRepository)
        {
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
        /// Displays a list of all ski runs using the XML repository.
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void DisplaySkiRunDetail(SkiRunRepositoryXML skiRunRepository)
        {
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
        /// Displays a list of all ski runs using the JSON repository.
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void DisplaySkiRunDetail(SkiRunRepositoryJSON skiRunRepository)
        {
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
        private static void QuerySkiRunsByVertical(SkiRunRepositoryCSV skiRunRepository)
        {
            int[] minMaxValues = ConsoleView.DisplayGetSkiRunQuery();
            List<SkiRun> results = skiRunRepository.QueryByVertical(minMaxValues[0], minMaxValues[1]);
            ConsoleView.DisplayQueryResults(results);
            ConsoleView.DisplayContinuePrompt();
        }

        /// <summary>
        /// Allows the user to select a list of ski runs based on the vertical value for the XML repository.
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void QuerySkiRunsByVertical(SkiRunRepositoryXML skiRunRepository)
        {
            int[] minMaxValues = ConsoleView.DisplayGetSkiRunQuery();
            List<SkiRun> results = skiRunRepository.QueryByVertical(minMaxValues[0], minMaxValues[1]);
            ConsoleView.DisplayQueryResults(results);
            ConsoleView.DisplayContinuePrompt();
        }

        /// <summary>
        /// Allows the user to select a list of ski runs based on the vertical value for the JSON repository.
        /// </summary>
        /// <param name="skiRunRepository"></param>
        private static void QuerySkiRunsByVertical(SkiRunRepositoryJSON skiRunRepository)
        {
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
        private static void UpdateSkiRun(SkiRunRepositoryCSV skiRunRepository, List<SkiRun> skiRuns)
        {
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
        /// Updates a specific ski run's information in the data source with data entered by the user for the XML repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        /// <param name="skiRuns"></param>
        private static void UpdateSkiRun(SkiRunRepositoryXML skiRunRepository, List<SkiRun> skiRuns)
        {
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
        /// Updates a specific ski run's information in the data source with data entered by the user for the JSON repository
        /// </summary>
        /// <param name="skiRunRepository"></param>
        /// <param name="skiRuns"></param>
        private static void UpdateSkiRun(SkiRunRepositoryJSON skiRunRepository, List<SkiRun> skiRuns)
        {
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
