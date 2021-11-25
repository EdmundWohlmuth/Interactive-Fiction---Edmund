using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactive_Fiction___Edmund
{
    class Program
    {
        //decleration

        static int storyLength = 15;
        static int pageNum; // element that determins page to be displayed

        static string[] story = new string[storyLength]; // Stores the story / title & pages
        static string[] splitText;

        static int playerChoiceA;  // response A
        static int playerChoiceB;  // response B

        static string selection;
        static string textToSplit;

        static bool isGameOver;
        static bool isPlaying;

        static void Main(string[] args)
        {
            pageNum = 0;
            isPlaying = false;

            MainMenu();

                //gameplay loop
                while (isGameOver == false && isPlaying) //gameloop (while not game over and is past Main Menu
                {
                    PlotText(); // establishes text to write
                    SplitText(); // splits PlotText(); into readable text && decision values                 
                    Console.Write(splitText[0] + "\n" + splitText[1] + "\n" + splitText[2] + "\n");
                    UserChoice(); // determins player decision                  
                }         
            Console.ReadKey(true);
        }

        static void PlotText()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Page " + pageNum); // display Page number

            Console.ForegroundColor = ConsoleColor.Cyan;

            // Story Text blocks
            story[0] = "This is text that explains nonsense;1: Walk Left;2: Walk Right;1;3";
            story[1] = "I hate this;1: Walk Left;2: Walk Right;2;0";
            story[2] = "But this stuff is pretty cool;1: Walk Left;2: Walk Right;4;6";
            story[3] = "Some new nonsense approaches;1: Walk Left;2: Walk Right;5;6";
            story[4] = "AHHHHHHHHHHHHHHHH;1: Walk Left;2: Walk Right;1;3";
            story[5] = "This is text that explains nonsense;1: Walk Left;2: Walk Right;1;3";
            story[6] = "This is text that explains nonsense;1: Walk Left;2: Walk Right;10;12";
            story[7] = "This is text that explains nonsense;1: Walk Left;2: Walk Right;1;3";
            story[8] = "This is text that explains nonsense;1: Walk Left;2: Walk Right;1;3";
            story[9] = "This is text that explains nonsense;1: Walk Left;2: Walk Right;1;14";
            story[10] = "This is text that explains nonsense;1: Walk Left;2: Walk Right;9;13";
            story[11] = "This is text that explains nonsense;1: Walk Left;2: Walk Right;1;3";
            story[12] = "This is a game over;1: Restart;2: Quit;0;12"; // game over
            story[13] = "This is win #1;1: Restart;2: Quit;0;13"; // win 1
            story[14] = "This is win #2;1: Restart;2: Quit;0;14"; // win 2


        }  

        static void UserChoice()
        {
            selection = Console.ReadLine();

            switch (selection) // find a way to make this dynamic to account for diffrent awnsers depending on the page
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
                    Console.WriteLine("Temp text, would normally save game");
                    UserChoice();
                    break;

                case "4":
                    Console.WriteLine("Temp text, would normally quit game");
                    break;

                default: // basically the else statement
                    Console.WriteLine(selection + " not recognised, Type 1 - 4 to continue");
                    UserChoice();
                    break;
            }
        }
        static void SplitText()
        {
            textToSplit = story[pageNum];

            textToSplit.Split(';'); // splits string into new strings on ';' characters
            splitText = textToSplit.Split(';'); // creates an array of strngs based off of textToSplit

            playerChoiceA = int.Parse(splitText[3]);
            playerChoiceB = int.Parse(splitText[4]);
        }


        static void MainMenu()
        {
            //  insert ASCII text title screen here

            Console.WriteLine("Title Screen!");
            Console.WriteLine("==============");
            Console.WriteLine("Press 1 to start\nPress 2 to continue\nPress 3 to exit");

            MenuSelect();

        }

        static void MenuSelect()
        {
            string menuSelect = Console.ReadLine();

            switch (menuSelect)
            {
                case "1":
                    isPlaying = true;
                    Console.Clear();
                    break;

                case "2":
                    Console.WriteLine("Temp text, will eventually load a save game");
                    MainMenu();
                    break;

                case "3":
                    Console.WriteLine("Temp text, will eventually exit the program");
                    break;

                default:
                    Console.WriteLine(menuSelect + " Not recognised");
                    MainMenu();
                    break;
            }
        }
    }
}
