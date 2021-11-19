using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactive_Fiction___Edmund
{
    class Program
    {
        static int storyLength = 10;

        //decleration
        static string[] story = new string[storyLength]; // Stores the story / title & pages / 

        static string[] failureText = new string[storyLength]; // stores fail conditions that can be edited

        static string[] ChoiceA = new string[storyLength]; // response A
        static string[] ChoiceB = new string[storyLength]; // response B

        static string selection;
        static int pageNum; // element that determins page to be displayed

        static bool isGameOver;
        static bool isFailState;

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
            story[1] = "Hello World! " + System.Environment.NewLine + ChoiceA[pageNum] + System.Environment.NewLine + ChoiceB[pageNum]; ;
            story[2] = "Hello again! " + System.Environment.NewLine + ChoiceA[pageNum] + System.Environment.NewLine + ChoiceB[pageNum]; ;
            story[3] = "text " + System.Environment.NewLine + ChoiceA[pageNum] + System.Environment.NewLine + ChoiceB[pageNum]; ;
            story[4] = "text " + System.Environment.NewLine + ChoiceA[pageNum] + System.Environment.NewLine + ChoiceB[pageNum]; ;
            story[5] = "text " + System.Environment.NewLine + ChoiceA[pageNum] + System.Environment.NewLine + ChoiceB[pageNum]; ;
            story[6] = "text " + System.Environment.NewLine + ChoiceA[pageNum] + System.Environment.NewLine + ChoiceB[pageNum]; ;
            story[7] = "text " + System.Environment.NewLine + ChoiceA[pageNum] + System.Environment.NewLine + ChoiceB[pageNum]; ;
            story[8] = "text " + System.Environment.NewLine + ChoiceA[pageNum] + System.Environment.NewLine + ChoiceB[pageNum]; ;
            story[9] = "text " + System.Environment.NewLine + ChoiceA[pageNum] + System.Environment.NewLine + ChoiceB[pageNum]; ;
        }

        static void DecisionText() // What the play can choose
        {
            ChoiceA[0] = "Choice 1 (Page 1)"; // will differ depending on context (cross river, climb tree, etc.)
            ChoiceB[0] = "Choice 2 (Page 2)";

            ChoiceA[1] = "Choice 1 (Page 2)";
            ChoiceB[1] = "Choice 2 (Page 3)";

            ChoiceA[2] = "Choice 1 (Page 3)";
            ChoiceB[2] = "Choice 2 (Page 4)";

            ChoiceA[3] = "Choice 1 (Page 4)";
            ChoiceB[3] = "Choice 2 (Page 5)";

            ChoiceA[4] = "Choice 1 (Page 5)";
            ChoiceB[4] = "Choice 2 (Page 6)";

            ChoiceA[5] = "Choice 1 (Page 6)";
            ChoiceB[5] = "Choice 2 (Page 7)";

            ChoiceA[6] = "Choice 1 (Page 7)";
            ChoiceB[6] = "Choice 2 (Page 8)";

            ChoiceA[7] = "Choice 1 (Page 8)";
            ChoiceB[7] = "Choice 2 (Page 9)";

            ChoiceA[8] = "Choice 1 (Page 9)";
            ChoiceB[8] = "Choice 2 (Page 10)";

            ChoiceA[9] = "Choice 1 (Page 10)";
            ChoiceB[9] = "Choice 2 (Page 11)";
        }

        static void UserChoice()
        {
            Console.WriteLine("What Will you do?");

            selection = Console.ReadLine();

            switch (selection) // find a way to make this dynamic to account for diffrent awnsers depending on the page
            {
                case "1":
                    pageNum = pageNum + 1;
                    PageSelect();
                    break;

                case "2":
                    pageNum = pageNum + 2;
                    PageSelect();
                    break;

                case "3":
                    Console.WriteLine("Temp text, would normally save game");
                    break;

                case "4":
                    Console.WriteLine("Temp text, would normally quit game");
                    break;

                default: // basically the else statement
                    Console.WriteLine(selection + " not recognised, choose 1 - 4 to continue");
                    UserChoice();
                    break;
            }
        }

        static void PageSelect()
        {
            if (pageNum == 2 || pageNum == 6 || pageNum == 9) // maybe turn the ints into a list that is called???
            {
                isFailState = true;                
                FailText();
            }
            else
            {

            }
        }

        static void FailText()
        {           
            failureText[0] = "You've died";
            failureText[1] = "You've died but diffrent";
            failureText[2] = "You've died but painfully";
            failureText[3] = "text";
            failureText[4] = "text";
            failureText[5] = "text";
            failureText[6] = "text";
            failureText[7] = "text";
            failureText[8] = "text";
            failureText[9] = "text";
            
            isGameOver = true;
        }

    }
}
