using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographicTechnique<string, string>
    {
        /// <summary>
        /// The most common diagrams in english (sorted): TH, HE, AN, IN, ER, ON, RE, ED, ND, HA, AT, EN, ES, OF, NT, EA, TI, TO, IO, LE, IS, OU, AR, AS, DE, RT, VE
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string Analyse(string plainText)
        {
            throw new NotImplementedException();
        }

        public string Analyse(string plainText, string cipherText)
        {
            throw new NotSupportedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            String[,] matrix = new String[5, 5];
            Dictionary<char, bool> alphabitDic = new Dictionary<char, bool>();
            int _StringIndex = 0;
            int counter = 0;
            String aphabit = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            foreach (Char c in aphabit)  // initialization for the map
                alphabitDic[c] = false;

            HashSet<Char> uniquekey = new HashSet<Char>();

            key = key.ToUpper();

            foreach (char c in key)  //add the key to the set to take distinct chars
                uniquekey.Add(c);


            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (_StringIndex >= uniquekey.Count)
                    {
                        completeMatrix(i, j, alphabitDic, ref  matrix); //complete the matrix
                        i = j = 6; // to break the loop
                    }
                    else
                    {
                        if ((uniquekey.ElementAt(_StringIndex).Equals('I') || uniquekey.ElementAt(_StringIndex).Equals('J')) && counter < 1)
                        {
                            matrix[i, j] = "ij".ToUpper();
                            counter++;
                            alphabitDic['J'] = true;
                            alphabitDic['I'] = true;

                        }

                        else
                        {
                            if (uniquekey.ElementAt(_StringIndex).Equals('J') || uniquekey.ElementAt(_StringIndex).Equals('I') && counter == 1)
                                _StringIndex++; // to avoid insert i or j twice
                            matrix[i, j] = uniquekey.ElementAt(_StringIndex).ToString().ToUpper();
                            alphabitDic[uniquekey.ElementAt(_StringIndex)] = true;
                            _StringIndex++;
                        }
                    }
                }
            }

            cipherText =cipherText.ToUpper();
           
            
           
            _StringIndex = 0;
           int _stringindex2 = 0;
           
            String[] tokens = new String[cipherText.Length / 2];
            for (int i = 0; i < cipherText.Length; i += 2) // devide the plain into a pairs of char
            {
                tokens[_StringIndex] = cipherText[_stringindex2] + "" + cipherText[_stringindex2 + 1];
                _StringIndex++;
                _stringindex2 += 2;

            }

            String decryptedText = "";
            for (int i = 0; i < tokens.Length; i++) // encrypt the token
            {
                decryptedText += decryptToken(tokens[i], ref matrix);

            }
            if (decryptedText[decryptedText.Length - 1] == 'X')
            {
                decryptedText = decryptedText.Remove(decryptedText.Length - 1, 1);
            }
            String plainText = "";
            for (int i = 0; i < decryptedText.Length;i++ )
            {
                if (i == decryptedText.Length - 1 && decryptedText[i] == 'X')
                    continue;
                else if (i == decryptedText.Length- 1 && decryptedText[i] != 'X')
                    plainText = plainText + decryptedText[i];
                else if (i % 2!= 0 && decryptedText[i - 1] == decryptedText[i + 1] && decryptedText[i] == 'X')
                    continue;
                else
                    plainText = plainText + decryptedText[i];
            }

            return plainText;
        }

        public string Encrypt(string plainText, string key)
        {
            String[,] matrix = new String[5, 5];
            Dictionary<char, bool> alphabitDic = new Dictionary<char, bool>();
            int _StringIndex = 0;
            int counter = 0;
            String aphabit = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            foreach (Char c in aphabit)  // initialization for the map
                alphabitDic[c] = false;

            HashSet<Char> uniquekey = new HashSet<Char>();

            key = key.ToUpper();

            foreach (char c in key)  //add the key to the set to take distinct chars
                uniquekey.Add(c);


            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (_StringIndex >= uniquekey.Count)
                    {
                        completeMatrix(i, j, alphabitDic, ref  matrix); //complete the matrix
                        i = j = 6; // to break the loop
                    }
                    else
                    {
                        if ((uniquekey.ElementAt(_StringIndex).Equals('I')||uniquekey.ElementAt(_StringIndex).Equals('J')) && counter < 1)
                        {
                            matrix[i, j] = "ij".ToUpper();
                            counter++;
                            alphabitDic['J'] = true;
                            alphabitDic['I'] = true;

                        }
                       
                        else
                        {
                            if (uniquekey.ElementAt(_StringIndex).Equals('J') || uniquekey.ElementAt(_StringIndex).Equals('I') && counter == 1)
                                _StringIndex++; // to avoid insert i or j twice
                            matrix[i, j] = uniquekey.ElementAt(_StringIndex).ToString().ToUpper();
                            alphabitDic[uniquekey.ElementAt(_StringIndex)] = true;
                            _StringIndex++;
                        }
                    }
                }
            }
           
           
            plainText = plainText.ToUpper();

           
         
            _StringIndex = 0;
            int _stringindex2 = 0;
            int forLength=0;
            if(plainText.Length%2==0)
                forLength=plainText.Length/2;
            else
                forLength=plainText.Length/2 -1;
            for (int i = 0; i < forLength; i++) // check if there is a pair of chars has same chars if there insert a X between them
            {
               
                if (plainText[_StringIndex] == plainText[_StringIndex + 1])
                 plainText=   plainText.Insert(_StringIndex+1, "X");
                _StringIndex += 2;

            }
            
            _StringIndex = 0;
            _stringindex2 = 0;
            if (plainText.Length % 2 != 0) // if the plain has odd chars add a 'X' at the last of it
            {
                plainText += "X";
            }
            String [] tokens = new String[plainText.Length / 2];
            for (int i = 0; i < plainText.Length; i += 2) // devide the plain into a pairs of char
            {
                tokens[_StringIndex] = plainText[_stringindex2] + "" + plainText[_stringindex2 + 1];
                _StringIndex++;
                _stringindex2 += 2;

            }

            String cipherText = "";
            for (int i = 0; i < tokens.Length; i++) // encrypt the token
            {
                cipherText += encryptToken(tokens[i], ref matrix);

            }
            return cipherText;


        }

        private static void completeMatrix(int _i, int _j, Dictionary<char, bool> token, ref string[,] matrix)
        {
            String LeftChars = "";
            foreach (KeyValuePair<char, bool> index in token)
                if (index.Value == false)
                    LeftChars += index.Key;
            int Index = 0;
            int counter = 0;
            for (int i = _i; i < 5; i++)
            {
                for (int j = _j; j < 5; j++)
                {
                    if ((LeftChars[Index].ToString() == "J" ||LeftChars[Index].ToString() == "I") && counter < 1)
                    {
                        matrix[i, j] = "IJ";
                        Index++;
                        counter++;
                    }
                    
                    else
                    {
                        if (counter == 1 && (LeftChars[Index] == 'I' || LeftChars[Index] == 'J'))
                            Index++;

                        matrix[i, j] = LeftChars[Index].ToString();
                        Index++;

                    }

                }
                _j = 0;
            }

        }
        public String encryptToken(String token, ref String[,] matrix)
        {
            String encryptedToken = "";

            int row1 = 0, row2 = 0, column1 = 0, column2 = 0;

            for (int i = 0; i < 5; i++) //find 1st char
            {
                for (int j = 0; j < 5; j++)
                {

                    if (token[0] == matrix[i, j][0])
                    {
                        row1 = i;
                        column1 = j;
                        i = j = 6;
                    }
                }

            }
            for (int i = 0; i < 5; i++) // find 2nd char
            {
                for (int j = 0; j < 5; j++)
                {

                    if (token[1] == matrix[i, j][0])
                    {
                        row2 = i;
                        column2 = j;
                        i = j = 6;
                    }
                }

            }


            if (row1 == row2)
            {
                if (column1 + 1 > 4)
                    encryptedToken += matrix[row1, 0][0];
                else
                    encryptedToken += matrix[row1, column1 + 1][0];
                if (column2 + 1 > 4)
                    encryptedToken += matrix[row1, 0][0];
                else
                    encryptedToken += matrix[row1, column2 + 1][0];
            }
            else if (column1 == column2)
            {
                if (row1 + 1 > 4)
                    encryptedToken += matrix[0, column1][0];
                else
                    encryptedToken += matrix[row1 + 1, column1][0];
                if (row2 + 1 > 4)
                    encryptedToken += matrix[0, column1][0];
                else
                    encryptedToken += matrix[row2 + 1, column1][0];
            }
            else
            {
                encryptedToken += matrix[row1, column2][0];
                encryptedToken += matrix[row2, column1][0];
            }
            return encryptedToken;
        }
         public  String decryptToken(String token, ref String[,] matrix)
        {
            String decryptedToken = "";

            int row1 = 0, row2 = 0, column1 = 0, column2 = 0;

            for (int i = 0; i < 5; i++) //find 1st char
            {
                for (int j = 0; j < 5; j++)
                {

                    if (token[0] == matrix[i, j][0])
                    {
                        row1 = i;
                        column1 = j;
                        i = j = 6;
                    }
                }

            }
            for (int i = 0; i < 5; i++) // find 2nd char
            {
                for (int j = 0; j < 5; j++)
                {

                    if (token[1] == matrix[i, j][0])
                    {
                        row2 = i;
                        column2 = j;
                        i = j = 6;
                    }
                }

            }
            if (row1 == row2)
            {
                if (column1 - 1 <0)
                    decryptedToken += matrix[row1, 4][0];
                else
                    decryptedToken += matrix[row1, column1 - 1][0];
                if (column2 - 1 <0)
                    decryptedToken += matrix[row1, 4][0];
                else
                    decryptedToken += matrix[row1, column2 - 1][0];
            }
            else if (column1 == column2)
            {
                if (row1 - 1 <0)
                    decryptedToken += matrix[4, column1][0];
                else
                    decryptedToken += matrix[row1 - 1, column1][0];
                if (row2 -1 <0)
                    decryptedToken += matrix[4, column1][0];
                else
                    decryptedToken += matrix[row2 - 1, column1][0];
            }
            else
            {
                decryptedToken += matrix[row1, column2][0];
                decryptedToken += matrix[row2, column1][0];
            }
            return decryptedToken;

        }
   
        }
    }

