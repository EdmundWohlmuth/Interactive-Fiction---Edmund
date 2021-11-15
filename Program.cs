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
        static string[] story = new string[3];
        static int pageNum;

        static bool isGameOver;

        static void Main(string[] args)
        {
            while (isGameOver == false)
            {
                Appendix();

            }

            Console.ReadKey(true);
        }

        static void Appendix()
        {
            switch (pageNum)
            {
                case 0:
                    Console.WriteLine("Hello World");
                    break;
                case 1:
                    Console.WriteLine("Page One");
                    break;
                case 2:
                    Console.WriteLine("Page Two");
                    break;
            }
        }

        static void PlotText()
        {

        }

        static void PageOne()
        {
            
        }

        static void PageTwo()
        {

        }


    }
}
