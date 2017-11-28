using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiRunRater;

namespace SkiRunRater
{
    public static class ConsoleView
    {
        #region ENUMERABLES


        #endregion

        #region FIELDS

        //
        // window size
        //
        private const int WINDOW_WIDTH = ViewSettings.WINDOW_WIDTH;
        private const int WINDOW_HEIGHT = ViewSettings.WINDOW_HEIGHT;

        //
        // horizontal and vertical margins in console window for display
        //
        private const int DISPLAY_HORIZONTAL_MARGIN = ViewSettings.DISPLAY_HORIZONTAL_MARGIN;
        private const int DISPALY_VERITCAL_MARGIN = ViewSettings.DISPALY_VERITCAL_MARGIN;

        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS        

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            Console.WriteLine(ConsoleUtil.Center("Press any key to continue.", WINDOW_WIDTH));
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        /// <summary>
        /// method to display all ski run info
        /// </summary>
        public static void DisplayAllSkiRuns(List<SkiRun> skiRuns, bool resetDisplay)
        {
            if (resetDisplay == true)
            {
                DisplayReset();
            }
            
            DisplayMessage("All of the existing ski runs are displayed below;");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(8));
            columnHeader.Append("Ski Run".PadRight(25));
            columnHeader.Append("Vertical in Feet".PadRight(5));

            DisplayMessage(columnHeader.ToString());

            foreach (SkiRun skiRun in skiRuns)
            {
                StringBuilder skiRunInfo = new StringBuilder();

                skiRunInfo.Append(skiRun.ID.ToString().PadRight(8));
                skiRunInfo.Append(skiRun.Name.PadRight(25));
                skiRunInfo.Append(skiRun.Vertical.ToString().PadRight(5));

                DisplayMessage(skiRunInfo.ToString());
            }
        }

        /// <summary>
        /// The DisplayErrorMessage method will display the error message passed in the errorText parameter
        /// in red text.
        /// </summary>
        /// <param name="errorText"></param>
        public static void DisplayErrorMessage(String errorText)
        {
            //Display the error message to the screen.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(errorText);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// display the Exit prompt
        /// </summary>
        public static void DisplayExitPrompt()
        {
            DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            DisplayMessage("Thank you for using our application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// This method gets the lower and upper bounds of the query from the user
        /// </summary>
        public static int[] DisplayGetSkiRunQuery()
        {
            
            bool validValues = false;
            int[] values = new int[2];
            while (!validValues)
            {
                DisplayReset();
                DisplayMessage("Ski Run Query");
                DisplayMessage("");
                values[0] = GetIntegerFromUser("Enter the minimum vertical value (in feet): ");
                values[1] = GetIntegerFromUser("Enter the maximum vertical value (in feet): ");
                if (values[0] > values[1])
                {
                    DisplayErrorMessage("The minimum value cannot be greater than the maximum value. You will have to select values again.");
                    DisplayContinuePrompt();
                    continue;
                }
                validValues = true;
            }
            
            return values;
        }

        /// <summary>
        /// The DisplayHeader method will display the header text passed in the headerText parameter.
        /// </summary>
        /// <param name="headerText"></param>
        public static void DisplayHeader(String headerText)
        {
            //Display the header text.
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine(headerText);
            Console.WriteLine("");
        }

        /// <summary>
        /// display a message in the message area
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            // message is not an empty line, display text
            if (message != "")
            {
                //
                // create a list of strings to hold the wrapped text message
                //
                List<string> messageLines;

                //
                // call utility method to wrap text and loop through list of strings to display
                //
                messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);
                foreach (var messageLine in messageLines)
                {
                    Console.WriteLine(messageLine);
                }
            }
            // display an empty line
            else
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// display a message in the message area without a new line for the prompt
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayPromptMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            //
            // create a list of strings to hold the wrapped text message
            //
            List<string> messageLines;

            //
            // call utility method to wrap text and loop through list of strings to display
            //
            messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);

            for (int lineNumber = 0; lineNumber < messageLines.Count() - 1; lineNumber++)
            {
                Console.WriteLine(messageLines[lineNumber]);
            }

            Console.Write(messageLines[messageLines.Count() - 1]);
        }

        /// <summary>
        /// This method displays the query results
        /// </summary>
        /// <param name="skiRuns"></param>
        public static void DisplayQueryResults(List<SkiRun> skiRuns)
        {
            DisplayReset();

            DisplayMessage("The ski runs that satisfy your criteria are listed below:");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(8));
            columnHeader.Append("Ski Run".PadRight(25));
            columnHeader.Append("Vertical in Feet".PadRight(5));

            DisplayMessage(columnHeader.ToString());

