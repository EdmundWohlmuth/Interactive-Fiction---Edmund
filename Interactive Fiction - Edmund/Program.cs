using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Monster_Hunter__An_Interactive_Story
{
    class Program
    {
        //decleration

        static int pageNum; // element that determins page to be displayed

        static string[] story;
        static string[] splitText;

        static int playerChoiceA;  // response A
        static int playerChoiceB;  // response B

        static string selection;
        static string textToSplit;
        static string saveData;

        static string SourceData;
        static byte[] tmpSource;
        static byte[] tmpHash;

        static string savePath = @"save.txt";
        static string storyPath = @"story.txt";      

        static bool isGameOver;
        static bool quitGame;

        static void Main(string[] args)
        {
            // initilization

            pageNum = 0;
            story = File.ReadAllLines(@"story.txt");

            quitGame = false;

            // ------------------------------------------------------------------------

            HashCheck();
            SaveGameInit();

            while (isGameOver == false)
            {
                MainMenu();

                //gameplay loop
                while (isGameOver == false) //gameloop (while not game over / quiting the game and is past Main Menu
                {
                    ErrorChecking(); // Checks to see if pageNum is in range
                    HasDelimiters(); // checks if page is an ending or not
                    PrintPageText(); // writes text                   
                    UserSelection(); // determins player decision
                }

                Console.Clear();

                if (quitGame == false)
                {
                    isGameOver = false;
                }                              
            }
        }

            // ------------------------- ERROR CHECK / HASH ------------------------------

        static void ErrorChecking()
        {
            if (!File.Exists(storyPath)) // checks if file exists
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR - story.txt not found. Press any key to quit.");
                Console.ReadKey(true);

                quitGame = true;
                isGameOver = true;
            }

            if (story[pageNum].Length == 0) // ensures page isn't empty
            {
                isGameOver = true;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR - String " + pageNum + " is empty. Press any key to quit.");
                pageNum = 0;
                Console.ReadKey(true);

                quitGame = true;
                isGameOver = true;
            }

            if (pageNum > story.Length || pageNum < story.Length - story.Length) // checks if story is witthin array range
            {
                Console.Clear();
                pageNum = 0;
                isGameOver = true;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR - Page Number is outside the established range of the story. Press any key to quit to menu");
                Console.ReadKey(true);

                quitGame = true;
                isGameOver = true;
            }

            HashCheck();
        }

        static void HashCheck() // checks to see if HASH is valid - locks down any unauthorised changes to story.txt
        {           
            SourceData = File.ReadAllText(storyPath);
            tmpSource = ASCIIEncoding.ASCII.GetBytes(SourceData);
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource); 

            if (ByteArrayToString(tmpHash) != "6E8EC0C5E2091FBC90C5AB3613756494") // hard coded but within scope of project
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR - story.txt is corrupted. Press any key to quit");
                Console.ReadKey(true);

                quitGame = true;
                isGameOver = true;
            }
        }

        static string ByteArrayToString(byte[] arrInput) // creates HASH
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
            // -------------- GAMEPLAY LOOP ------------------------------------

        static void HasDelimiters() // checks to see if string has dilimiters
        {
            if (story[pageNum].Contains(";"))
            {
                SplitText();
            }          
            else
            {
                isGameOver = true;
            }
        }

        static void SplitText() // splits PlotText(); into readable text && decision values
        {
            textToSplit = story[pageNum];

            textToSplit.Split(';'); // splits string into new strings on ';' characters
            splitText = textToSplit.Split(';'); // creates an array of strngs based off of textToSplit

            int.TryParse(splitText[splitText.Length - 2], out playerChoiceA); // Checks the last two strings to be used in page navigation - errors blocked by hash code
            int.TryParse(splitText[splitText.Length - 1], out playerChoiceB); // ---------------------------------------------------------------------------------------
        }

        static void PrintPageText() // prints the story and options
        {
            Console.ForegroundColor = ConsoleColor.White;

            if (pageNum == 0)
            {
                Console.WriteLine("Introduction");
            }
            else
            {
                Console.WriteLine("Page " + pageNum); // Display page number
            }

            Console.ForegroundColor = ConsoleColor.Cyan;

            if (isGameOver)
            {
                Print(story[pageNum]);
            }
            else
            {
                for (int i = 0; i < splitText.Length - 2; i++)
                {
                    Print(splitText[i]);
                }

            }            
        }

        static void UserSelection() // switich statement for player choice **Decided to keep the enter key and press enter style system as personal choice
        {
            selection = Console.ReadLine();

            if (!isGameOver)
            {
                switch (selection)
                {
                    case "1":

                        pageNum = playerChoiceA;
                        Console.Clear();

                        break;

                    case "2":

                        pageNum = playerChoiceB;
                        Console.Clear();

                        break;

                    case "3":

                        SaveGame();
                        Console.Clear();
                        Console.WriteLine("Game Saved!");

                        break;

                    case "4":

                        isGameOver = true;

                        break;

                    default: // basically the else statement

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(selection + " not recognised, Press any key to try again");
                        Console.ReadKey();
                        Console.Clear();

                        break;
                }
            }           
        }

            // ---------------------------- TEXT DISPLAY ------------------------------------

        public static void Print(string text, int delay = 25) // slowly prints out text
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

            // ----------------------------- SAVE GAME --------------------------------------

        static void SaveGame() //checks to see if file exists and saves
        {
            if (File.Exists(savePath))
            {
                saveData = pageNum.ToString();
                File.WriteAllText(savePath, saveData);
            }
            else
            {
                SaveGameInit();
            }           
        }

        static void SaveGameInit() // runs at start to check if save file is present as well as if save file is not present, it creates a new save.txt
        {
            if (!File.Exists(savePath))
            {
                saveData = pageNum.ToString();
                File.WriteAllText(savePath, saveData);
            }
        }

            // ------------------------- MAIN MENU ----------------------------------------

        static void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                            _                                      _              ");
            Console.WriteLine("  /\\/\\    ___   _ __   ___ | |_  ___  _ __    /\\  /\\ _   _  _ __  | |_  ___  _ __ ");
            Console.WriteLine(" /    \\  / _ \\ | '_ \\ / __|| __|/ _ \\| '__|  / /_/ /| | | || '_ \\ | __|/ _ \\| '__|");
            Console.WriteLine("/ /\\/\\ \\| (_) || | | |\\__ \\| |_|  __/| |    / __  / | |_| || | | || |_|  __/| |   ");
            Console.WriteLine("\\/    \\/ \\___/ |_| |_||___/ \\__|\\___||_|    \\/ /_/   \\__,_||_| |_| \\__|\\___||_|   ");
            Console.WriteLine("==================================================================================");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Press 1 to start\nPress 2 to continue\nPress 3 to exit");
            Console.WriteLine(" ");
            Console.WriteLine(" An interactive story By: Edmund Wohlmuth");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("'Monster Hunter' is a game series developed and published by CAPCOM");
            Console.ForegroundColor = ConsoleColor.White;
            
            MenuSelect();
        }

        static void MenuSelect() // menu selection for player input
        {
            string menuSelect = Console.ReadLine();

            switch (menuSelect)
            {
                case "1":

                    pageNum = 0;
                    Console.Clear();
                    break;

                case "2":

                    Console.Clear();
                    saveData = File.ReadAllText(savePath);
                    pageNum = int.Parse(saveData);

                    break;

                case "3":

                    quitGame = true;
                    isGameOver = true;

                    break;

                default:

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(menuSelect + " Not recognised, Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    isGameOver = true;

                    break;
            }
        }
    }
}
