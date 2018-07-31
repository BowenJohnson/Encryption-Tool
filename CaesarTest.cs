using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaesarEncryption;

namespace EncrpytionTestSuite
{
    public class CaesarTest
    {
        private static int _key = 0;

        public CaesarTest()
        {

        }

        public static void RunTest()
        {
            Menu();
        }

        private static void Menu()
        {
            bool done = false;

            while (!done)
            {
                Console.WriteLine("1. Change the key. (Current: {0})", _key);
                Console.WriteLine("2. Encrpyt text from file.");
                Console.WriteLine("3. Decrypt text from file.");
                Console.WriteLine("4. Exit");
                Console.Write("Selection: ");

                try
                {
                    int response = Convert.ToInt32(Console.ReadLine());

                    switch (response)
                    {
                        case 1:
                            ChangeKey();
                            break;
                        case 2:
                            Encrypt();
                            break;
                        case 3:
                            Decrypt();
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

        private static void ChangeKey()
        {
            bool done = false;

            while (!done)
            {
                try
                {
                    Console.Write("Please input a new key: ");

                    int key = Convert.ToInt32(Console.ReadLine());

                    _key = key;
                    if (key >= 0 && key <= 25)
                    {
                        done = true;
                    }
                    else
                    {
                        Console.WriteLine("Please input a value from 0-25.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter an integer value between 0-25.");
                }
            }
        }

        private static string ReadFile()
        {
            Console.Write("Please input an input file name: ");
            string path = @Console.ReadLine();

            string fileText = "";
            if (System.IO.File.Exists(path))
            {
                fileText = System.IO.File.ReadAllText(path);
            }
            else
            {
                Console.WriteLine("That file does not exist.");
            }

            return fileText;
        }

        private static void WriteFile(string text)
        {
            Console.Write("Please input an output file name: ");
            string path = @Console.ReadLine();

            System.IO.File.WriteAllText(path, text);
        }

        private static void Encrypt()
        {
            string plainText = ReadFile();

            string cipherText = Caesar.Encrypt(_key, plainText);

            WriteFile(cipherText);
        }

        private static void Decrypt()
        {
            string cipherText = ReadFile();

            string plainText = Caesar.Decrypt(_key, cipherText);

            WriteFile(plainText);
        }
    }
}
