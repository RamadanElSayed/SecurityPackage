using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public int Encrypt(int p, int q, int M, int e)
        {
           
            int n = p * q;
            int totient = (p - 1) * (q - 1);
            ulong c = 0;
            ulong result = 1;
            for (int i = 0; i < e / 2; i++)
            {
                c = (ulong)((M * M) % n);
                result *= c;
                result %= (ulong)n;
            }
            if (e % 2 == 1)
            {
                c = (ulong)(M % n);
                result *= c;
                result %= (ulong)n;
            }
            result %= (ulong)n;
            return (int)result;
        }
        private int EE(int b, int m)
        {
            int A1 = 1, A2 = 0, A3 = m, B1 = 0, B2 = 1, B3 = b;

            while (true)
            {
                if (B3 == 0)
                    return 0;
                if (B3 == 1)
                {
                    while (B2 < 0)
                        B2 += m;
                    return B2;
                }
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
        public int Decrypt(int p, int q, int C, int e)
        {
            int n = p * q;
            int totient = (p - 1) * (q - 1);
            ulong c = 0;
            int d = EE(e, totient);
            
            //if (d > n)
            //    d %= n;
            ulong result = 1;
            for (int i = 0; i < d / 2; i++)
            {
                c = (ulong)((C * C) % n);
                result *= c;
                result %= (ulong)n;
            }
            if (d % 2 == 1)
            {
                c = (ulong)(C % n);
                result *= c;
                result %= (ulong)n;
            }
            result %= (ulong)n;
            return (int)result;
        }
       

    }
}
