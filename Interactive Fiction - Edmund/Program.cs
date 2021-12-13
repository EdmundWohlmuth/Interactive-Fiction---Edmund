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
        static string[] achivements;
        static string[] splitText;

        static int playerChoiceA;  // response A
        static int playerChoiceB;  // response B
        static int achiveInt;

        static string selection;
        static string textToSplit;
        static string saveData;

        static string SourceData;
        static byte[] HashBytes;
        static byte[] ToStringHash;

        static string savePath = @"save.txt";
        static string storyPath = @"story.txt"; 
        static string achivementsPath = @"achivement.txt";

        static string achivementList;
        static string achivementsCheck = "0";

        static bool endOne = false;
        static bool endTwo = false;
        static bool endThree = false;
        static bool endFour = false;

        static bool isGameOver;
        static bool quitGame;

        static void Main(string[] args)
        {
            // initilization

            pageNum = 0;
<<<<<<< Updated upstream
            story = File.ReadAllLines(@"story.txt");           
=======
            achiveInt = 0;
            story = File.ReadAllLines(storyPath);
            achivements = File.ReadAllLines(achivementsPath);

>>>>>>> Stashed changes

            quitGame = false;

            // ------------------------------------------------------------------------

            HashCheck();
            SaveGameInit();

            while (isGameOver == false)
            {
                AchivementsCheck();
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

            // ------------------------- ERROR CHECK ------------------------------

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
            // ----------------- HASH CODE --------------------------------------

        static void HashCheck() // checks to see if HASH is valid - locks down any unauthorised changes to story.txt
        {           
            SourceData = File.ReadAllText(storyPath);
            HashBytes = ASCIIEncoding.ASCII.GetBytes(SourceData);
            ToStringHash = new MD5CryptoServiceProvider().ComputeHash(HashBytes); 

            if (ByteArrayToString(ToStringHash) != "6E8EC0C5E2091FBC90C5AB3613756494") // hard coded but within scope of project
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
                AchivementsCheck();
                isGameOver = true;             
            }
        }

        static void SplitText() // splits PlotText(); into readable text && decision values
        {
            textToSplit = story[pageNum];

            textToSplit.Split(';'); // splits string into new strings on ';' characters
            splitText = textToSplit.Split(';'); // creates an array of strngs based off of textToSplit

            bool optionOneSuccess = int.TryParse(splitText[splitText.Length - 2], out playerChoiceA); // Checks the last two strings to be used in page navigation - errors blocked by hash code
            bool optionTwoSuccess = int.TryParse(splitText[splitText.Length - 1], out playerChoiceB); // ---------------------------------------------------------------------------------------

            if (!optionOneSuccess || !optionTwoSuccess)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                if (!optionOneSuccess)
                {
                    Console.WriteLine("ERROR - " + playerChoiceA + " is an invalid int, press any key to exit.");
                }
                else
                {
                    Console.WriteLine("ERROR - " + playerChoiceB + " is an invalid int, press any key to exit.");
                }

                isGameOver = true;
                quitGame = true;
            }
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

            // ------------------------- ACHIVEMENTS ---------------------------------------
        static void AchivementsCheck() // Hash codes are essentially passcodes that allow the achivements to persist
        {          
            achivementList = File.ReadAllText(achivementsPath);
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (pageNum == 5 && !achivements[0].Contains("0CC175B9C0F1B6A831C399E269772661"))
            {
                endOne = true;
                Console.WriteLine("Achivement unlocked: Lunch");
                Console.WriteLine();

                achivementsCheck = "a";
                achiveInt = 0;
                AchivementHash();
            }
            else if (achivements[0].Contains("0CC175B9C0F1B6A831C399E269772661"))
            {
                endOne = true;
            }

            if (pageNum == 14 && !achivements[1].Contains("92EB5FFEE6AE2FEC3AD71C777531578F"))
            {
                endTwo = true;                
                Console.WriteLine("Achivement unlocked: Survival");
                Console.WriteLine();

                achivementsCheck = "b";
                achiveInt = 1;
                AchivementHash();
            }
            else if (achivements[1].Contains("92EB5FFEE6AE2FEC3AD71C777531578F"))
            {
                endTwo = true;
            }

            if (pageNum == 13 && !achivements[2].Contains("4A8A08F09D37B73795649038408B5F33"))
            {
                endThree = true;
                Console.WriteLine("Achivement unlocked: A long way down");
                Console.WriteLine();

                achivementsCheck = "c";
                achiveInt = 2;
                AchivementHash();
            }
            else if (achivements[2].Contains("4A8A08F09D37B73795649038408B5F33"))
            {
                endThree = true;
            }

            if (pageNum == 15 && !achivements[3].Contains("8277E0910D750195B448797616E091AD"))
            {
                endFour = true;
                Console.WriteLine("Achivement unlocked: Monster Hunter");
                Console.WriteLine();

                achivementsCheck = "d";
                achiveInt = 3;
                AchivementHash();
            }
            else if (achivements[3].Contains("8277E0910D750195B448797616E091AD"))
            {
                endFour = true;
            }

<<<<<<< Updated upstream
            // run a new hash here to rewrite the old one?
                   
        }        
=======
                                  
        } 
        
        static void AchivementHash()
        {
            Byte[] AchiveBytes = ASCIIEncoding.ASCII.GetBytes(achivementsCheck);
            Byte[] AchiveHash = new MD5CryptoServiceProvider().ComputeHash(AchiveBytes);

            if (ByteArrayToString(AchiveHash) != "CFCD208495D565EF66E7DFF9F98764DA")
            {
                File.WriteAllText(achivements[achiveInt], ByteArrayToString(AchiveHash));
            }

            // There are better ways to do this yes, but this is a way I UNDERSTAND, I hope the hardcoding is witthin the scope of this project.
        }
>>>>>>> Stashed changes

        static void AchivementsMenu()
        {
            if (endOne == true)
            {
                Console.WriteLine("Lunch");
                Console.WriteLine("You had spirit but at the end of the day, the Rathian bested you and you're now no more than dragon food.");
                Console.WriteLine();
            }
            else if (endTwo == true)
            {
                Console.WriteLine("Survival");
                Console.WriteLine("Your Martial prowess surpassed the might of the Rathian and you forced it to flee! but maybe you could have brought it down...");
                Console.WriteLine();
            }
            else if (endThree == true)
            {
                Console.WriteLine("A long way down");
                Console.WriteLine("It was an inopportune time to fall of the Rathian, and now you're nothing more than a Monster Hunter Pancake.");
                Console.WriteLine();
            }
            else if (endFour == true)
            {
                Console.WriteLine("Monster Hunter");
                Console.WriteLine("Against all odds and one helluva ride you were able to take down the flying wyvern Rathian!");
                Console.WriteLine();
            }
            else
            {
                Print("You have not unlocked any achivements!\nPlay the game to aquire some achivements!");
                Console.WriteLine();
            }

            Print("Press any key to return to the Main Menu");
            Console.ReadKey(true);
            isGameOver = true;
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

            Console.WriteLine("Press 1 to start\nPress 2 to continue\nPress 3 to view Achivements\nPress 4 to exit");
            Console.WriteLine(" ");
            Print(" An interactive story By: Edmund Wohlmuth");
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

                    Console.Clear();
                    AchivementsMenu();

                    break;

                case "4":

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
