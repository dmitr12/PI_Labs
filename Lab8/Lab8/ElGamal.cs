using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lab8
{
    class ElGamal
    {
        int st = 168, en = 181;
        List<BigInteger> openKey;
        List<BigInteger> secretKey;

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
            BigInteger p = GetP();
            Console.WriteLine($"p={p}");
            BigInteger g = GetPrimitiveRoot(p);
            Console.WriteLine($"g={g}");
            BigInteger x = new Random().Next(1, (int)p - 1);
            Console.WriteLine($"x={x}");
            BigInteger y = new BigInteger((Math.Pow((double)g, (double)x))) % p;
            Console.WriteLine($"y={y}");
            openKey = new List<BigInteger>();
            openKey.AddRange(new BigInteger[] { p, g, y });
            Console.WriteLine($"OpenKey: {openKey[0]},{openKey[1]},{openKey[2]}");
            secretKey = new List<BigInteger>();
            secretKey.AddRange(new BigInteger[] { p, g, x });
            Console.WriteLine($"SecretKey: {secretKey[0]},{secretKey[1]},{secretKey[2]}");
            Console.WriteLine("Keys generated.");
        }

        public List<BigInteger> Encrypt(string text)
        {
            List<BigInteger> array = new List<BigInteger>();
            Random random = new Random();
            for (int i = 0; i != text.Length; i++)
            {
                int k = random.Next(1, (int)openKey[0] - 1);
                BigInteger a = BigInteger.Pow(openKey[1], k) % openKey[0];
                BigInteger b = (BigInteger.Pow(openKey[2], k) * (int)text[i]) % openKey[0];
                array.Add(a);
                array.Add(b);
            }
            return array;
        }

        public string Decrypt(int length_text, List<BigInteger> array_number)
        {
            string save_text = "";
            BigInteger integer;

            for (int i = 0; i < array_number.Count; i+=2)
            {
                integer = (array_number[i+1] * (BigInteger.Pow(array_number[i], (int)(secretKey[0] - 1 - secretKey[2])))) % secretKey[0];
                save_text += (char)integer;
            }
            return save_text;
        }

        public BigInteger GetP()
        {
            Random rand = new Random();
            BigInteger p = new BigInteger(rand.Next(st,en));
            while (!(IsSimple(p)))
                p++;
            return p;
        }

        public BigInteger GetPrimitiveRoot(BigInteger module)
        {
            BigInteger funcE = module - 1;
            List<BigInteger> factors = Factorization(funcE);
            for(int g = 1; g <= module; g++)
            {
                List<BigInteger> list = new List<BigInteger>();
                for (BigInteger i = 0; i < factors.Count; i++)
                    list.Add(new BigInteger(Math.Pow(g, (double)(funcE / factors[(int)i]))) % module);
                if (!list.Contains(1))
                    return g;
            }
            return 0;
        }

        public List<BigInteger> Factorization(BigInteger x)
        {
            List<BigInteger> res = new List<BigInteger>();
            for (BigInteger i = 2; i <= new BigInteger(Math.Sqrt((double)x)); i++)
            {
                while (x % i == 0)
                {
                    res.Add(i);
                    x /= i;
                }
            }
            if (x != 1)
                res.Add(x);
            return res;
        }

        BigInteger GetObr(BigInteger e, BigInteger funcE)
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
    }
}
