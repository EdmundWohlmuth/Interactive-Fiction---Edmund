using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactive_Fiction___Edmund
{
    class Program
    {
        static int storyLength = 3;

        //decleration
        static string[] story = new string[storyLength]; // Stores the story / title & pages / 
        static string[] ChoiceA = new string[storyLength]; // response A
        static string[] ChoiceB = new string[storyLength]; // response B

        static string selection;
        static int pageNum; // element that determins page to be displayed

        static bool isGameOver;

        static void Main(string[] args)
        {
            pageNum = 0;

            //gameplay loop
            while (isGameOver == false) //gameloop
            {                 
                PlotText();
                Console.WriteLine(story[pageNum]);
                Console.WriteLine(" ");
                UserChoice();

                Console.ReadKey(true);                  

            }

            Console.ReadKey(true);
        }

        static void PlotText()
        {
            DecisionText(); // declare decisions

            Console.WriteLine("Page " + pageNum); // display Page number

            // Story Text blocks
            story[0] = "I am introducing you to the game! " + System.Environment.NewLine + ChoiceA[pageNum] + System.Environment.NewLine + ChoiceB[pageNum];
            story[1] = "Hello World!";
            story[2] = "Hello again!";

            
            
        }

        static void DecisionText() // What the play can choose
        {
            ChoiceA[0] = "Choice 1 (Page 1)"; // will differ depending on context (cross river, climb tree, etc.)
            ChoiceB[0] = "Choice 2 (Page 2)";

            ChoiceA[1] = "Choice 1";
            ChoiceB[1] = "Choice 2";

            ChoiceA[2] = "Choice 1";
            ChoiceB[2] = "Choice 2";
        }

        static void UserChoice()
        {
            Console.WriteLine("What Will you do?");

            selection = Console.ReadLine();

            switch (selection) // find a way to make this dynamic to account for diffrent awnsers depending on the page
            {
                case "1":
                    pageNum = 1; // TEMP - find a way to, depending on pageNum, set current page correctly.
                    break;

                case "2":
                    PageSelect2();
                    break;

                case "3":

                    break;

                case "4":

                    break;

                default: // basically the else statement
                    Console.WriteLine(selection + " not recognised, choose 1 - 4 to continue");
                    UserChoice();
                    break;
            }
        }

        static void PageSelect1()
        {

        }

        static void PageSelect2()
        {
            
        }

    }
}
