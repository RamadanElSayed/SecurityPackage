using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {

        string alphabits = "abcdefghijklmnopqrstuvwxyz";

        //  Char [] corresponding_Chars={'D','Q','E','P','R','S','F','T','A','W','X','U','G','O','B','V','H','N','C','M','I','Z','L','Y','J','K'};


        public string Analyse(string plainText, string cipherText)
        {

            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();

            string charsNotInKey = "";
            char[] keyAsChars = new char[26];
            for (int i = 0; i < keyAsChars.Length; i++)
            {
                keyAsChars[i] = '*';
            }
            string CiperChar = "";
            String Key = "";

            for (int i = 0; i < plainText.Length; i++)
            {   //get char from cipher, then know corres char in P.T, then put the cipher char in the index (alphabitic order) of the P.T char.
                CiperChar += cipherText[i];
                int indexOfPlainTextChar = alphabits.IndexOf(plainText[i]);
                keyAsChars[indexOfPlainTextChar] = CiperChar[i];
            }
            for (int i = 0; i < 26; i++)
            {
                if (!keyAsChars.Contains(alphabits[i])) { charsNotInKey += alphabits[i]; }


            }
            //cpmplete key to 26 chars.
            int x = 0;
            for (int i = 0; i < 26; i++)
            {
                if ((keyAsChars[i] == '*')) { keyAsChars[i] = charsNotInKey[x]; x++; }
            }
            //put keyAsChars into key
            //Key = keyAsChars.ToString();
            return new string(keyAsChars);
            //throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            // get index of char  of cipher in key then + 97 
            // p=12 ...then +97 then 97+12=109 give m .
            string ciphertexttwo = cipherText.ToLower();
            string keytwo = key.ToLower();
            char[] chars = new char[ciphertexttwo.Length];
            for (int i = 0; i < ciphertexttwo.Length; i++)
            {
                if (ciphertexttwo[i] == ' ')
                {
                    chars[i] = ' ';
                }
                else
                {
                    int j = keytwo.IndexOf(ciphertexttwo[i]) + 97;
                    chars[i] = (char)j;
                }
            }
            return new string(chars);
        }

        public string Encrypt(string plainText, string key)
        {
            // a=97..
            // word meet ..m=109 ..109-97=12 ..index 12 fom key=p . wkman z=122 then 122-97=25 and 0 char26
            string ciphertexttwo = plainText.ToLower();

            string keytwo = key.ToLower();
            char[] chars = new char[ciphertexttwo.Length];
            for (int i = 0; i < ciphertexttwo.Length; i++)
            {
                if (ciphertexttwo[i] == ' ')
                {
                    chars[i] = ' ';
                }

                else
                {
                    int j = ciphertexttwo[i] - 97;
                    chars[i] = keytwo[j];
                }
            }

            return new string(chars);
        }

        /*  
           /// <summary>
           /// Frequency Information:
           /// E   12.51%
           /// T	9.25
           /// A	8.04
           /// O	7.60
           /// I	7.26
           /// N	7.09
           /// S	6.54
           /// R	6.12
           /// H	5.49
           /// L	4.14
           /// D	3.99
           /// C	3.06
           /// U	2.71
           /// M	2.53
           /// F	2.30
           /// P	2.00
           /// G	1.96
           /// W	1.92
           /// Y	1.73
           /// B	1.54
           /// V	0.99
           /// K	0.67
           /// X	0.19
           /// J	0.16
           /// Q	0.11
           /// Z	0.09
           /// </summary>
           /// <param name="cipher"></param>
           /// <returns>Plain text</returns>
           */
        public static string Alphabetize(string s)
        {
            // 1.
            // Convert to char array.
            char[] a = s.ToCharArray();

            // 2.
            // Sort letters.
            Array.Sort(a);

            // 3.
            // Return modified string.
            return new string(a);
        }
        public string AnalyseUsingCharFrequency(string cipher)
        {

            char[] ArrayOfRelativeFreq = { 'e', 't', 'a', 'o', 'i', 'n', 's', 'r', 'h', 'l', 'd', 'c', 'u', 'm', 'f', 'p', 'g', 'w', 'y', 'b', 'v', 'k', 'x', 'j', 'q', 'z' };

            string LowerCaseCipher = cipher.ToLower();

            //araange cipher alpahbitically
            string AlphabetizeCipher = Alphabetize(LowerCaseCipher);
            //3 arrays will play on: AlphabetizeCipher, FreqOfDistinctCharsInCipher, distinctCharsInCipher, ArrayOfRelativeFreq

            //get distinct #chars in cipher /test this
            var count = (new HashSet<char>(LowerCaseCipher)).Count;

            char[] distinctCharsInCipher = new char[count];
            int[] FreqOfDistinctCharsInCipher = new int[count];

            //put the first char of cipher, in the first index in distinctCharsInCipher array
            distinctCharsInCipher[0] = LowerCaseCipher[0];

            FreqOfDistinctCharsInCipher[0] = 1;

            int IndexInDistinctCharsInCipherArray = 0, IndexInFreqOfDistinctCharsInCipherArray = 0;
            for (int i = 0; i < LowerCaseCipher.Length - 1; i++)
            {
                if (LowerCaseCipher[i] == LowerCaseCipher[i + 1])
                {
                    if (FreqOfDistinctCharsInCipher[Array.IndexOf(distinctCharsInCipher, LowerCaseCipher[i + 1])] < 2)
                    {
                        FreqOfDistinctCharsInCipher[IndexInFreqOfDistinctCharsInCipherArray] += 1;
                    }
                    else {

                        int index = Array.IndexOf(distinctCharsInCipher, LowerCaseCipher[i + 1]);
                        FreqOfDistinctCharsInCipher[index] += 1;
               
                    }
                }
                else if (!(distinctCharsInCipher.Contains(LowerCaseCipher[i + 1])))
                {
                    IndexInFreqOfDistinctCharsInCipherArray++; IndexInDistinctCharsInCipherArray++;

                    distinctCharsInCipher[IndexInDistinctCharsInCipherArray] = LowerCaseCipher[i + 1];
                    FreqOfDistinctCharsInCipher[IndexInFreqOfDistinctCharsInCipherArray] = 1;


                }
                //here the char  in distcCharArray, get its index and add 1 to its freq in freqArray
                else
                {
                    //may be wrong
                    int index = Array.IndexOf(distinctCharsInCipher, LowerCaseCipher[i + 1]);
                    FreqOfDistinctCharsInCipher[index] += 1;
                }

            }// here we have distc chars in array,and their freq in array.
            /* var items = new List<KeyValuePair<int, String>>();
items.Add(new KeyValuePair<int, String>(1, "first"));
items.Add(new KeyValuePair<int, String>(1, "second"));
var lookup = items.ToLookup(kvp => kvp.Key, kvp => kvp.Value);*/
            //put them in a map.now this is a freq with a char
            var items = new List<KeyValuePair<int, char>>();
            //1-commented  var mapOfCharWithFreq = new Dictionary<int, char>();
            //  char in a given cipher with the one that has the most freq values
            var mapOfcorresChars = new List<KeyValuePair<char, char>>();

            for (int i = 0; i < distinctCharsInCipher.Length; i++)
            {
                items.Add(new KeyValuePair<int, char>(FreqOfDistinctCharsInCipher[i], distinctCharsInCipher[i]));

                //2- commented  mapOfCharWithFreq[FreqOfDistinctCharsInCipher[i]] = distinctCharsInCipher[i];


            }
            //addded the foolowing line:
            var lookup = items.ToLookup(kvp => kvp.Key, kvp => kvp.Value);
            // arrange freqs of distcchars in ascending
            Array.Sort(FreqOfDistinctCharsInCipher);
            // reverse order to descending

            Array.Reverse(FreqOfDistinctCharsInCipher);//then this array is in descending order
            int v = 0;
            //put each char in the disticCharsArray with corrsponding char in freqOrderedChars 
            var distcFreqArray = FreqOfDistinctCharsInCipher.Distinct().ToArray();
            for (int i = 0; i < distcFreqArray.Length; i++)
            {
                ///////// //may cause error as map may contain more than a char with same freq
                //added this following: 4 lines
                foreach (char x in lookup[distcFreqArray[i]])
                {

                    //4- commented char tmp = mapOfCharWithFreq[FreqOfDistinctCharsInCipher[i]];
                    char tmp = x;
                    //3-commented  char tmp = mapOfCharWithFreq[FreqOfDistinctCharsInCipher[i]];
                    mapOfcorresChars.Add(new KeyValuePair<char, char>(tmp, ArrayOfRelativeFreq[v]));
                    //  mapOfcorresChars[tmp] = ArrayOfRelativeFreq[v];
                    v++;

                }

            }
            string key = "";
            var lookup2 = mapOfcorresChars.ToLookup(kvp => kvp.Key, kvp => kvp.Value);
            for (int i = 0; i < LowerCaseCipher.Length; i++)
            {
                foreach (char x in lookup2[LowerCaseCipher[i]])
                {
                    char originalChar = x;
                    key += originalChar;
                }
            }


            return key;


        }
    }
}
