using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using static System.Console;

namespace Lab10
{
    class Program
    {
        public static bool IsCoprime(int a, int b)
        {
            return a == b
                   ? a == 1
                   : a > b
                        ? IsCoprime(a - b, b)
                        : IsCoprime(b - a, a);
        }
        static void Main(string[] args)
        {
            MD5Hash md5 = new MD5Hash();
            WriteLine("ЭЦП НА ОСНОВЕ RSA");
            WriteLine("-----Отправитеть-----");
            RSA rsa = new RSA(8);
            WriteLine("Введите сообщение:");
            string msg = ReadLine();
            string hashMessage = md5.GetHash(msg);
            List<BigInteger> secretKey = rsa.GetSecretKey();
            List<BigInteger> openKey = rsa.GetOpenKey();
            List<BigInteger> encryptHashMessageBySecretKey = rsa.Encrypt(hashMessage, secretKey);
            WriteLine("-----Получатель-----");
            string hashMessageReceiver = md5.GetHash(msg);
            WriteLine("Хэш полученного сообщения: " + hashMessageReceiver);
            List<BigInteger> decrypt = rsa.Decrypt(encryptHashMessageBySecretKey, openKey);
            string hashMessageReceiverDecryptByOpenKey = "";
            foreach (int s in decrypt)
                hashMessageReceiverDecryptByOpenKey += (char)s;
            WriteLine("Расшифрованная подпись: " + hashMessageReceiverDecryptByOpenKey);
            if (hashMessage == hashMessageReceiverDecryptByOpenKey)
            {
                WriteLine(hashMessage + " равна " + hashMessageReceiverDecryptByOpenKey);
                WriteLine("Подпись верна");
            }
            else
            {
                WriteLine(hashMessage + " не равна " + hashMessageReceiverDecryptByOpenKey);
                WriteLine("Подпись не верна");
            }



            WriteLine("ЭЦП ШНОРРА");

            WriteLine("Генерация ключей");

            BigInteger bi = new BigInteger(Math.Pow(2, 6));
            while (!MillerRabinTest(bi, 10))
            {
                bi++;
            }
            BigInteger p = bi;
            WriteLine("p=" + p);

            List<BigInteger> list = new List<BigInteger>();
            BigInteger n = p - 1;
            for (BigInteger i = 2; i < new BigInteger(Math.Sqrt((double)n)) + 1;)
            {
                if (n % i == 0)
                {
                    list.Add(i);
                    n /= i;
                }
                else
                {
                    ++i;
                }
            }
            if (n > 1)
                list.Add(n);
            foreach (var t in list)
                WriteLine(t);
            BigInteger q = list[list.Count - 1];
            WriteLine("q=" + q);

            BigInteger g;
            for (int i = 2; ; i++)
            {
                BigInteger b = new BigInteger(Math.Pow(i, (double)q));
                if (b % p == 1)
                {
                    g = i;
                    break;
                }
            }
            WriteLine("g=" + g);

            BigInteger x = q - (q / 2);
            WriteLine("Закрытый ключ x=" + x);

            double m = Math.Pow((double)g, (double)(x)) % (double)p;
            BigInteger y = new BigInteger(m);
            WriteLine("y=" + y);
            WriteLine($"Открытый ключ: {p};{q};{g};{y}");

            int k = 10;
            BigInteger a = new BigInteger((Math.Pow((double)g, k)) % (double)p);
            WriteLine("k=" + k);
            WriteLine("a=" + a);

            WriteLine("Введите сообщение:");
            string msg1 = ReadLine();
            int hH = H(msg1, (int)a) % (int)p;
            WriteLine("hH=" + hH);
            int ss = (k - ((int)x * hH)) % (int)(p - 1);
            while (ss < 0)
                ss += ((int)p - 1);
            WriteLine("ss=" + ss);
            int X = ((int)(new BigInteger(Math.Pow((int)g, ss)) % p) * (int)(new BigInteger(Math.Pow((int)y, hH)) % p)) % (int)p;
            WriteLine("X=" + X);
            if (H(msg1, (int)a) == H(msg1, X))
                WriteLine("Подпись верна");
        }

        public static bool MillerRabinTest(BigInteger n, int k)
        {
            if (n == 2 || n == 3)
                return true;
            if (n < 2 || n % 2 == 0)
                return false;
            BigInteger t = n - 1;
            int s = 0;
            while (t % 2 == 0)
            {
                t /= 2;
                s += 1;
            }
            for (int i = 0; i < k; i++)
            {
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] _a = new byte[n.ToByteArray().LongLength];
                BigInteger a;
                do
                {
                    rng.GetBytes(_a);
                    a = new BigInteger(_a);
                }
                while (a < 2 || a >= n - 2);
                BigInteger x = BigInteger.ModPow(a, t, n);
                if (x == 1 || x == n - 1)
                    continue;
                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, n);
                    if (x == 1)
                        return false;
                    if (x == n - 1)
                        break;
                }
                if (x != n - 1)
                    return false;
            }
            return true;
        }

        public static int H(string msg, int a)
        {
            int sumSymbols = 0;
            foreach (int c in msg)
                sumSymbols += c;
            string h = sumSymbols.ToString() + a.ToString();
            return int.Parse(h);
        }
    }
}