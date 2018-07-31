using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaesarEncryption;

namespace CrackingSuite
{
    public class CaesarCrack
    {
        private static char[] commonLetters =
        {
            'e', 't', 'a', 'o', 'i', 'n', 's',
            'r', 'h', 'l', 'd', 'c', 'u', 'm',
            'f', 'p', 'g', 'w', 'y', 'b', 'v',
            'k', 'x', 'j', 'q', 'z'
        };

        private static int[] commonLetterCount = new int[26];
        private static bool[] commonLetterChecked = new bool[26];

        private static string[] commonWords =
        {
            "the", "and", "you", "that", "was","for", "are",
            "with", "his", "they", "this", "have", "from", "one",
            "had", "word", "but", "not", "what", "all", "were", "when",
            "your", "can", "said", "there", "use", "each", "which", "she",
            "how", "their", "will", "other", "about", "out", "many", "then",
            "them","these", "some", "her", "would", "make", "like", "him",
            "into", "time", "has", "look", "two", "more", "write", "see",
            "number", "way", "could", "people", "my", "than", "first", "water",
            "been", "call", "who", "oil", "its", "now", "find", "long", "down",
            "day", "did", "get", "come", "made", "may", "part"
        };

        private static string inputFile = @"c:\text\ciphertext.txt";
        private static string outputFile = @"c:\text\text.txt";

        public CaesarCrack()
        {
            for (int i = 0; i < commonLetterCount.Length; i++)
            {
                commonLetterCount[i] = 0;
            }

            for (int i = 0; i < commonLetterChecked.Length; i++)
            {
                commonLetterChecked[i] = false;
            }
        }

        public static void RunCrack()
        {
            Console.Write("Please enter the ciphertext file: ");
            inputFile = Console.ReadLine();

            Console.Write("Please enter the file name to output to: ");
            outputFile = Console.ReadLine();

            string cipherText = ReadFile(inputFile);
            CrackCipher(cipherText);
        }

        private static string ReadFile(string file)
        {
            string fileText = "";
            if (System.IO.File.Exists(file))
            {
                fileText = System.IO.File.ReadAllText(file);
            }
            else
            {
                Console.WriteLine("The input file does not exist");
            }

            return fileText;
        }

        private static void CrackCipher(string cipherText)
        {
            // Step 1: Find the most common letters in the ciphertext
            foreach (char cipherLetter in cipherText)
            {
                for (int i = 0; i < commonLetters.Length; i++)
                {
                    if (cipherLetter == commonLetters[i])
                    {
                        (commonLetterCount[i])++;
                    }
                }
            }

            int englishIndex = -1; // iterative index through english letter array
            int cipherIndex = 0; // index corresponding to the index of the current cipher letter being tested
            int matchCount = 0; // count for how many words match the list of common english words
            int key = 0; // The key to use to decrypt
            while (matchCount < 3 && englishIndex < 25)
            {
                // Step 2: Find the key that makes the most common ciphertext letter
                // Become equal to the most common english letter.
                cipherIndex = 0;
                englishIndex++;
                for (int i = 0; i < commonLetterCount.Length; i++)
                {
                    if (commonLetterCount[i] > commonLetterCount[cipherIndex] && commonLetterChecked[i] == false)
                    {
                        cipherIndex = i;
                    }
                }
                key = FindKey(englishIndex, cipherIndex);

                // Step 3: compare plainText to most common words, and if more than 3
                // words match, assume it is correct
                string plainText = Caesar.Decrypt(key, cipherText);
                matchCount = 0;
                foreach(string plainWord in plainText.Split(' '))
                {
                    foreach(string commonWord in commonWords)
                    {
                        if (plainWord == commonWord)
                        {
                            matchCount++;
                            if (matchCount > 3) break;
                        }
                    }
                    if (matchCount > 3) break;
                }
                commonLetterChecked[cipherIndex] = true;
            }
            
            if (matchCount > 3)
            {
                string plainText = Caesar.Decrypt(key, cipherText);
                WriteFile(plainText, outputFile);
                Console.WriteLine("Plain Text written to: {0}", outputFile);
                Console.WriteLine("The decryption key is: {0}", key);
            }
            else
            {
                Console.WriteLine("This algorithm could not crack the text. Are you sure it's english?");
            }
        }

        private static int FindKey(int english, int cipher)
        {
            return ((int)commonLetters[english].ToString().ToUpper().ToCharArray()[0] - (int)commonLetters[cipher].ToString().ToUpper().ToCharArray()[0]);
        }

        private static void WriteFile(string plainText, string file)
        {
            System.IO.File.WriteAllText(file, plainText);
        }
    }
}
