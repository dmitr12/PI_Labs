using System;
using System.Collections.Generic;
using System.Text;

namespace Lab7
{
    class Backpack
    {
        int countBits = 32;
        double a = 0, n = 0;
        public List<double> Encrypt(string message, List<double> openKey)
        {
            List<double> encryptTxt = new List<double>();
            string bin;
            double weight = 0;
            foreach (char c in message)
            {
                bin = GetBin(c, 8);
                for (int i = 0; i < bin.Length; i++)
                    if (bin[i] == '1')
                        weight += openKey[i];
                encryptTxt.Add(weight);
                weight = 0;
            }
            return encryptTxt;
        }

        public List<double> Decrypt(List<double> encrypt, List<double> secretKey)
        {
            List<double> decrypt = new List<double>();
            string decryptTxt = "";
            decimal d;
            double aMinus = GetObr(a, n);
            Console.WriteLine($"a={a}");
            Console.WriteLine($"aMinus={aMinus}");
            Console.WriteLine($"n={n}");
            for (int i = 0; i < encrypt.Count; i++)
            {
                d = ((decimal)encrypt[i] * (decimal)aMinus) % (decimal)n;
                decrypt.Add((double)d);
                decryptTxt += GetChar((double)d, secretKey);
            }
            Console.WriteLine($"decrypt: {decryptTxt}");
            return decrypt;
        }

        public string GetBin(int number, int countBits)
        {
            string res = "";
            res = Convert.ToString(number, 2);
            while (res.Length != countBits)
                res = "0" + res;
            return res;
        }

        public ulong GetNumberFromBinString(string binNumber)
        {
            ulong res = 0;
            for (int i = 0; i < binNumber.Length; i++)
                if (binNumber[i] == '1')
                    res += (ulong)Math.Pow(2, binNumber.Length - 1 - i);
            return res;
        }

        public List<double> GenerateSecretKey(int z)
        {
            Random random = new Random();
            string binNumber = "1";
            List<double> secretKey = new List<double>();
            for (int i = 0; i < z; i++)
            {
                for (int j = 0; j < countBits; j++)
                    binNumber += random.Next(0, 2);
                countBits -= z / 2;
                secretKey.Add(GetNumberFromBinString(binNumber));
                binNumber = "1";
            }
            secretKey.Reverse();
            return secretKey;
        }

        public List<double> GenerateOpenKey(List<double> secretKey)
        {
            Random rand = new Random();
            List<double> openKey = new List<double>();
            foreach (var ui in secretKey)
                n += ui;
            n += (double)rand.Next(1, 10);
            a = GetMutuallySimpleNumber(n);
            foreach (double ui in secretKey)
                openKey.Add((ui * a) % n);
            return openKey;
        }

        double GetDecFromBin(string strBin)
        {
            double dec = 0;
            for (int i = 0; i < strBin.Length; i++)
                if (strBin[i] == '1')
                    dec += Math.Pow(2, strBin.Length - 1 - i);
            return dec;
        }

        public char GetChar(double d, List<double> secretKey)
        {
            string bin = "";
            for (int i = 0; i < secretKey.Count; i++)
            {
                if (d >= secretKey[secretKey.Count - 1 - i])
                {
                    bin = "1" + bin;
                    d -= secretKey[secretKey.Count - 1 - i];
                }
                else
                    bin = "0" + bin;
            }
            return (char)GetDecFromBin(bin);
        }

        public double GetMutuallySimpleNumber(double n)
        {
            for (ulong i = 10000; i < n; i++)
                if (Nod(i, n) == 1)
                    return i;
            return 0;
        }

        public double Nod(double a, double b)
        {
            if (a == 0)
                return b;
            else
            {
                while (b != 0)
                {
                    if (a > b)
                        a -= b;
                    else
                        b -= a;
                }
                return a;
            }
        }
        public double GetObr(double a, double b)
        {
            int i = 1;
            while (true)
            {
                if ((b * i + 1) % a == 0)
                    return (double)((b * i + 1) / a);
                i++;
            }
            return 0;
        }

        public List<double> Encrypt64(string message, List<double> openKey)
        {
            List<double> encryptTxt = new List<double>();
            string bin;
            double weight = 0;
            foreach (char c in message)
            {
                bin = GetBin(Base64.Table.IndexOf(c), 6);
                for (int i = 0; i < bin.Length; i++)
                    if (bin[i] == '1')
                        weight += openKey[i];
                encryptTxt.Add(weight);
                weight = 0;
            }
            return encryptTxt;
        }

        public List<double> Decrypt64(List<double> encrypt, List<double> secretKey)
        {
            List<double> decrypt = new List<double>();
            string decryptTxt = "";
            decimal d;
            double aMinus = GetObr(a, n);
            for (int i = 0; i < encrypt.Count; i++)
            {
                d = ((decimal)encrypt[i] * (decimal)aMinus) % (decimal)n;
                decrypt.Add((double)d);
                decryptTxt += GetChar64((double)d, secretKey);
            }
            Console.WriteLine($"decrypt: {decryptTxt}");
            return decrypt;
        }

        public char GetChar64(double d, List<double> secretKey)
        {
            string bin = "";
            for (int i = 0; i < secretKey.Count; i++)
            {
                if (d >= secretKey[secretKey.Count - 1 - i])
                {
                    bin = "1" + bin;
                    d -= secretKey[secretKey.Count - 1 - i];
                }
                else
                    bin = "0" + bin;
            }
            return Base64.Table[(int)GetDecFromBin(bin)];
        }
    }
}