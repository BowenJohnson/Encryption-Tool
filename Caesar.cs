using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarEncryption
{
    public class Caesar
    {
        public Caesar()
        {

        }

        public static string Encrypt(int key, string plaintext)
        {
            StringBuilder ciphertext = new StringBuilder();

            // convert plaintext to uppercase
            plaintext = plaintext.ToUpper();

            // Loop through each character in the plaintext string
            foreach (char currentChar in plaintext)
            {
                if ((int)currentChar >= 65 && (int)currentChar <= 90)
                {
                    // convert plaintext from ascii (65-90) down to (0-25)
                    int plainCharValue = (int)currentChar - 65;

                    // Run algorithm 
                    int cipherCharValue = (plainCharValue + key) % 26;

                    // convert cipher value back to ascii
                    Char cipherChar = (Char)(cipherCharValue + 65);

                    // append each character to the string builder
                    ciphertext.Append(cipherChar);
                }
                else
                {
                    ciphertext.Append(currentChar);
                }
            }

            // convert built string to normal string
            string cipher = ciphertext.ToString();

            return cipher;
        }

        public static string Decrypt(int key, string ciphertext)
        {
            StringBuilder plaintext = new StringBuilder();

            // convert ciphertext to uppercase
            ciphertext = ciphertext.ToUpper();

            // Loop through each character in the ciphertext string
            foreach (char currentChar in ciphertext)
            {
                if ((int)currentChar >= 65 && (int)currentChar <= 90)
                {
                    // convert ciphertext from ascii (65-90) down to (0-25)
                    int cipherCharValue = (int)currentChar - 65;

                    // Run algorithm 
                    int plainCharValue = ((cipherCharValue - key) + 26) % 26;

                    // convert plaintext value back to ascii
                    Char plainChar = (Char)(plainCharValue + 65);
                    
                    // append each character to the string builder
                    plaintext.Append(plainChar);
                }
                else
                {
                    plaintext.Append(currentChar);
                }
            }

            // convert built string to normal string
            string plain = plaintext.ToString();

            plain = plain.ToLower();

            return plain;
        }
    }
}
