using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            int p = cipherText.Length;
            int k = plainText.Length;
            string s = cipherText.ToLower();
            char[] pc = s.ToCharArray();
            char[] kc = plainText.ToCharArray();
            char[] key = new char[p];
            for (int i = 0; i < p; i++)
            {
                int e = kc[i] - 'a';
                e = e - pc[i];
                if (e < 0)
                {
                    e = e * (-1);
                }
                if (e < 97)
                {
                    e = e + 26;
                }
                key[i] = (char)e;
            }
            int kll = key.Length;
            string finall = "";
            finall += key[0];
            for (int i = 1; i < kll; i++)
            {
                if (key[0] != key[i])
                {
                    finall += key[i];
                }
                else if (key[1] != key[i + 1])
                {
                    finall += key[i];

                }
                else
                {
                    break;
                }
            }
            return finall;
            throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            string s= cipherText.ToLower();
            int p = cipherText.Length;
            int k = key.Length;
            int d = p - k;
            char[] pc = s.ToCharArray();
            char[] kc = new char[p];
            char[] temp = key.ToCharArray();
            temp.CopyTo(kc, 0);
            for (int j = 0; j < d; j++)
            {
                kc[k + j] = kc[j];
            }
            for (int i = 0; i < p; i++)
            {
                int e = pc[i] - kc[i];
                if (e < 0)
                {
                    e = e * (-1);
                    e = 26 - e;
                }
                char x = (char)(e + 'a');
                if (x < 97)
                {
                    pc[i] = (char)(x + 97);
                }
                else
                    pc[i] = (char)(x);
            }
            return new string(pc).ToLower();
           // throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
            int p = plainText.Length;
            int k = key.Length;
            int d = p - k;
            char[] pc = plainText.ToCharArray();
            char[] kc = new char[p];
            char[] temp = key.ToCharArray();
            temp.CopyTo(kc, 0);
            for (int j = 0; j < d; j++)
            {
                kc[k + j] = kc[j];
            }
                for (int i = 0; i < p; i++)
                {
                    char e = plainText[i];
                    int x = e - 'a';
                    if (x + kc[i] > 122)
                    {
                        pc[i] = (char)((x + kc[i]) - 26);
                    }
                    else
                        pc[i] = (char)(x + kc[i]);
                }
            return new string(pc).ToUpper();
            //throw new NotImplementedException();
        }
    }
}