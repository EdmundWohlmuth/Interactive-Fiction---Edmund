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
            EstablishingText();

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
            story[0] = "The large fire-breathing wyvern bears upon you!;1: Parry and retreat;2: Roll to the left;1;3";
            story[1] = "You swing your sword in an arch, fire singeing your armor;1: Heavy overhead swing at the Rathian;2: Riposte and sidestep;2;3";
            story[2] = "You prepare a heavy swing, but the Rathian is faster, a fireball strikes you in the chest and you're on the ground!;1: Get up!;2: Roll left!;4;6";
            story[3] = "Your evasive moves avoid a dangerous strike, and you prepare a heavy hit in return;1: Strike the head;2: Go for the legs;2;5";
            story[4] = "The time it takes for you too get up is not fast enough, as the Rathian's toothy maw closes down on you.;You've Died;Game Over!;4;4"; // death 1
            story[5] = "You swing for the legs knocking it down!;1: Follow up with a strike to its head;2: Climb the Rathian to move to its blind spot;2;7";
            story[6] = "You roll to the side as the rathian stomps down on where you used to be, standing now you make your move;1: Stand your ground;2: Take a swing at the Rathian;0;1";
            story[7] = "You've climbed to the Rathian's back as it rights its self and begins to fly away, with you on its back!;1: Jump off before its too late;2: Hang on!;13;8";
            story[8] = "You hunker down and hang on as the Rathian flys higher and higher;1: Jump off;2: Hang on;13;9";
            story[9] = "The Rathian reaches its crusing altitude, and you are comfortable enough to manuever;1: Climb to the Head;2: Climb to the wings;10;11";
            story[10] = "You've reached the Head of the Rathian, despite its attempts to shake you off you hang on, you could reach for your main weapon, as it would ensure a kill, but is sacrificing your stability worth it?;1: Reach for your main weapon;2: Reach for your knife;12;14";
            story[11] = "The area around the wings is alot harder to keep your grip on, but if you could just get a little closer, you might be able to bring the beat down...;1: Continue to the wings;2:Go to the head instead;12;10";
            story[12] = "The flight becomes too much and your grip on the Rathian releases, its a long, long fall to the ground, and you think there's no way to walk away from this as the ground meets your face and sudden darkness envelopes your world.;You've died!;2: Quit;0;12"; // death 2
            story[13] = "You jump off the Rathian, as it flys up high and away. The End.;1: Restart;2: Quit;0;13"; // win 1
            story[14] = "With your knife drawn you stab the Rathian in the eye, and using its body as a brace you survive the landing, resulting in an unexpected hunt to be sure, but a welcome one. The End.;1: Restart;2: Quit;0;14"; // win 2


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

        static void EstablishingText()
        {
            Console.WriteLine("You are a Monster Hunter, and on a routine hunt you've been ambushed by a Rathian!");
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
