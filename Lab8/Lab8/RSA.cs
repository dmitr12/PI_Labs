using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lab8
{
    class RSA
    {
        int sizeModule;
        BigInteger p;
        BigInteger q;
        List<BigInteger> openKey;
        List<BigInteger> secretKey;

        public RSA(int sizeModule)
        {
            this.sizeModule = sizeModule;
        }

        public bool IsSimple(BigInteger num)
        {
            for (BigInteger i = 2; i <= new BigInteger(Math.Sqrt((double)num)); i++)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }

        public void GenerateKeys()
        {
            Console.WriteLine("Generating keys...");
            openKey = new List<BigInteger>();
            secretKey = new List<BigInteger>();
            Set_P_Q(sizeModule);
            BigInteger n = p * q;
            BigInteger funcEiler = (p - 1) * (q - 1);
            BigInteger e = GetE(funcEiler);
            openKey.AddRange(new BigInteger[] { e, n });
            Console.WriteLine($"(e,n)-({e},{n})");
            BigInteger d = GetD(e, funcEiler);
            secretKey.AddRange(new BigInteger[] { d, n });
            Console.WriteLine($"(d,n)-({d},{n})");
            Console.WriteLine("Keys generated");
        }

        public List<BigInteger> Encrypt(string message)
        {
            List<BigInteger> res = new List<BigInteger>();
            foreach (int c in message)
                res.Add(((BigInteger.Pow(c, (int)openKey[0])) % openKey[1]));
            return res;
        }

        public List<BigInteger> Decrypt(List<BigInteger> decrypt)
        {
            List<BigInteger> res = new List<BigInteger>();
            foreach(var v in decrypt)
                res.Add(((BigInteger.Pow(v, (int)secretKey[0])) % secretKey[1]));
            return res;
        }

        BigInteger GetE(BigInteger fE)
        {
            if (fE > 65537)
                return 65537;
            else if (fE > 257)
                return 257;
            else if (fE > 17)
                return 17;
            return 3;
        }

        BigInteger GetD(BigInteger e, BigInteger funcE) 
        {
            int d = 2;
            while (true)
            {
                if ((e * d) % funcE == 1)
                    break;
                else
                    d++;
            }
            return new BigInteger(d);
        }

       void Set_P_Q(int sizeModule)
       {
            Random rand = new Random();
            p = new BigInteger(0);
            q = new BigInteger(0);    
            for (BigInteger i = BigInteger.Pow(2, sizeModule / 2)+rand.Next(0, 100) ; i<i*100; i++)
            {           
                if (p != 0)
                    if (IsSimple(i))
                        q = i;
                if (p != 0 && q != 0)
                    break;
                if (IsSimple(i))
                {
                    p = i;
                    continue;
                }
            }
       }
    }
}
