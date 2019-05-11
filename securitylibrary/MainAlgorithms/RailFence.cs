using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            int key = 0;
            int keyCount = plainText.Length / 2;
            if (plainText.Length == cipherText.Length)
            {
                key = 2;
                for(int i = key; i <= keyCount; i++)
                {
                    if (Encrypt(plainText, i).Equals(cipherText))
                    {
                        return i;
                    }
                }
            }
            return key;
        }

        public string Decrypt(string cipherText, int key)
        {
            cipherText = cipherText.ToLower();
            List<char> cipher = new List<char>();
            char[,] cipherMatrix;
            int row = key;
            int col = 0;
            int spaces = 0;
            int counter = 0;

            if (cipherText.Length % key != 0)
            {
                col = (cipherText.Length / key) + 1;
                spaces = (col * row) - cipherText.Length;
                cipherMatrix = new char[col, row];
                for (int l = 0; l < spaces; l++)
                {
                    cipherText = cipherText + " ";
                }
            }
            else
            {
                col = cipherText.Length / key;
                cipherMatrix = new char[col, row];
            }

            for (int j = 0; j < row; j++)
            {
                for (int i = 0; i < col; i++)
                {
                    cipherMatrix[i, j] = cipherText.ElementAt(counter);
                    counter++;
                }
            }
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    cipher.Add(cipherMatrix[i, j]);
                }
            }
            List<char> cipher1 = new List<char>();
            for (int i = 0; i < cipher.Count; i++)
            {

                if (cipher[i] != ' ')
                {
                    cipher1.Add(cipher[i]);
                }
            }
            return String.Join(String.Empty, cipher1);
        }

        public string Encrypt(string plainText, int key)
        {
            plainText = plainText.ToLower();
            List<char> cipher = new List<char>();
            char[,] cipherMatrix;
            int row = key;
            int col = 0;
            int spaces = 0;
            int counter = 0;

            if (plainText.Length % key != 0)
            {
                col = (plainText.Length / key) + 1;
                spaces = (col * row) - plainText.Length;
                cipherMatrix = new char[col, row];

                for (int l = 0; l < spaces; l++)
                {
                    plainText = plainText + " ";
                }

            }

            else
            {
                col = plainText.Length / key;
                cipherMatrix = new char[col, row];
            }

            for (int i = 0; i < col; i++)
            {
                for (int j = row - 1; j >= 0; j--)
                {
                    cipherMatrix[i, j] = plainText.ElementAt(counter);
                    counter++;
                }
            }

            for (int j = row - 1; j >= 0; j--)
            {
                for (int i = 0; i < col; i++)
                {
                    cipher.Add(cipherMatrix[i, j]);
                }
            }
            List<char> cipher1 = new List<char>();
            for (int i = 0; i < cipher.Count; i++)
            {

                if (cipher[i] != ' ')
                {
                    cipher1.Add(cipher[i]);
                }
            }
            return String.Join(String.Empty, cipher1);
        }
    }
}
