using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Lab8
{
    class T1
    {   
        List<BigInteger> a = new List<BigInteger>();
        List<BigInteger> x = new List<BigInteger>();
        List<BigInteger> n = new List<BigInteger>();

        public T1()
        {
            GenerateA();
            GenerateX();
            GenerateN();
        }

        public void GenerateA()
        {
            Console.WriteLine("Generating a...");
            BigInteger start = 5;
            for(int i = 0; i < 5; i++)
            {
                start += new Random().Next(1, 4);
                a.Add(start);
            }
        }

        public void Check()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            BigInteger y = 0;
            for(int i = 0; i < 5; i++)
            {
                Console.WriteLine($"a={a[i]}");
                Console.WriteLine($"x={x[i]}");
                Console.WriteLine($"n={n[i]}");
                sw.Restart();
                y = new BigInteger(Math.Pow((double)a[i], (double)x[i])) % n[i];
                sw.Stop();
                Console.WriteLine($"y={y}");
                Console.WriteLine($"time: {sw.Elapsed}");
            }
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

        public void GenerateX()
        {
            Console.WriteLine("Generating x...");
            int st = 3;
            BigInteger n = new BigInteger(Math.Pow(10, st));
            for(int i = 0; i < 5; i++)
            {
                while (!IsSimple(n))
                    n++;
                x.Add(n);
                st += new Random().Next(1, 3);
                n = new BigInteger(Math.Pow(10,st));
            }
        }

        public void GenerateN()
        {
            Console.WriteLine("Generating n...");
            for (int i = 0; i < 5; i++)
                n.Add(GetBigIntFromString());
        }

        public BigInteger GetBigIntFromString()
        {
            BigInteger res=0;
            string str = "1";
            for (int i = 0; i < 1023; i++)
                str += new Random().Next(0, 2);
            for(int i=0;i<str.Length;i++)
            {
                if (str[str.Length-1 - i] == '1')
                    res += new BigInteger(Math.Pow(2, i));
            }
            return res;
        }
    }
}
