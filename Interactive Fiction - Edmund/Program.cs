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

        static int storyLength = 16;
        static int pageNum; // element that determins page to be displayed

        static string[] story = new string[storyLength]; // Stores the story / title & pages
        static string[] splitText;

        static int playerChoiceA;  // response A
        static int playerChoiceB;  // response B

        static string selection;
        static string textToSplit;

        static bool isGameOver;

        static void Main(string[] args)
        {
            pageNum = 0;

            MainMenu();      

                //gameplay loop
                while (isGameOver == false) //gameloop (while not game over and is past Main Menu
                {
                    PlotText(); // establishes text to write
                    CheckText(); // checks if page is an ending or not
                    PageText(); // writes text                   
                    UserChoice(); // determins player decision
                }
                // this is a test to see if this is the real repository
                }         
        }

        static void PlotText()
        {
            // Story Text blocks
            story[0] = "You are a Monster Hunter, and on a routine hunt you've been ambushed by a Rathian!;Press 1 or 2 to progress the story;Press 3 to save, and 4 to Quit;1;1";
            story[1] = "The large fire-breathing wyvern bears upon you!;1: Parry and retreat;2: Roll to the left;2;4";
            story[2] = "You swing your sword in an arch, fire singeing your armor;1: Heavy overhead swing at the Rathian;2: Riposte and sidestep;3;4";
            story[3] = "You prepare a heavy swing, but the Rathian is faster, a fireball strikes you in the chest and you're on the ground!;1: Get up!;2: Roll left!;5;7";
            story[4] = "Your evasive moves avoid a dangerous strike, and you prepare a heavy hit in return;1: Strike the head;2: Go for the legs;3;6";
            story[5] = "The time it takes for you too get up is not fast enough, as the Rathian's toothy maw closes down on you. You've Died"; // death 1
            story[6] = "You swing for the legs knocking it down!;1: Follow up with a strike to its head;2: Climb the Rathian to move to its blind spot;3;8";
            story[7] = "You roll to the side as the rathian stomps down on where you used to be, standing now you make your move;1: Stand your ground;2: Take a swing at the Rathian;1;2";
            story[8] = "You've climbed to the Rathian's back as it rights its self and begins to fly away, with you on its back!;1: Jump off before its too late;2: Hang on!;14;9";
            story[9] = "You hunker down and hang on as the Rathian flys higher and higher;1: Jump off;2: Hang on;14;10";
            story[10] = "The Rathian reaches its crusing altitude, and you are comfortable enough to manuever;1: Climb to the Head;2: Climb to the wings;11;12";
            story[11] = "You've reached the Head of the Rathian, despite its attempts to shake you off you hang on, you could reach for your main weapon, as it would ensure a kill, but is sacrificing your stability worth it?;1: Reach for your main weapon;2: Reach for your knife;13;15";
            story[12] = "The area around the wings is alot harder to keep your grip on, but if you could just get a little closer, you might be able to bring the beat down...;1: Continue to the wings;2:Go to the head instead;13;11";
            story[13] = "The flight becomes too much and your grip on the Rathian releases, its a long, long fall to the ground, and you think there's no way to walk away from this as the ground meets your face and sudden darkness envelopes your world. You've died!"; // death 2
            story[14] = "You jump off the Rathian, as it flys up high and away. The End."; // win 1
            story[15] = "With your knife drawn you stab the Rathian in the eye, and using its body as a brace you survive the landing, resulting in an unexpected hunt to be sure, but a welcome one. The End."; // win 2

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
                        Console.WriteLine("Temp text would save game");
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


        static void MainMenu()
        {
            //  insert ASCII text title screen here

            Console.WriteLine("Monster Hunter! - An interactive story.");
            Console.WriteLine("==============");
            Console.WriteLine("Press 1 to start\nPress 2 to continue\nPress 3 to exit");
            Console.WriteLine(" ");
            Console.WriteLine("By: Edmund Wohlmuth");
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
                    Console.Clear();
                    break;

                case "2":
                    Console.WriteLine("Temp text, will eventually load a save game");
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
