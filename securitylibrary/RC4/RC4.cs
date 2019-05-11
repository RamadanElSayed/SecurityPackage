using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RC4
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class RC4 : CryptographicTechnique
    {
        public override string Decrypt(string cipherText, string key)
        {
            List<int> cipher = new List<int>();
            if (cipherText.Contains("0x") == true)
            {
                string[] stringArr = cipherText.Split('x');
                cipherText = stringArr[1];
                List<string> stringList = new List<string>();
                for (int i = 0; i < cipherText.Length; i += 2)
                {
                    stringList.Add(cipherText[i] + "" + cipherText[i + 1]);
                }

                for (int i = 0; i < stringList.Count; i++)
                {
                    int intValue = Int32.Parse(stringList[i], System.Globalization.NumberStyles.HexNumber);
                    cipher.Add(intValue);
                }
                stringArr = key.Split('x');
                key = stringArr[1];
                stringList.Clear();
                for (int i = 0; i < key.Length; i += 2)
                {
                    stringList.Add(key[i] + "" + key[i + 1]);
                }
                key = "";
                for (int i = 0; i < stringList.Count; i++)
                {
                    int intValue = Int32.Parse(stringList[i], System.Globalization.NumberStyles.HexNumber);
                    key += (char)intValue;
                }

                int[] S = new int[256];
                int[] T = new int[256];
                int counter = 0;
                for (int i = 0; i < 256; i++)
                {
                    S[i] = i;
                    if (counter == key.Length)
                        counter = 0;
                    T[i] = key[counter];
                    counter++;
                }

                int j = 0;
                for (int i = 0; i < 255; i++)
                {
                    j = (j + S[i] + T[i]) % 256;

                    int temp = S[i];
                    S[i] = S[j];
                    S[j] = temp;
                }
                List<int> K = new List<int>();
                int I = 0;
                j = 0;
                for (int k = 0; k < cipherText.Length; k++)
                {
                    I = (I + 1) % 256;
                    j = (j + S[I]) % 256;

                    int temp = S[I];
                    S[I] = S[j];
                    S[j] = temp;

                    int t = (S[I] + S[j]) % 256;
                    K.Add(S[t]);
                }



                List<int> cipherList = xorOperation(cipher, K);
                string plain = "0x";
                for (int i = 0; i < cipherList.Count; i++)
                {
                    plain += cipherList[i].ToString("X");
                }
                return plain;
            }
            else
            {
                int[] S = new int[256];
                int[] T = new int[256];
                int counter = 0;
                for (int i = 0; i < 256; i++)
                {
                    S[i] = i;
                    if (counter == key.Length)
                        counter = 0;
                    T[i] = key[counter];
                    counter++;
                }

                int j = 0;
                for (int i = 0; i < 255; i++)
                {
                    j = (j + S[i] + T[i]) % 256;

                    int temp = S[i];
                    S[i] = S[j];
                    S[j] = temp;
                }
                List<int> K = new List<int>();
                int I = 0;
                j = 0;
                for (int k = 0; k < cipherText.Length; k++)
                {
                    I = (I + 1) % 256;
                    j = (j + S[I]) % 256;

                    int temp = S[I];
                    S[I] = S[j];
                    S[j] = temp;

                    int t = (S[I] + S[j]) % 256;
                    K.Add(S[t]);
                }
              
                for (int k = 0; k < cipherText.Length; k++)
                {
                    int number = cipherText[k];
                    cipher.Add(number);
                }
                List<int> plaingList = xorOperation(cipher, K);
                string plain = "";
                for (int i = 0; i < plaingList.Count; i++)
                {
                    plain += (char)plaingList[i];
                }
                return plain;
            }
        }

        public override  string Encrypt(string plainText, string key)
        {
            List<int> plain = new List<int>();
           
            if (plainText.Contains("0x")==true)
            {
                string[] stringArr = plainText.Split('x');
                plainText = stringArr[1];
                List<string> stringList = new List<string>();
                for ( int i=0;i<plainText.Length;i+=2)
                {
                    stringList.Add(plainText[i]+""+plainText[i + 1]);
                }

                for ( int i=0;i<stringList.Count;i++)
                {
                    int intValue = Int32.Parse(stringList[i], System.Globalization.NumberStyles.HexNumber);
                    plain.Add(intValue);
                }
                stringArr = key.Split('x');
                key = stringArr[1];
                stringList.Clear();
                for (int i =0;i<key.Length;i+=2)
                {
                    stringList.Add(key[i] + "" + key[i + 1]);
                }
                key = "";
                for ( int i=0;i<stringList.Count;i++)
                {
                    int intValue = Int32.Parse(stringList[i], System.Globalization.NumberStyles.HexNumber);
                    key+= (char)intValue ;
                }

                int[] S = new int[256];
                int[] T = new int[256];
                int counter = 0;
                for (int i = 0; i < 256; i++)
                {
                    S[i] = i;
                    if (counter == key.Length)
                        counter = 0;
                    T[i] = key[counter];
                    counter++;
                }

                int j = 0;
                for (int i = 0; i < 255; i++)
                {
                    j = (j + S[i] + T[i]) % 256;

                    int temp = S[i];
                    S[i] = S[j];
                    S[j] = temp;
                }
                List<int> K = new List<int>();
                int I = 0;
                j = 0;
                for (int k = 0; k < plainText.Length; k++)
                {
                    I = (I + 1) % 256;
                    j = (j + S[I]) % 256;

                    int temp = S[I];
                    S[I] = S[j];
                    S[j] = temp;

                    int t = (S[I] + S[j]) % 256;
                    K.Add(S[t]);
                }



                List<int> cipherList = xorOperation(plain, K);
                string cipher = "0x";
                for (int i = 0; i < cipherList.Count; i++)
                {
                    cipher +=cipherList[i].ToString("X");
                }
                return cipher;
            }
            else
            {
                for (int k = 0; k < plainText.Length; k++)
                {
                    int number = plainText[k];
                    plain.Add(number);
                }
                int[] S = new int[256];
                int[] T = new int[256];
                int counter = 0;
                for (int i = 0; i < 256; i++)
                {
                    S[i] = i;
                    if (counter == key.Length)
                        counter = 0;
                    T[i] = key[counter];
                    counter++;
                }

                int j = 0;
                for (int i = 0; i < 255; i++)
                {
                    j = (j + S[i] + T[i]) % 256;

                    int temp = S[i];
                    S[i] = S[j];
                    S[j] = temp;
                }
                List<int> K = new List<int>();
                int I = 0;
                j = 0;
                for (int k = 0; k < plainText.Length; k++)
                {
                    I = (I + 1) % 256;
                    j = (j + S[I]) % 256;

                    int temp = S[I];
                    S[I] = S[j];
                    S[j] = temp;

                    int t = (S[I] + S[j]) % 256;
                    K.Add(S[t]);
                }



                List<int> cipherList = xorOperation(plain, K);
                string cipher = "";
                for (int i = 0; i < cipherList.Count; i++)
                {
                    cipher += (char)cipherList[i];
                }
                return cipher;
            }
           
        }
        static public List<int> xorOperation(List<int> plain , List<int> key)
        {
            List<int> result = new List<int>();
            for ( int i=0;i<plain.Count;i++)
            {
                result.Add(plain[i] ^ key[i]);
            }
            return result;
        }
    }
}
