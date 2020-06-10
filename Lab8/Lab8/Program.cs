using System;
using System.Collections.Generic;
using System.Numerics;
using static System.Console;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("\nRSA:");
            RSA rsa = new RSA(8);
            rsa.GenerateKeys();
            WriteLine("Введите текст:");
            List<BigInteger> enc = rsa.Encrypt(ReadLine());
            WriteLine("Encrypt:");
            foreach (var v in enc)
                WriteLine(v);
            string decrypt = "";
            foreach (int s in rsa.Decrypt(enc))
                decrypt += (char)s;
            WriteLine("Decrypt: " + decrypt);
            WriteLine("\nElGamal: ");
            ElGamal eg = new ElGamal();
            eg.GenerateKeys();
            WriteLine("Введите текст:");
            string text = ReadLine();
            List<BigInteger> encrypt = eg.Encrypt(text);
            foreach (var v in encrypt)
                WriteLine(v);
            string decr = "";
            foreach (var r in eg.Decrypt(text.Length, encrypt))
                decr += r;
            WriteLine(decr);
        }
    }
}