            foreach (SkiRun skiRun in skiRuns)
            {
                StringBuilder skiRunInfo = new StringBuilder();

                skiRunInfo.Append(skiRun.ID.ToString().PadRight(8));
                skiRunInfo.Append(skiRun.Name.PadRight(25));
                skiRunInfo.Append(skiRun.Vertical.ToString().PadRight(5));

                DisplayMessage(skiRunInfo.ToString());
            }
        }

        public static string DisplayChoosePersistence()
        {
            string userResponse = "0";
            bool validResponse = false;
            while (!validResponse)
            {
                DisplayReset();
                Console.WriteLine("Choose the type of persistence that the application will use:");
                Console.WriteLine();
                Console.WriteLine("(1) CSV file");
                Console.WriteLine("(2) XML file");
                Console.WriteLine("(3) JSON file");
                Console.WriteLine("(4) SQL Database");
                Console.WriteLine();
                userResponse = Console.ReadLine();
                if (userResponse != "1" && userResponse != "2" && userResponse != "3" && userResponse!="4")
                {
                    DisplayErrorMessage("You did not enter a valid response. Press any key to try again.");
                    Console.ReadKey(true);
                    continue;
                }
                validResponse = true;
            }
            
            return userResponse;
            


        }

        /// <summary>
        /// reset display to default size and colors including the header
        /// </summary>
        public static void DisplayReset()
        {
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Ski Run Rater", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Displays the detail information for a single ski run.
        /// </summary>
        /// <param name="skiRun"></param>
        public static void DisplaySkiRunDetail(SkiRun skiRun)
        {
            DisplayReset();

            DisplayMessage($"The details for ski run {skiRun.Name} are listed below: ");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(8));
            columnHeader.Append("Ski Run".PadRight(25));
            columnHeader.Append("Vertical in Feet".PadRight(5));

            DisplayMessage(columnHeader.ToString());

            StringBuilder skiRunInfo = new StringBuilder();

            skiRunInfo.Append(skiRun.ID.ToString().PadRight(8));
            skiRunInfo.Append(skiRun.Name.PadRight(25));
            skiRunInfo.Append(skiRun.Vertical.ToString().PadRight(5));

            DisplayMessage(skiRunInfo.ToString());
        }

        /// <summary>
        /// display the welcome screen
        /// </summary>
        public static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("Welcome to", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Ski Run Rater", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Prompts the user to enter an integer value.  Once the user has entered the value the input
        /// is checked to make sure it is valid.
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public static int GetIntegerFromUser(string prompt)
        {
            //Variable Declarations.
            int userInteger = 0;
            string userResponse = "";
            bool validResponse = false;

            //Loop until the user provides a valid response.
            while (validResponse == false)
            {
                //Prompt the user for the integer value
                Console.Write($"{prompt}");
                userResponse = Console.ReadLine();

                //Check the integer provided is within the range given.
                if (int.TryParse(userResponse, out userInteger))
                {
                    validResponse = true;
                }
                else
                {
                    DisplayErrorMessage("Please enter an integer value.\n");
                }
            }

            return userInteger;
        }

        /// <summary>
        /// method to display the manager menu and get the user's choice
        /// </summary>
        /// <returns></returns>
        public static AppEnum.ManagerAction GetUserActionChoice()
        {
            AppEnum.ManagerAction userActionChoice = AppEnum.ManagerAction.None;
            //
            // set a string variable with a length equal to the horizontal margin and filled with spaces
            //
            string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);

            //
            // set up display area
            //
            DisplayReset();

            //
            // display the menu
            //
            DisplayMessage("Ski Manager Menu");
            DisplayMessage("");
            Console.WriteLine(
                leftTab + "1. Display All Ski Runs Information" + Environment.NewLine +
                leftTab + "2. Display Ski Run Detail" + Environment.NewLine +
                leftTab + "3. Add a Ski Run" + Environment.NewLine +
                leftTab + "4. Delete a Ski Run" + Environment.NewLine +
                leftTab + "5. Update a Ski Run" + Environment.NewLine +
                leftTab + "6. Query by Vertical" + Environment.NewLine +
                leftTab + "E. Exit" + Environment.NewLine);

            DisplayMessage("");
            DisplayPromptMessage("Enter the number/letter for the menu choice.");
            ConsoleKeyInfo userResponse = Console.ReadKey(true);

            switch (userResponse.KeyChar)
            {
                case '1':
                    userActionChoice = AppEnum.ManagerAction.ListAllSkiRuns;
                    break;
                case '2':
                    userActionChoice = AppEnum.ManagerAction.DisplaySkiRunDetail;
                    break;
                case '3':
                    userActionChoice = AppEnum.ManagerAction.AddSkiRun;
                    break;
                case '4':
                    userActionChoice = AppEnum.ManagerAction.DeleteSkiRun;
                    break;
                case '5':
                    userActionChoice = AppEnum.ManagerAction.UpdateSkiRun;
                    break;
                case '6':
                    userActionChoice = AppEnum.ManagerAction.QuerySkiRunsByVertical;
                    break;
                case 'E':
                case 'e':
                    userActionChoice = AppEnum.ManagerAction.Quit;
                    break;
                default:
                    Console.WriteLine(
                        "It appears you have selected an incorrect choice." + Environment.NewLine +
                        "Press any key to try again or the ESC key to exit.");

                    userResponse = Console.ReadKey(true);
                    if (userResponse.Key == ConsoleKey.Escape)
                    {
                        userActionChoice = AppEnum.ManagerAction.Quit;
                    }
                    break;
            }

            return userActionChoice;
        }

        /// <summary>
        /// Gets an input from the user.
        /// </summary>
        /// <returns></returns>
        public static string GetUserResponse(string prompt)
        {
            Console.WriteLine();
            Console.Write($"{prompt} ");

            //Return the user's menu choice.
            return Console.ReadLine();
        }

        #endregion
    }
}
