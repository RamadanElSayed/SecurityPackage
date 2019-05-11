using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        static int matrixRow, matrixColumn;
        static List<int> mylist;
        static string[,] matirxLetter;
        public List<int> Analyse(string plainText, string cipherText)
        {

            string plaintextone = plainText.ToUpper();
            string ciphertexttwo = cipherText.ToUpper();
            // get all columns
            int key = 2;
            List<int> keyLens = new List<int>();
            string[,] cipherMat;
            for (int i = 2; i <= plaintextone.Length / 2; i++)
            {
                if (plaintextone.Length % i == 0)
                {
                    keyLens.Add(i);
                }
            }
            int col = key;
            int row = 0;
            List<int> mylistOfKeys = new List<int>();

            foreach (int i in keyLens)
            {
                row = plaintextone.Length / i;
                cipherMat = new string[row, i];
                int counter = 0;
                // fill matrix row by row
                for (int j = 0; j < row; j++)
                {
                    for (int u = 0; u < i; u++)
                    {
                        cipherMat[j, u] = plaintextone.ElementAt(counter).ToString();
                        counter++;
                    }

                }


                // write column by column

                string tempvalue = "";

                for (int h = 0; h < i; h++)
                {
                    for (int y = 0; y < row; y++)
                    {
                        tempvalue += cipherMat[y, h];
                    }
                }

                //  int indexindex = 0;
                //   string keykey = "";
                Boolean flag = false;
                int indexxx = 0;
                // get number of char.s equal row  to compare 
                for (int p = 0; p < i; p++)
                {

                    string sumchars = tempvalue.Substring(indexxx, row);
                    //     Console.Write(sumchars + ",");
                    //   Console.WriteLine("" + indexxx);
                    indexxx += row;
                    //   indexindex += row;
                    int indexindextwo = 0;
                    // compare with every char.s in cipher text
                    for (int a = 0; a < i; a++)
                    {
                        string myvalue = ciphertexttwo.Substring(indexindextwo, row);
                        indexindextwo += row;
                        //  Console.WriteLine(myvalue);
                        if (sumchars.Equals(myvalue))
                        {
                            mylistOfKeys.Add((a + 1));
                            flag = true;
                        }



                    }

                    // to break loop cuz must find all sub block
                    if (flag == false)
                    {
                        break;
                    }


                }

            }

            if (mylistOfKeys.Count == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    mylistOfKeys.Add(0);
                }
                return mylistOfKeys;
            }
            else
            {
                return mylistOfKeys;

            }
        }


        public string Decrypt(string cipherText, List<int> key)
        {

            string plaintext = "";

            int coulmnlength = key.Count;
            int rowlength = 0;
            if (cipherText.Length % coulmnlength != 0)
            {
                rowlength = (cipherText.Length / coulmnlength) + 1;
            }
            else
            {
                rowlength = cipherText.Length / coulmnlength;
            }

            //Console.Write("" + rowlength + "coumns" + coulmnlength);
            string[,] matrix = new string[rowlength, coulmnlength];
            int idex = 0;

            // write coulmn by column
            for (int i = 0; i < coulmnlength; i++)
            {
                for (int j = 0; j < rowlength; j++)
                {
                    if (idex < cipherText.Length)
                    {
                        matrix[j, i] = cipherText.Substring(idex, 1);

                        idex++;
                    }


                }

            }
            string[,] matrixtwo = new string[rowlength, coulmnlength];

            // wrirte column by column according to key 
            for (int i = 0; i < coulmnlength; i++)
            {
                for (int j = 1; j < coulmnlength + 1; j++)
                {

                    if (key[i] == j)
                    {
                        for (int k = 0; k < rowlength; k++)
                        {
                            matrixtwo[k, i] = matrix[k, key[i] - 1];

                        }
                    }
                }
            }
            /*
                for (int i = 0; i < rowlength; i++)
                {
                    for (int j = 0; j < coulmnlength; j++)
                    {
                        Console.Write(matrix[i, j]);

                    }
                    Console.WriteLine();
                }*/
            for (int i = 0; i < rowlength; i++)
            {
                for (int j = 0; j < coulmnlength; j++)
                {
                    // Console.Write(matrixtwo[i, j]);
                    plaintext += matrixtwo[i, j];
                }

            }


            return plaintext;
        }

        public static int minindex()
        {
            // return min index of minvalue.
            int minval = 100;
            int minindex = 0;
            for (int i = 0; i < mylist.Count; i++)
            {
                if (mylist[i] < minval)
                {
                    minval = mylist[i];
                    minindex = i;

                }
            }
            // mylist.Remove(minindex);
            return minindex;
        }
        public string Encrypt(string plainText, List<int> mykey)
        {
            //    string key= string.Join(" ", mykey.Select(x => x.ToString()).ToArray());
            mylist = new List<int>();
            mylist = mykey;
            int index = 0;
            string ciphertxt = "";
            string plaintwo = plainText.ToUpper();
            // column
            matrixColumn = mykey.Count;

            if (plaintwo.Length % mykey.Count != 0)
            {
                matrixRow = (plaintwo.Length / mykey.Count) + 1;
            }
            else
            {
                matrixRow = plaintwo.Length / mykey.Count;
            }
            matirxLetter = new string[matrixRow, matrixColumn];

            //fill matrix with char.s  write row by row 
            for (int i = 0; i < matrixRow; i++)
            {
                for (int j = 0; j < matrixColumn; j++)
                {
                    if (index < plaintwo.Length)
                    {
                        matirxLetter[i, j] = plaintwo.Substring(index, 1);

                        index++;
                    }
                    else
                    {
                        matirxLetter[i, j] = "x";
                    }
                }
            }

            // read column by column
            for (int k = 0; k < mylist.Count; k++)
            {

                // get index of minvalue in key 
                int mnindex = minindex();
                // set in value of key in this index high value to avoid taking it again.
                mylist[mnindex] = 1000;
                for (int x = 0; x < matrixRow; x++)
                {

                    ciphertxt += matirxLetter[x, mnindex];
                }
            }


            return ciphertxt;
        }
    }
}
