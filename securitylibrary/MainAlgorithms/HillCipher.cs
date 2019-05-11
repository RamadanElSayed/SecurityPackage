using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher : ICryptographicTechnique<string, string>, ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            int[,] matrix = new int[2, plainText.Count / 2];
            int counter = 0;
            int b = 0;
            int column1 = 0, column2 = 0;
            for (int i = 0; i < plainText.Count / 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    matrix[j, i] = plainText[counter];


                    counter++;
                }

            }

            int[,] invKey = new int[2, 2];
            invKey[0, 0] = 0;
            int[,] submatrix = new int[2, 2];
            for (int k = 0; k < plainText.Count - 1 / 2; k++)
            {
                for (int i = k + 1; i < plainText.Count / 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {

                        submatrix[j, 0] = matrix[j, k];

                    }

                    for (int n = 0; n < 2; n++)
                    {
                        submatrix[n, 1] = matrix[n, i];

                    }



                    int det = submatrix[0, 0] * submatrix[1, 1] - submatrix[0, 1] * submatrix[1, 0];
                    if (det > 25)
                        det = det % 26;
                    if (det < 0)
                        while (det < 0)
                            det += 26;

                     b = EE(det, 26);
                    if (b == 0)
                    {
                        continue;
                    }
                    else
                    {
                        column1 = k;
                        column2 = i;
                        if (b > 25)
                            b = b % 26;
                        if (b < 0)
                            while (b < 0)
                                b += 26;

                        invKey[0, 0] = b * submatrix[1, 1];
                        invKey[0, 1] = -b * submatrix[1, 0];
                        invKey[1, 0] = -b * submatrix[0, 1];
                        invKey[1, 1] = b * submatrix[0, 0];
                        i = k = 7;
                        break;
                    }


                }
            }
            if (invKey[0, 0] ==0)
                throw new InvalidAnlysisException();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (invKey[i, j] > 25)
                        invKey[i, j] = invKey[i, j] % 26;
                    else if (invKey[i, j] < 0)
                        while (invKey[i, j] < 0)
                            invKey[i, j] += 26;


                }

            }

            int temp = invKey[0, 1];
            invKey[0, 1] = invKey[1, 0];
            invKey[1, 0] = temp;
            
            counter = 0;
            int[,] cipher = new int[2, cipherText.Count/2];
            for ( int i=0;i<cipherText.Count/2;i++)
            {
                for ( int j =0;j<2;j++)
                {
                    cipher[j, i] = cipherText[counter];
                    counter++;
                }
            }

            List<int> subCipher = new List<int>();
            int[,] matrix2by2 = new int[2, 2];
            for (int i = 0; i < 2; i++)
               matrix2by2[i,0]=cipher[i, column1];
            for (int i = 0; i < 2; i++)
                matrix2by2[i, 1] = cipher[i, column2];
            for ( int i=0;i<2;i++)
            {
                for (int j = 0; j < 2; j++)
                    subCipher.Add(matrix2by2[i, j]);
            }


            List<int> key = new List<int>();
            counter = 0;
            int multiplicationResult = 0;
            for (int i = 0; i < 3; i += 2)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {

                        multiplicationResult += subCipher[counter] *invKey[k, j]  ;
                        counter++;
                    }
                    if (multiplicationResult > 25)
                        multiplicationResult = multiplicationResult % 26;
                    counter -= 2;
                    key.Add(multiplicationResult);
                    multiplicationResult = 0;

                }
                counter += 2;
            }



            return key;
        }

        public string Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
        }

        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
           
            int matrixSize = (int)Math.Sqrt(key.Count);
            int[,] matrixKey = new int[matrixSize, matrixSize];
            List<int> plainText = new List<int>();
            int det = 0;
            int multiplicationResult = 0;
            int value = 0;
            int counter = 0;
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    matrixKey[i, j] = key[counter];
                    counter++;
                }
               
            }
            if (matrixSize==2)
            {
                det = matrixKey[0, 0] * matrixKey[1, 1] - matrixKey[0, 1] * matrixKey[1, 0];
                if (Math.Abs(det) != 1)
                    throw new InvalidAnlysisException();
                det = 1 / det;
                int temp = matrixKey[0, 0];
                matrixKey[0, 0] = matrixKey[1, 1];
                matrixKey[1, 1] = temp;
                matrixKey[0, 1] = -matrixKey[0, 1];
                matrixKey[1, 0] = -matrixKey[1, 0];
                for (int i = 0; i < matrixSize; i++)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        matrixKey[i, j] *= det;
                    }
                }

                counter = 0;
               
                for (int i = 0; i < cipherText.Count; i += matrixSize)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        for (int k = 0; k < matrixSize; k++)
                        {

                            multiplicationResult += matrixKey[j, k] * cipherText[counter];
                            counter++;
                        }
                        if (multiplicationResult > 25)
                            multiplicationResult = multiplicationResult % 26;
                        if (multiplicationResult < 0)
                            while (multiplicationResult < 0)
                                multiplicationResult += 26;
                        counter -= matrixSize;
                        plainText.Add(multiplicationResult);
                        multiplicationResult = 0;

                    }
                    counter += matrixSize;
                }
                return plainText;
            }

            det = calculateDet(matrixKey, matrixSize);
          
            if (det > 25)
                det = det % 26;
            else if (det < 0)
            {
                while (det < 0)
                    det += 26;
            }
           
            int b = EE(det, 26);
           
            if (b > 25)
                b = b % 26;
            else if (b < 0)
            {
                while (b < 0)
                    b += 26;
            }

            int[,] inverseKey = new int[matrixSize, matrixSize];
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    det = caldet(matrixKey, matrixSize, i, j);
                    if (det > 25)
                        det = det % 26;
                    else if (det < 0)
                    {
                        while (det < 0)
                            det += 26;
                    }
                    value = b * (int)Math.Pow(-1, i + j) * det;
                    if (value > 25)
                        value = value % 26;
                    else if (value < 0)
                    {
                        while (value < 0)
                            value += 26;

                    }
                    inverseKey[i, j] = value;
                }


            }

           
           
            int[,] trnsKey = new int[matrixSize, matrixSize];
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    trnsKey[i, j] = inverseKey[j, i];
                    
                }
                
            }
           
            counter = 0;
           
            for (int i = 0; i < cipherText.Count; i += matrixSize)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    for (int k = 0; k < matrixSize; k++)
                    {

                        multiplicationResult += trnsKey[j, k] * cipherText[counter];
                        counter++;
                    }
                    if (multiplicationResult > 25)
                        multiplicationResult = multiplicationResult % 26;
                    counter -= matrixSize;
                    plainText.Add(multiplicationResult);
                    multiplicationResult = 0;

                }
                counter += matrixSize;
            }

            return plainText;
        }
        public int caldet(int[,] matrix, int size, int i, int j)
        {
            List<List<int>> indices = new List<List<int>>();

            int counter = 0;
            for (int i1 = 0; i1 < size; i1++)
            {
                for (int j1 = 0; j1 < size; j1++)
                {
                    indices.Add(new[] { i1, j1 }.ToList());
                    counter++;
                }
            }

            for (int i1 = 0; i1 < indices.Count; i1++)
            {

                if (indices[i1][0] == i || indices[i1][1] == j)
                {
                    indices.RemoveAt(i1);
                    i1--;

                }
            }
            int result = matrix[indices[0][0], indices[0][1]] * matrix[indices[3][0], indices[3][1]] -
                matrix[indices[1][0], indices[1][1]] * matrix[indices[2][0], indices[2][1]];



            return result;
        }
        public int GCD(int a, int b)
        {
            int R = 0;
            if (b == 0)
                return a;
            R = a % b;
            a = b;
            b = R;
            return GCD(a, b);

        }
        private int EE(int b, int m)
        {   
            int A1 = 1, A2 = 0, A3 = m, B1 = 0, B2 = 1, B3 = b;

            while(true)
            {
                if (B3 == 0)
                    return 0;
                if(B3 ==1)
                    return B2;
                int Q = A3 / B3;
                int T1 = A1 - (Q * B1), T2 = A2 - (Q * B2), T3 = A3 - (Q * B3);
                A1 = B1;
                A2 = B2;
                A3 = B3;

                B1 = T1;
                B2 = T2;
                B3 = T3;
            }


            return 0;
        }
       

        public int calculateDet(int[,] matrix, int size)
        {
            if (size == 2)
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            int det = matrix[0, 0] * (matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1]) -
                matrix[0, 1] * (matrix[1, 0] * matrix[2, 2] - matrix[1, 2] * matrix[2, 0]) +
                matrix[0, 2] * (matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0]);

            return det;
        }
        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            int counter = 0;
            int matrixSize = (int)Math.Sqrt(key.Count);

            int multiplicationResult = 0;
            List<int> cipher = new List<int>();
            int[,] matrixKey = new int[matrixSize, matrixSize];
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    matrixKey[i, j] = key[counter];
                    counter++;
                }
            }
            counter = 0;

            for (int i = 0; i < plainText.Count; i += matrixSize)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    for (int k = 0; k < matrixSize; k++)
                    {

                        multiplicationResult += matrixKey[j, k] * plainText[counter];
                        counter++;
                    }
                    if (multiplicationResult > 25)
                        multiplicationResult = multiplicationResult % 26;
                    counter -= matrixSize;
                    cipher.Add(multiplicationResult);
                    multiplicationResult = 0;

                }
                counter += matrixSize;
            }

            return cipher;
       
        }

        public string Encrypt(string plainText, string key)
        {
            throw new NotImplementedException();
        }

        public List<int> Analyse3By3Key(List<int> plain3, List<int> cipher3)
        {
            int matrixSize = (int)Math.Sqrt(plain3.Count);
            int det = 0;
            int b = 0;
            int[,] plainMatrix = new int[matrixSize, matrixSize];
            int counter = 0;
            for ( int i=0;i<matrixSize;i++)
            {
                for ( int j =0;j<matrixSize;j++)
                {
                    plainMatrix[j, i] = plain3[counter];
                    counter++;
                }
            }
            int[,] subPlain = new int[matrixSize, matrixSize];
            subPlain[0, 0] = 0;
            for (int i=0;i<matrixSize;i++)
            {
                for(int j=i+1;j<matrixSize;j++)
                {
                    for(int k=j+1;k<matrixSize;k++)
                    {

                        for (int a = 0; a < matrixSize; a++)
                            subPlain[a, 0] = plainMatrix[a, i];
                        for (int d = 0; d < matrixSize; d++)
                            subPlain[d, 1] = plainMatrix[d, j];
                        for (int e = 0; e < matrixSize; e++)
                            subPlain[e, 2] = plainMatrix[e, k];

                         det = calculateDet(subPlain, matrixSize);
                        if (det > 25)
                            det = det % 26;
                        else if (det < 0)
                            while (det < 0)
                                det += 26;

                         b = EE(det, 26);
                        if (b == 0)
                            continue;
                       else if (b > 25)
                            b = b % 26;
                        else if (b < 0)
                            while (b < 0)
                                b += 26;

                    }
                }
            }

            if (subPlain[0, 0] == 0)
                throw new InvalidAnlysisException();
            int[,] inversePlain = new int[matrixSize, matrixSize];
            int value = 0;
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    det = caldet(subPlain, matrixSize, i, j);
                    if (det > 25)
                        det = det % 26;
                    else if (det < 0)
                    {
                        while (det < 0)
                            det += 26;
                    }
                    value = b * (int)Math.Pow(-1, i + j) * det;
                    if (value > 25)
                        value = value % 26;
                    else if (value < 0)
                    {
                        while (value < 0)
                            value += 26;

                    }
                    inversePlain[i, j] = value;
                }


            }
            int[,] trnsPlain = new int[matrixSize, matrixSize];
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    trnsPlain[i, j] = inversePlain[j, i];

                }

            }
            int[,] cipherMatrix = new int[matrixSize, matrixSize];
            counter = 0;
            for ( int i=0;i<matrixSize;i++)
            {
                for ( int j =0;j<matrixSize;j++)
                {

                    cipherMatrix[j, i] = cipher3[counter];
                    counter++;
                }
            }
            
            cipher3.Clear();
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {

                    cipher3.Add(cipherMatrix[i, j]);
                }
            }
            counter = 0;
            int multiplicationResult = 0;
            List<int> key = new List<int>();
            for (int i = 0; i < cipher3.Count; i += matrixSize)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    for (int k = 0; k < matrixSize; k++)
                    {

                        multiplicationResult += cipher3[counter]*trnsPlain[k, j]  ;
                        counter++;
                    }
                    if (multiplicationResult > 25)
                        multiplicationResult = multiplicationResult % 26;
                    counter -= 3;
                    key.Add(multiplicationResult);
                    multiplicationResult = 0;

                }
                counter += matrixSize;
            }

            return key;
        }

        public string Analyse3By3Key(string plain3, string cipher3)
        {
            throw new NotImplementedException();
        }
    }
}
