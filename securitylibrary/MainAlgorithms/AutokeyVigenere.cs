using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText1, string cipherText1)
        {
            string genKey;   
            string plainText=plainText1.ToLower();string cipherText= cipherText1.ToLower();
            int p = cipherText.Length;
            int k = plainText.Length;

            char[] pc = cipherText.ToCharArray();
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
            genKey= new string(key);
            //gen. mainKey:
            List<char> mainKey = new List<char>();
            int x = 0,j=0,count=0;
            for (int i = 0; i < genKey.Length; i++) 
            
            
            {
                mainKey.Add(genKey[i]);

                if (genKey[i] == plainText[0])
                
                {
                    x = i; j = 0;
                    for (; x < genKey.Length; x++)
                    {
                        if (genKey[x] == plainText[j]) {


                            count++; j++;

                        }


                    }

                    if (count == (genKey.Length - i))
                    
                    { 
                        int tmp1=mainKey.Count-1;
                        mainKey.RemoveAt(tmp1);
                        goto returnMainKey;
                    
                    }
                
                }



            }

            returnMainKey:
           char [] tmp=new char [mainKey.Count];
            genKey="";
            for(int i=0;i<mainKey.Count;i++){tmp[i]=mainKey[i];}
            genKey =new string(tmp);

            return genKey;
           
        }

        public  string Decrypt(string cipherText1, string key1)
        {
            string cipherText = cipherText1.ToLower();
            string key = key1.ToLower();
            // cipherText.ToLower();
            char[] plain = new char[cipherText.Length];
            List<char> plainToWriteToKey = new List<char>();
            int palinIndex = 0, cipherIndex = 0;
            List<char> currentKey = new List<char>();

            for (int i = 0; i < key.Length; i++) { currentKey.Add(key[i]); }

            for (int i = 0; i < plain.Length; i++) { plain[i] = '*'; }
            int number1 = 0;
        loop:
            for (int i = 0; i < currentKey.Count; i++)
            {

                number1 = 0;
                //adaed last
                for (int f = 0; f < plain.Length; f++) { if (plain[f] != '*') { number1++; } }
                if (number1 == cipherText.Length) { goto returnPlain; }
                //last
                ///
                int e = cipherText[cipherIndex] - currentKey[i];
                if (e < 0)
                {
                    e = e * (-1);
                    e = 26 - e;
                }
                char x = (char)(e + 'a');
                if (x < 97)
                {
                    plain[palinIndex] = (char)(x + 97);
                    plainToWriteToKey.Add(plain[palinIndex]);
                }
                else
                {
                    plain[palinIndex] = (char)(x);
                    plainToWriteToKey.Add(plain[palinIndex]);
                }
                palinIndex++;
                cipherIndex++;
                ///
            }
            // write the plain gen to the array, then call the above again.
            //? int number = plain[0].Count(s => s != null);

            currentKey.Clear();
            for (int j = 0; j < plainToWriteToKey.Count; j++)
            {

                currentKey.Add(plainToWriteToKey[j]);


            }
            plainToWriteToKey.Clear();
            int number = 0;
            for (int i = 0; i < plain.Length; i++) { if (plain[i] != '*') { number++; } }

            if (number < cipherText.Length)
            {

                goto loop;

            }
        returnPlain:
           return new string(plain).ToLower();

        }
        public string Encrypt(string plainText, string key)
        {
            plainText.ToLower(); key.ToLower();
            int pLen = plainText.Length;
            int kLen = key.Length;
            char[] cipher = new char[pLen];
            char[] compKey = new char[pLen];
            char[] tmp = (key.ToCharArray());
            tmp.CopyTo(compKey, 0);

            if (key.Length < plainText.Length)
            {
                for (int i = kLen; i < pLen; i++)
                {

                    for (int j = 0; j < pLen; j++)
                    {

                        compKey[i] = plainText[j];
                        if (i == pLen - 1)
                        {
                            goto copmKeyReady;
                        }
                        i++;
                    }
                }

            }
        //here: the complete key is generated in compKey.
            copmKeyReady:

            ///generate cipher

            for (int i = 0; i < pLen; i++)
            {
                char e = plainText[i];
                int x = e - 'a';
                if (x + compKey[i] > 122)
                {
                    cipher[i] = (char)((x + compKey[i]) - 26);
                }
                else
                    cipher[i] = (char)(x + compKey[i]);
            }
          
            return new string(cipher).ToUpper();
      

            /// 
            ///
           
        }
    }
}
