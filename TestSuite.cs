using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrackingSuite;

namespace EncrpytionTestSuite
{
    class TestSuite
    {
        private static int _key = 0;

        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("1. Caesar Test");
                Console.WriteLine("2. Caesar Crack");
                Console.WriteLine("3. SDES");
                Console.WriteLine("4. Exit Tests");
                Console.Write("Selection: ");

                try
                {
                    int response = Convert.ToInt32(Console.ReadLine());

                    switch (response)
                    {
                        case 1:
                            CaesarTest.RunTest();
                            break;
                        case 2:
                            CaesarCrack.RunCrack();
                            break;
                        case 3:
                            new SDESTest().Menu();
                            break;
                        case 4:
                            done = true;
                            break;
                        default:
                            Console.WriteLine("Please input a value from 1-4.");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Enter an integer value from 1-4.");
                }
            }
        }
    }
}
