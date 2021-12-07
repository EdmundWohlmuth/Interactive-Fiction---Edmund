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
        static bool willQuitApp;

        static void Main(string[] args)
        {
            // initilization

            pageNum = 0;
            story = File.ReadAllLines(@"story.txt");

            willQuitApp = false;

            // ------------------------------------------------------------------------

            HashCheck();
            SaveGameInit();

            while (isGameOver == false || willQuitApp == true)
            {
                MainMenu();

                //gameplay loop
                while (isGameOver == false || willQuitApp == true) //gameloop (while not game over and is past Main Menu
                {
                    ErrorChecking(); // Checks to see if pageNum is in range
                    HasDelimiters(); // checks if page is an ending or not
                    PrintPageText(); // writes text                   
                    UserSelection(); // determins player decision
                }

                Console.Clear();
                isGameOver = false;
            }
        }

            // ------------------------- GAMEPLAYLOOP ------------------------------

        static void ErrorChecking()
        {
            if (!File.Exists(storyPath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR - story.txt not found. Press any key to quit.");
                Console.ReadKey(true);
                Environment.Exit(0);
            }

            if (story[pageNum].Length == 0)
            {
                isGameOver = true;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR - String " + pageNum + " is unfinished. Press any key to quit.");
                pageNum = 0;
                Console.ReadKey(true);
                Environment.Exit(0);
            }

            if (pageNum > story.Length || pageNum < story.Length - story.Length) // R E A L L Y  jank
            {
                Console.Clear();
                pageNum = 0;
                isGameOver = true;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR - Page Number is outside the established range of the story. Press any key to quit to menu");
                Console.ReadKey(true);
            }

            HashCheck();
        }

        static void HashCheck()
        {           
            SourceData = File.ReadAllText(storyPath);
            tmpSource = ASCIIEncoding.ASCII.GetBytes(SourceData);
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource); 

            if (ByteArrayToString(tmpHash) != "6E8EC0C5E2091FBC90C5AB3613756494")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR - story.txt is corrupted. Press any key to quit");
            }
        }

        static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        static void HasDelimiters()
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

            int.TryParse(splitText[splitText.Length - 2], out playerChoiceA); // make the two dilimiters story.Lenght - 2 or something, and use TryParse
            int.TryParse(splitText[splitText.Length - 1], out playerChoiceB); // -----------------------------------------------------------------------
        }

        static void PrintPageText()
        {
            Console.ForegroundColor = ConsoleColor.White;

            if (pageNum == 0)
            {
                Console.WriteLine("Introduction");
            }
            else
            {
                Console.WriteLine("Page " + pageNum); // display Page number (might make page 0 establishing text, and set it to read "establishing text" instead of page 0)
            }

            Console.ForegroundColor = ConsoleColor.Cyan;

            if (isGameOver)
            {
                Console.WriteLine(story[pageNum]);
            }
            else
            {
                for (int i = 0; i < splitText.Length - 2; i++)
                {
                    Console.WriteLine(splitText[i]);
                }

            }            
        }

        static void UserSelection()
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
                        Console.WriteLine(selection + " not recognised, Type 1 - 4 to continue");
                        break;
                }
            }           
        }

            // ----------------------------- SAVE GAME --------------------------------------

        static void SaveGame()
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

        static void SaveGameInit()
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

        static void MenuSelect()
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

                    Environment.Exit(0);

                    break;

                default:
                    Console.WriteLine(menuSelect + " Not recognised");
                    break;
            }
        }
    }
}
