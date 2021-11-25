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

        static int storyLength = 10;
       
        static string[] story = new string[storyLength]; // Stores the story / title & pages / 

        static string[] failureText = new string[storyLength]; // stores fail conditions that can be edited
        static string[] splitText;

        static int ChoiceA; // response A
        static int ChoiceB; // response B

        static string selection;
        static string textToSplit;
        static string textToPrint;

        static int pageNum; // element that determins page to be displayed

        static bool isGameOver;
        static bool isFailState;

        static void Main(string[] args)
        {
            pageNum = 0;

            //gameplay loop
            while (isGameOver == false) //gameloop
            {               
                PlotText(); // establishes text to write
                SplitText(); // splits PlotText(); into readable text && decision values
                Console.WriteLine(); // writes the story text and decisions
                Console.Write(splitText);
                UserChoice(); // determins player decision

                Console.ReadKey(true);                  
            }

            Console.ReadKey(true);
        }

        static void PlotText()
        {          
            Console.WriteLine("Page " + pageNum); // display Page number

            // Story Text blocks
            story[0] = "This is text that explains nonsense, What will you do? \n 1: Walk Left \n 2: Walk Right \n 1;3";
            story[1] = "I hate this \n What will you do? \n 1: Walk Left \n 2: Walk Right \n 2;0";
            story[2] = "But this stuff is pretty cool \n What will you do? \n 1: Walk Left \n 2: Walk Right \n 4;6";
            story[3] = "Some new nonsense approaches \n What will you do? \n 1: Walk Left \n 2: Walk Right \n 5;6";
            story[4] = "AHHHHHHHHHHHHHHHH \n What will you do? \n 1: Walk Left \n 2: Walk Right \n 1;3";
            story[5] = "This is text that explains nonsense \n What will you do? \n 1: Walk Left \n 2: Walk Right \n 1;3";
            story[6] = "This is text that explains nonsense \n What will you do? \n 1: Walk Left \n 2: Walk Right \n 1;3";
            story[7] = "This is text that explains nonsense \n What will you do? \n 1: Walk Left \n 2: Walk Right \n 1;3";
            story[8] = "This is text that explains nonsense \n What will you do? \n 1: Walk Left \n 2: Walk Right \n 1;3";
            story[9] = "This is text that explains nonsense \n What will you do? \n 1: Walk Left \n 2: Walk Right \n 1;3";
        }  

        static void UserChoice()
        {

            selection = Console.ReadLine();

            ChoiceA = int.Parse(splitText[0]);
            ChoiceB = int.Parse(splitText[1]);

            switch (selection) // find a way to make this dynamic to account for diffrent awnsers depending on the page
            {
                case "1":

                    pageNum = ChoiceA;

                    if (pageNum >= 10)
                    {
                        pageNum = 10;
                    }                    
                    break;

                case "2":

                    pageNum = ChoiceB;

                    if (pageNum >= 10)
                    {
                        pageNum = 10;
                    }
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
            failureText[0] = "Text";
            failureText[1] = "Text";
            failureText[2] = "You've Died";
            failureText[3] = "text";
            failureText[4] = "text";
            failureText[5] = "text";
            failureText[6] = "You've died but painfully";
            failureText[7] = "text";
            failureText[8] = "text";
            failureText[9] = "You've died but diffrently";

            Console.WriteLine(" ");
            Console.WriteLine(failureText[pageNum]);
            
            isGameOver = true;
        }

        static void SplitText()
        {
            textToSplit = story[pageNum];

            textToSplit.Split(';'); // splits string into new strings on '_' characters
            splitText = textToSplit.Split(';'); // creates an array of strngs based off of textToSplit            
        }
    }
}
