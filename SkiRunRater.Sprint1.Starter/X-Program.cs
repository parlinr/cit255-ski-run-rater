using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_PersistenceFileStream
{
    class XProgram
    {
        /*
        static void Main(string[] args)
        {
            //Variable Declarations.
            bool exitApplication = false;
            string textFilePath = "Data\\Data.txt";

            //ObjectListReadWrite(textFilePath);
            
            //Display the opening screen.
            DisplayOpeningScreen();

            //Begin the application loop.
            do
            {
                //Display the main menu on the screen.
                DisplayMainMenu();

                //Get the user's menu choice and take the appropriate action.
                switch (GetUserResponse("Enter a Menu Option:"))
                {
                    case "1":  //Display All Records.
                        DisplayAllRecords(textFilePath);
                        break;
                    case "2":  //Add a new Record.
                        AddPlayerScore(textFilePath);
                        break;
                    case "3":   //Delete a Record.
                        DeletePlayerScore(textFilePath);
                        break;
                    case "4":   //Update a Record.
                        UpdatePlayerScore(textFilePath);
                        break;
                    case "5":   //Clear All Records.
                        ClearAllRecords(textFilePath);
                        break;
                    case "6":   //Exit.
                        exitApplication = true;
                        break;
                    default:
                        DisplayErrorMessage("You have entered an invalid menu option, press any key to try again");
                        Console.ReadKey();
                        break;
                }
            } while (exitApplication == false);

            //Display the closing screen.
            DisplayClosingScreen();
        }

        /// <summary>
        /// Add a new player name and score to the file of player scores.
        /// </summary>
        /// <param name="filePath"></param>
        private static void AddPlayerScore(string filePath)
        {
            //Variable Declarations.
            string newPlayer = "";
            int newScore = 0;
            Scores newPlayerScore = new Scores();
            List<Scores> playerScores = new List<Scores>();

            //Read the records from the file.
            //playerScores = CsvUtility.ReadScoresFromTextFile(filePath);
            try
            {
                //Save the new list of scores to the file.
                playerScores = CsvUtility.ReadScoresFromTextFile(filePath);
            }
            catch (Exception e)
            {
                //Display an error message for the specific error.
                CatchIOExceptions(e);

                //Return to the main menu.
                return;
            }

            //Display the Record(s) on the screen.
            DisplayHeader("Add New Player Score");

            //Get a new name from the user.
            newPlayer = GetUserResponse("Enter the player's name:");

            //Get a new score from the user.
            while (!int.TryParse(GetUserResponse("Enter the player's score:"), out newScore))
            {
                //If the numer entered is not a valid integer...

                //Display an error message
                DisplayErrorMessage("You have entered an invalid integer.  Press any key to re-enter.");
                Console.ReadKey();

                //Re-dislay the header.
                DisplayHeader("Add New Player Score");
            } 

            //Add the new player's name and score to the list of scores.
            newPlayerScore.PlayerName = newPlayer;
            newPlayerScore.PlayerScore = newScore;
            playerScores.Add(newPlayerScore);

            //Save the new list of scores to the file.           
            try
            {
                //Save the new list of scores to the file.
                CsvUtility.WriteScoresToTextFile(playerScores, filePath);

                //Tell the user the record has been added.
                DisplaySuccessMessage("The Record has been Added.");
                DisplayContinuePrompt();
            }
            catch (Exception e)
            {
                //Display an error message for the specific error.
                CatchIOExceptions(e);

                //Return to the main menu.
                return;
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
                DisplayErrorMessage(exc.Message.ToString());
                Console.ReadKey();
                return;
            }
            else if(exc is DirectoryNotFoundException)
            {
                //Display the error message on the screen.
                DisplayErrorMessage(exc.Message.ToString());
                Console.ReadKey();
                return;
            }
            else if (exc is FileNotFoundException)
            {
                //Display the error message on the screen.
                DisplayErrorMessage(exc.Message.ToString());
                Console.ReadKey();
                return;
            }
            else if (exc is EndOfStreamException)
            {
                //Display the error message on the screen.
                DisplayErrorMessage(exc.Message.ToString());
                Console.ReadKey();
                return;
            }
            else if (exc is Exception)
            {
                //Display the error message on the screen.
                DisplayErrorMessage(exc.Message.ToString());
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Clears all the scores from the file.
        /// </summary>
        /// <param name="filePath"></param>
        private static void ClearAllRecords(string filePath)
        {
            //Variable Declarations.
            List<Scores> playerScores = new List<Scores>();

            //Save a blank list to the file.           
            try
            {



                //Ask the user if they wish to clear all records.
                if (DisplayGetYesNoPrompt("Are you sure you want to clear all scores?") == true)
                {
                    //If the user response is yes...

                    //Save the list to the file.
                    CsvUtility.WriteScoresToTextFile(playerScores, filePath);

                    //Tell the user the record has been added.
                    DisplaySuccessMessage("All records have been cleared.");
                    DisplayContinuePrompt();
                }
                else
                {
                    //If the user response is no...

                    
                }




                ////Save the list to the file.
                //CsvUtility.WriteScoresToTextFile(playerScores, filePath);

                ////Tell the user the record has been added.
                //DisplaySuccessMessage("All records have been cleared.");
                //DisplayContinuePrompt();
            }
            catch (Exception e)
            {
                //Display an error message for the specific error.
                CatchIOExceptions(e);

                //Return to the main menu.
                return;
            }
        }

        /// <summary>
        /// Deletes the player name and score for a specific player in the file.
        /// </summary>
        /// <param name="filePath"></param>
        private static void DeletePlayerScore(string filePath)
        {
            //Variable Declarations.
            List<Scores> playerScores = new List<Scores>();
            Scores playerToDelete = new Scores();

            try
            {
                //Read the records from the file.
                playerScores = CsvUtility.ReadScoresFromTextFile(filePath);
            }
            catch (Exception e)
            {
                //Display an error message for the specific error.
                CatchIOExceptions(e);

                //Return to the main menu.
                return;
            }

            //Display the Record(s) on the screen.
            DisplayHeader("Delete Player Score");

            //Get the player name to delete from the user.
            playerToDelete.PlayerName = GetUserResponse("Enter the name of the player to delete:");

            //Remove the player entered by the user from the list.
            for (int i = 0; i < playerScores.Count; i++)
            {
                if (playerScores[i].PlayerName == playerToDelete.PlayerName)
                {
                    playerScores.RemoveAt(i);
                }
            }

            //Save the remaining list of player names and scores to the file.
            try
            {
                //Save the new list of scores to the file.
                CsvUtility.WriteScoresToTextFile(playerScores, filePath);

                //Tell the user the record has been deleted.
                DisplaySuccessMessage("The Record has been Deleted.");
                DisplayContinuePrompt();
            }
            catch (Exception e)
            {
                //Display an error message for the specific error.
                CatchIOExceptions(e);

                //Return to the main menu.
                return;
            }

        }

        /// <summary>
        /// Reads all records from a text file and displays them on the screen.
        /// </summary>
        /// <param name="filePath"></param>
        private static void DisplayAllRecords(string filePath)
        {
            //Variable Declarations.
            List<Scores> playerScores = new List<Scores>();
            
            try
            {
                //Read the records from the file.
                playerScores = CsvUtility.ReadScoresFromTextFile(filePath);
            }
            catch (Exception e)
            {
                //Display an error message for the specific error.
                CatchIOExceptions(e);

                //Return to the main menu.
                return;
            }

            //Display the Record(s) on the screen.
            DisplayHeader("Display All Player Scores");
            DisplayPlayerScores(playerScores);

            //Display the continue prompt.
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Displays a message on the screen before the application exits.
        /// </summary>
        static void DisplayClosingScreen()
        {
            //Display the closing message on the screen.
            Console.Clear();
            Console.WriteLine("Thank you for using the Text file reader application by Bird");
            Console.WriteLine("Brain International.  For future applications go to");
            Console.WriteLine("www.birdbrain.com");

            //Display the continue prompt.
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Hold the execution of the program and prompts the user to press any key to continue.
        /// </summary>
        static void DisplayContinuePrompt()
        {
            //Display continue prompt.
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// Displays an error message on the screen.
        /// </summary>
        /// <param name="errorText"></param>
        static void DisplayErrorMessage(String errorText)
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
        /// Displays header information at the top of the screen.
        /// </summary>
        /// <param name="headerText"></param>
        static void DisplayHeader(String headerText)
        {
            //Display the header text.
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine(headerText);
            Console.WriteLine("");
        }

        /// <summary>
        /// Displays the player name and player's score that was read from a text file.
        /// </summary>
        /// <param name="scoreList"></param>
        static void DisplayPlayerScores(List<Scores> scoreList)
        {
            foreach (Scores player in scoreList)
            {
                Console.WriteLine("Player: {0}\tScore: {1}", player.PlayerName, player.PlayerScore);
            }
        }

        /// <summary>
        /// Displays the main menu on the screen.
        /// </summary>
        static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. Display All Records");
            Console.WriteLine("2. Add a Record");
            Console.WriteLine("3. Delete a Record");
            Console.WriteLine("4. Update a Record");
            Console.WriteLine("5. Clear all Records");
            Console.WriteLine("6. Exit");
        }

        /// <summary>
        /// Displays a message on the screen as the application is beginning.
        /// </summary>
        static void DisplayOpeningScreen()
        {
            Console.WriteLine("Welcome to Bird Brain International's Text File Reader Application.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Displays a message on the screen that an operation has been completed successfully.
        /// </summary>
        /// <param name="message"></param>
        static void DisplaySuccessMessage(String message)
        {
            //Display the error message to the screen.
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(message);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Display a Yes or No prompt with a message
        /// </summary>
        /// <param name="promptMessage">prompt message</param>
        /// <returns>bool where true = yes</returns>
        private static bool DisplayGetYesNoPrompt(string promptMessage)
        {
            bool yesNoChoice = false;
            bool validResponse = false;
            string userResponse;

            while (!validResponse)
            {
                //ConsoleUtil.DisplayReset();

                //ConsoleUtil.DisplayPromptMessage(promptMessage + "(yes/no)");
                //userResponse = Console.ReadLine();

                userResponse = GetUserResponse(promptMessage + "(yes/no)");

                if (userResponse.ToUpper() == "YES")
                {
                    validResponse = true;
                    yesNoChoice = true;
                }
                else if (userResponse.ToUpper() == "NO")
                {
                    validResponse = true;
                    yesNoChoice = false;
                }
                else
                {
                    //ConsoleUtil.DisplayMessage(
                    //    "It appears that you have entered an incorrect response." +
                    //    " Please enter either \"yes\" or \"no\"."
                    //    );

                    
                    DisplayErrorMessage("It appears that you have entered an incorrect response.  Please enter either \"yes\" or \"no\".");


                    DisplayContinuePrompt();
                }
            }

            return yesNoChoice;
        }

        /// <summary>
        /// Gets an input from the user.
        /// </summary>
        /// <returns></returns>
        private static string GetUserResponse(string prompt)
        {
            Console.WriteLine();
            Console.Write($"{prompt} ");

            //Return the user's menu choice.
            return Console.ReadLine();
        }

        static List<Scores> InitializeListOfHighScores()
        {
            List<Scores> highScoresClassList = new List<Scores>();

            // initialize the IList of high scores - note: no instantiation for an interface
            highScoresClassList.Add(new Scores() { PlayerName = "John", PlayerScore = 1296 });
            highScoresClassList.Add(new Scores() { PlayerName = "Joan", PlayerScore = 345 });
            highScoresClassList.Add(new Scores() { PlayerName = "Jeff", PlayerScore = 867 });
            highScoresClassList.Add(new Scores() { PlayerName = "Charlie", PlayerScore = 2309 });

            return highScoresClassList;
        }

        static void ObjectListReadWrite(string dataFile)
        {
            //List<HighScore> highScoresClassListWrite = new List<HighScore>();

            //List<string> highScoresStringListRead = new List<string>(); ;
            //List<HighScore> highScoresClassListRead = new List<HighScore>(); ;

            //string highScoreString;

            //// initialize a list of HighScore objects
            //highScoresClassListWrite = InitializeListOfHighScores();

            //Console.WriteLine("The following high scores will be added to Data.txt.\n");
            //// display list of high scores objects
            //DisplayHighScores(highScoresClassListWrite);

            //Console.WriteLine("\nAdd high scores to text file. Press any key to continue.\n");
            //Console.ReadKey();

            //// build the list of strings and write to the text file line by line
            //WriteHighScoresToTextFile(highScoresClassListWrite, dataFile);

            //Console.WriteLine("High scores added successfully.\n");

            //Console.WriteLine("Read into a string of HighScore and display the high scores from data file. Press any key to continue.\n");
            //Console.ReadKey();


            //// build the list of HighScore class objects from the list of strings
            //highScoresClassListRead = ReadHighScoresFromTextFile(dataFile);

            //// display list of high scores objects
            //DisplayHighScores(highScoresClassListRead);
        }

        /// <summary>
        /// Updates the score for a specific player in the file.
        /// </summary>
        /// <param name="filePath"></param>
        private static void UpdatePlayerScore(string filePath)
        {
            //Variable Declarations.
            int newScore = 0;
            List<Scores> playerScores = new List<Scores>();
            Scores playerToUpdate = new Scores();

            try
            {
                //Read the records from the file.
                playerScores = CsvUtility.ReadScoresFromTextFile(filePath);
            }
            catch (Exception e)
            {
                //Display an error message for the specific error.
                CatchIOExceptions(e);

                //Return to the main menu.
                return;
            }

            //Display the Record(s) on the screen.
            DisplayHeader("Update Player Score");

            //Get the player name to update from the user.
            playerToUpdate.PlayerName = GetUserResponse("Enter the name of the player to update:");

            //Get the new score from the user.
            while (!int.TryParse(GetUserResponse("Enter the player's score:"), out newScore))
            {
                //If the numer entered is not a valid integer...

                //Display an error message
                DisplayErrorMessage("You have entered an invalid integer.  Press any key to re-enter.");
                Console.ReadKey();

                //Re-dislay the header.
                DisplayHeader("Add New Player Score");
            }

            //Remove the player entered by the user from the list.
            for (int i = 0; i < playerScores.Count; i++)
            {
                if (playerScores[i].PlayerName == playerToUpdate.PlayerName)
                {
                    playerScores[i].PlayerScore = newScore;
                }
            }

            //Save the remaining list of player names and scores to the file.
            try
            {
                //Save the new list of scores to the file.
                CsvUtility.WriteScoresToTextFile(playerScores, filePath);

                //Tell the user the record has been updated.
                DisplaySuccessMessage("The Record has been Updated.");
                DisplayContinuePrompt();
            }
            catch (Exception e)
            {
                //Display an error message for the specific error.
                CatchIOExceptions(e);

                //Return to the main menu.
                return;
            }

        }
        */
    }
}
