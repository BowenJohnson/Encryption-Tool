using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDESEncryption;

namespace EncrpytionTestSuite
{
    public class SDESTest
    {
        SDES sdes = new SDES();

        public SDESTest()
        {

        }

        public void Menu()
        {
            bool done = false;

            while (!done)
            {
                Console.WriteLine("1. Create Keys");
                Console.WriteLine("2. Encrpyt");
                Console.WriteLine("3. Decrypt");
                Console.WriteLine("4. Exit");
                Console.Write("Selection: ");

                try
                {
                    int response = Convert.ToInt32(Console.ReadLine());

                    switch (response)
                    {
                        case 1:
                            sdes.CreateKeys();
                            break;
                        case 2:
                            sdes.DES(true);
                            break;
                        case 3:
                            sdes.DES(false);
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
