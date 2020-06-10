using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static System.Console;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Backpack bp = new Backpack();
            List<double> list = bp.GenerateSecretKey(8);
            double sum = 0;
            foreach (double ul in list)
                sum += ul;
            List<double> openKey = bp.GenerateOpenKey(list);
            WriteLine("Открытый ключ:");
            foreach (double q in openKey)
                WriteLine(q);
            WriteLine("Введите текст: ");
            sw.Start();
            List<double> encrypt = bp.Encrypt(ReadLine(), openKey);
            sw.Stop();
            WriteLine("Encrypt: ");
            foreach (var e in encrypt)
                WriteLine(e);
            WriteLine($"\nВремя: {sw.ElapsedMilliseconds} мс.");
            WriteLine("Закрытый ключ:");
            foreach (double ui in list)
                WriteLine(ui);
            WriteLine("\n\nDecrypt");
            sw.Restart();
            List<double> decrypt = bp.Decrypt(encrypt, list);
            sw.Stop();
            foreach (var d in decrypt)
                WriteLine(d);
            WriteLine($"\nВремя: {sw.ElapsedMilliseconds} мс.");
            //Base64
            WriteLine("\n\nBase64:");
            Backpack bp64 = new Backpack();
            List<double> secretKey64 = bp64.GenerateSecretKey(6);
            WriteLine("Закрытый ключ:");
            foreach (double sc in secretKey64)
                WriteLine(sc);
            List<double> openKey64 = bp64.GenerateOpenKey(secretKey64);
            WriteLine("Открытый ключ:");
            foreach (double ok in openKey64)
                WriteLine(ok);
            WriteLine("Введите текст:");
            List<double> encrypt64 = bp64.Encrypt64(ReadLine(), openKey64);
            WriteLine("Encrypt:");
            foreach (var t in encrypt64)
                WriteLine(t);
            WriteLine("Закрытый ключ:");
            foreach (var clk in secretKey64)
                WriteLine(clk);
            List<double> decrypt64 = bp64.Decrypt64(encrypt64, secretKey64);
            foreach (var d64 in decrypt64)
                WriteLine(d64);
        }
    }
}