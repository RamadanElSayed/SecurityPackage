using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public string Encrypt(string plainText, int key)
        {
            int L = plainText.Length;
            char[] encrypt = plainText.ToCharArray();
            for (int i=0; i<L; i++)
            {
                char e = plainText[i];
                e = (char)(e+key);
                if(e > 'z')
                {
                    e = (char)(e-26);
                }
                else if(e < 'a')
                {
                    e = (char)(e+26);
                }
                encrypt[i] = e;
            }
            return new string (encrypt);
            throw new NotImplementedException();
        }
        public string Decrypt(string cipherText, int key)
        {
            int s = cipherText.Length;
            char[] decrypt = new char[cipherText.Length];
            cipherText = cipherText.ToLower();
            for (int i=0; i<s; i++)
            {
                if(cipherText[i]-key<97)
                {
                    decrypt[i]= (char)((cipherText[i]-key)+26);
                }
                else decrypt[i]= (char)(cipherText[i]-key);
            }
            return new string(decrypt);
            
        }
        public int Analyse(string plainText, string cipherText)
        {
            char[] key = plainText.ToCharArray();
            char[] key1 = cipherText.ToCharArray();
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            int x = cipherText.Length;
            int s;
            if (cipherText[1] < plainText[1])
            {
                s = (char)((cipherText[1] - plainText[1]) + 26);
            }
            else 
                s = cipherText[1] - plainText[1];
            return s;
           
        }
    }
}
