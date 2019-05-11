using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DES
{
    public class DESAlgorithm
    {
        private static int[] leftshiftArray = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
        private string mainPlain = "0x0123456789ABCDEF";
        private string mainCipher = "0x85E813540F0AB405";
        private string mainKey = "0x133457799BBCDFF1";

        private string keyBinary = "";
        private string plainBinary = "";

        private List<string> pc1out = new List<string>();
        private List<string> pc2out = new List<string>();
        private List<string> ipout = new List<string>();
        private List<string> epout = new List<string>();
        private List<string> sBlocks = new List<string>();

        private string ci = "";
        private string di = "";
        private string left = "";
        private string right = "";
        private string leftN = "";
        private string rightN = "";

        private string s1 = "";
        private string s2 = "";
        private string s3 = "";
        private string s4 = "";
        private string s5 = "";
        private string s6 = "";
        private string s7 = "";
        private string s8 = "";

        private string temp = "";

        private int[,] st1 = new int[4, 16]
        {
            {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
            {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
            {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 5, 10, 0 },
            {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 }
        };

        private int[,] st2 = new int[4, 16]
        {
            { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
            { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
            { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
            { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 }
        };

        private int[,] st3 = new int[4, 16]
        {
            { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
            { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
            { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
            { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 }
        };

        private int[,] st4 = new int[4, 16]
        {
            { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
            { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
            { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
            { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 }
        };

        private int[,] st5 = new int[4, 16]
        {
            { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
            { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
            { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
            { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 }
        };

        private int[,] st6 = new int[4, 16]
        {
            { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
            { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
            { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
            { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 }
        };

        private int[,] st7 = new int[4, 16]
        {
            { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
            { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
            { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
            { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 }
        };

        private int[,] st8 = new int[4, 16]
        {
            { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
            { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
            { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
            { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 }
        };

        private int[,] pc1 = new int[8, 7]
        {
                {57, 49, 41, 33, 25, 17, 9},
                {1, 58, 50, 42, 34, 26, 18},
                {10, 2, 59, 51, 43, 35, 27},
                {19, 11, 3, 60, 52, 44, 36},
                {63, 55, 47, 39, 31, 23, 15},
                {7, 62, 54, 46, 38, 30, 22},
                {14, 6, 61, 53, 45, 37, 29},
                {21, 13, 5, 28, 20, 12, 4}
        };

        private int[,] pc2 = new int[6, 8]
        {
                {14, 17, 11, 24, 1, 5, 3, 28},
                {15, 6, 21, 10, 23, 19, 12, 4},
                {26, 8, 16, 7, 27, 20, 13, 2},
                {41, 52, 31, 37, 47, 55, 30, 40},
                {51, 45, 33, 48, 44, 49, 39, 56},
                {34, 53, 46, 42, 50, 36, 29, 32}
        };

        private int[,] ip = new int[8, 8] {
                {58, 50, 42, 34, 26, 18, 10, 2},
                {60, 52, 44, 36, 28, 20, 12, 4},
                {62, 54, 46, 38, 30, 22, 14, 6},
                {64, 56, 48, 40, 32, 24, 16, 8},
                {57, 49, 41, 33, 25, 17, 9, 1},
                {59, 51, 43, 35, 27, 19, 11, 3},
                {61, 53, 45, 37, 29, 21, 13, 5},
                {63, 55, 47, 39, 31, 23, 15, 7}
            };

        private int[,] ep = new int[8, 6] {
                {32, 1, 2, 3, 4, 5},
                {4, 5, 6, 7, 8, 9},
                {8, 9, 10, 11, 12, 13},
                {12, 13, 14, 15, 16, 17},
                {16, 17, 18, 19, 20, 21},
                {20, 21, 22, 23, 24, 25},
                {24, 25, 26, 27, 28, 29},
                {28, 29, 30, 31, 32, 1}
            };
        private int[,] p = new int[4, 8]
        {
            { 16, 7, 20, 21, 29, 12, 28, 17 },
            { 1, 15, 23, 26, 5, 18, 31, 10 },
            { 2, 8, 24, 14, 32, 27, 3, 9 },
            { 19, 13, 30, 6, 22, 11,  4, 25 }
        };

        private int[,] p2 = new int[8, 4]
        {
            { 16, 7, 20, 21 },
            { 29, 12, 28, 17 },
            { 1, 15, 23, 26 },
            { 5, 18, 31, 10 },
            { 2, 8, 24, 14 },
            { 32, 27, 3, 9 },
            { 19, 13, 30, 6 },
            { 22, 11, 4, 25 }
        };

        private int[,] ip_1 = new int[8, 8]
        {
            { 40, 8, 48, 16, 56, 24, 64, 32 },
            { 39, 7, 47, 15, 55, 23, 63, 31 },
            { 38, 6, 46, 14, 54, 22, 62, 30 },
            { 37, 5, 45, 13, 53, 21, 61, 29 },
            { 36, 4, 44, 12, 52, 20, 60, 28 },
            { 35, 3, 43, 11, 51, 19, 59, 27 },
            { 34, 2, 42, 10, 50, 18, 58, 26 },
            { 33, 1, 41, 9, 49, 17, 57, 25 }
        };

        private string shiftLeft(string s, int roundNumber)
        {
            int noOfShifts = leftshiftArray[roundNumber];
            string right = "";
            string left = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (i < noOfShifts)
                {
                    right += s[i];
                }
                else
                {
                    left += s[i];
                }
            }
            return left + right;
        }

        private string xor(string s1, string s2)
        {
            string result = "";
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1.ElementAt(i) == s2.ElementAt(i))
                {
                    result += "0";
                }
                else
                {
                    result += "1";
                }
            }
            return result;
        }

        private string extendWithZeros(string binary)
        {
            string output = "";
            int diff = 64 - binary.Length;
            string zeros = "";
            if (diff > 0)
            {
                for (int p = 0; p < diff; p++)
                {
                    zeros += "0";
                }
                output = zeros + binary;
            }
            return output;
        }

        private List<string> permConcat(string binary, int pcMode)
        {
            int r = 0;
            int c = 0;
            List<string> list = new List<string>();
            switch (pcMode)
            {
                case 1:
                    r = 8;
                    c = 7;
                    for (int i = 0; i < r; i++)
                    {
                        for (int j = 0; j < c; j++)
                        {
                            list.Add(binary.ElementAt(pc1[i, j] - 1).ToString());
                        }
                    }
                    break;

                case 2:
                    r = 6;
                    c = 8;
                    for (int i = 0; i < r; i++)
                    {
                        for (int j = 0; j < c; j++)
                        {
                            list.Add(binary.ElementAt(pc2[i, j] - 1).ToString());
                        }
                    }
                    break;

                case 3:
                    r = 8;
                    c = 8;
                    for (int i = 0; i < r; i++)
                    {
                        for (int j = 0; j < c; j++)
                        {
                            list.Add(binary.ElementAt(ip[i, j] - 1).ToString());
                        }
                    }
                    break;

                case 4:
                    r = 8;
                    c = 6;
                    for (int i = 0; i < r; i++)
                    {
                        for (int j = 0; j < c; j++)
                        {
                            list.Add(binary.ElementAt(ep[i, j] - 1).ToString());
                        }
                    }
                    break;
                case 5:
                    r = 8;
                    c = 4;
                    for (int i = 0; i < r; i++)
                    {
                        for (int j = 0; j < c; j++)
                        {
                            list.Add(binary.ElementAt(p2[i, j] - 1).ToString());
                        }
                    }
                    break;
                case 6:
                    r = 8;
                    c = 8;
                    for (int i = 0; i < r; i++)
                    {
                        for (int j = 0; j < c; j++)
                        {
                            list.Add(binary.ElementAt(ip_1[i, j] - 1).ToString());
                        }
                    }
                    break;
            }

            return list;
        }

        private string divideConcat(List<string> pc1out, int roundNumber)
        {
            string left = "";
            string right = "";
            string c = "";
            string d = "";
            for (int i = 0; i < pc1out.Count; i++)
            {
                if (i < (pc1out.Count / 2))
                {
                    left += pc1out[i];
                }
                else
                {
                    right += pc1out[i];
                }
            }

            //int numberOfShift = leftshiftArray[roundNumber];
            c = shiftLeft(left, roundNumber);
            d = shiftLeft(right, roundNumber);
            left = "";
            right = "";
            return c + d;
        }

        private string divideConcat(string prev_round, int roundNumber)
        {
            string left = "";
            string right = "";
            for (int i = 0; i < prev_round.Length; i++)
            {
                if (i < (prev_round.Length / 2))
                {
                    left += prev_round.ElementAt(i);
                }
                else
                {
                    right += prev_round.ElementAt(i);
                }
            }

            int numberOfShift = leftshiftArray[roundNumber];
            ci = shiftLeft(left, numberOfShift);
            di = shiftLeft(right, numberOfShift);
            return ci + di;
        }

        private string listToString(List<string> list)
        {
            string finalString = "";
            foreach (string s in list)
            {
                finalString += s;
            }
            return finalString;
        }

        private List<string> generateKeys(string initialKey)
        {
            string shiftRes = "";
            List<string> keys = new List<string>();
            List<string> pc1output = new List<string>();
            List<string> pc2output = new List<string>();
            string kBinary = Convert.ToString(Convert.ToInt64(initialKey, 16), 2);
            kBinary = extendWithZeros(kBinary);

            pc1output = permConcat(kBinary, 1);
            shiftRes = divideConcat(pc1output, 0);
            pc2output = permConcat(shiftRes, 2);
            keys.Add(listToString(pc2output));
            for (int i = 1; i < 16; i++)
            {
                shiftRes = divideConcat(shiftRes, i);
                pc2output = permConcat(shiftRes, 2);
                keys.Add(listToString(pc2output));
            }
            return keys;
        }

        private List<string> messageDivide(string binary)
        {
            string l = "";
            string r = "";
            List<string> messageLR = new List<string>();
            for (int i = 0; i < binary.Length; i++)
            {
                if (i < (binary.Length / 2))
                {
                    l += binary[i];
                }
                else
                {
                    r += binary[i];
                }
            }
            messageLR.Add(l);
            messageLR.Add(r);
            return messageLR;
        }

        private List<string> divideSBlocks(string binary)
        {
            int counter = 0;
            string block = "";
            List<string> blocks = new List<string>();
            foreach (char c in binary)
            {
                if (counter < 6)
                {
                    block += c;
                    counter++;
                }
                if (counter == 6)
                {
                    counter = 0;
                    blocks.Add(block);
                    block = "";
                }

            }
            return blocks;
        }

        private string extendWithZeros2(string binary)
        {
            int diff = 4 - binary.Length;
            string zeros = "";
            if (diff > 0)
            {
                for (int p = 0; p < diff; p++)
                {
                    zeros += "0";
                }

            }
            return zeros + binary;
        }

        private string calcSBox(List<string> sInput)
        {
            string rBin = "";
            int rDec = 0;

            string cBin = "";
            int cDec = 0;

            string value = "";
            string bvalue = "";
            string evalue = "";
            string fvalue = "";

            for (int i = 0; i < sInput.Count; i++)
            {
                rBin = sInput[i].ElementAt(0).ToString() + sInput[i].ElementAt(5).ToString();
                rDec = Convert.ToInt32(rBin, 2);

                cBin = sInput[i].ElementAt(1).ToString() + sInput[i].ElementAt(2).ToString() + sInput[i].ElementAt(3).ToString() + sInput[i].ElementAt(4).ToString();
                cDec = Convert.ToInt32(cBin, 2);

                switch (i + 1)
                {
                    case 1:
                        value = st1[rDec, cDec].ToString();
                        break;
                    case 2:
                        value = st2[rDec, cDec].ToString();
                        break;
                    case 3:
                        value = st3[rDec, cDec].ToString();
                        break;
                    case 4:
                        value = st4[rDec, cDec].ToString();
                        break;
                    case 5:
                        value = st5[rDec, cDec].ToString();
                        break;
                    case 6:
                        value = st6[rDec, cDec].ToString();
                        break;
                    case 7:
                        value = st7[rDec, cDec].ToString();
                        break;
                    case 8:
                        value = st8[rDec, cDec].ToString();
                        break;
                }
                bvalue = Convert.ToString(Convert.ToInt64(value, 10), 2); //convert to binary.
                evalue = extendWithZeros2(bvalue);                      //extend to 4 bits.
                fvalue += evalue;                                         //append value. (final txt) note: check if = 32-bits.
            }
            return fvalue;
        }

        private string calcSBox(string sBoxIn)
        {
            int Value = 0;
            string index;
            string BinVal = "";
            for (int i = 0; i < 48; i += 6)
            {
                index = "";
                index += sBoxIn[i];
                index += sBoxIn[i + 5];
                int row_i = Convert.ToInt32(index, 2);
                index = "";
                index += sBoxIn[i + 1];
                index += sBoxIn[i + 2];
                index += sBoxIn[i + 3];
                index += sBoxIn[i + 4];
                int col_i = Convert.ToInt32(index, 2);
                switch (i)
                {
                    case 0:
                        Value = st1[row_i, col_i];
                        break;
                    case 1:
                        Value = st2[row_i, col_i];
                        break;
                    case 2:
                        Value = st3[row_i, col_i];
                        break;
                    case 3:
                        Value = st4[row_i, col_i];
                        break;
                    case 4:
                        Value = st5[row_i, col_i];
                        break;
                    case 5:
                        Value = st6[row_i, col_i];
                        break;
                    case 6:
                        Value = st7[row_i, col_i];
                        break;
                    case 7:
                        Value = st8[row_i, col_i];
                        break;
                }
                BinVal += Convert.ToString(Value, 2).PadLeft(4, '0');

            }
            return BinVal;
        }

        private List<string> msgfunction(string left, string right, string key)
        {
            List<string> sBlocks = new List<string>();
            string temp = left;
            string l = "";
            string r = "";
            List<string> leftRight = new List<string>();
            l = right;
            List<string> eOutput = permConcat(right, 4);
            string res1 = xor(key, listToString(eOutput));
            sBlocks = divideSBlocks(res1);
            string sboxRes = calcSBox(sBlocks);
            List<string> pOutput = permConcat(sboxRes, 5);
            r = xor(left, listToString(pOutput));
            leftRight.Add(l);
            leftRight.Add(r);
            return leftRight;
        }

        private string ecryptMessage(string plainText, List<string> keyList)
        {
            List<string> leftRightNew = new List<string>();
            string l = "";
            string r = "";
            string text = "";
            string messBinary = Convert.ToString(Convert.ToInt64(plainText, 16), 2);
            messBinary = extendWithZeros(messBinary);
            List<string> ipOut = permConcat(messBinary, 3);
            List<string> leftRight = messageDivide(listToString(ipOut));
            l = leftRight[0];
            r = leftRight[1];
            for(int i = 0; i < 16; i++)
            {
                leftRightNew = msgfunction(l, r, keyList[i]);
                l = leftRightNew[0];
                r = leftRightNew[1];
                //Console.Write(l + "     " + r);
                //Console.WriteLine(l);
                //Console.WriteLine(r);
                //Console.WriteLine(keyList[i]);
            }
            text = r + l;
            text = listToString(permConcat(text, 6)) ;
            return text;
        }

        public string encrypt(string plainText, string key)
        {
            List<string> keys = generateKeys(key);
            string binaryMsg = ecryptMessage(plainText, keys);
            //Console.WriteLine(binaryMsg);
            //Console.WriteLine("1000010111101000000100110101010000001111000010101011010000000101");
            //binaryMsg = Convert.ToString(Convert.ToInt64(binaryMsg, 2), 16);
            binaryMsg = Convert.ToInt64(binaryMsg, 2).ToString("X");
            binaryMsg = "0x" + binaryMsg;
            return binaryMsg;
        }

        public string decrypt(string cipherText, string key)
        {
            List<string> keys = generateKeys(key);
            return "";
        }
    }
}
