using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Interactive_Fiction___Edmund
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
        static string path = @"save.txt";

        static bool isGameOver;

        static void Main(string[] args)
        {
            // initilization

            pageNum = 0;
            story = File.ReadAllLines(@"story.txt");

            // ------------------------------------------------------------------------

            SaveGameInit();
            MainMenu();      

                //gameplay loop
                while (isGameOver == false) //gameloop (while not game over and is past Main Menu
                {
                    CheckText(); // checks if page is an ending or not
                    PageText(); // writes text                   
                    UserChoice(); // determins player decision
                }        
        }

        static void CheckText()
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

            playerChoiceA = int.Parse(splitText[3]);
            playerChoiceB = int.Parse(splitText[4]);
        }

        static void PageText()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Page " + pageNum); // display Page number (might make page 0 establishing text, and set it to read "establishing text" instead of page 0)
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (isGameOver)
            {
                Console.WriteLine(story[pageNum]);
            }
            else
            {
                Console.WriteLine(splitText[0]);
                Console.WriteLine(splitText[1]);
                Console.WriteLine(splitText[2]);
            }            
        }

        static void UserChoice()
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
                        Environment.Exit(0);
                        break;

                    default: // basically the else statement
                        Console.WriteLine(selection + " not recognised, Type 1 - 4 to continue");
                        break;
                }
            }           
        }

        static void SaveGame()
        {
            saveData = pageNum.ToString();
            File.WriteAllText(path, saveData);
        }

        static void SaveGameInit()
        {
            if (!File.Exists(path))
            {
                saveData = pageNum.ToString();
                File.WriteAllText(path, saveData);
            }
        }

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

                    saveData = File.ReadAllText(@"save.txt");
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
