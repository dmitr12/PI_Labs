using System;
using System.Diagnostics;
using static System.Console;

namespace Lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            MD5 md5 = new MD5();
            Stopwatch sw = new Stopwatch();
            while (true)
            {
                WriteLine("Введите слово:");
                string msg = ReadLine();
                ForegroundColor = ConsoleColor.Green;
                sw.Restart();
                WriteLine(md5.GetHash(msg));
                sw.Stop();
                ResetColor();
                WriteLine($"Время: {sw.ElapsedMilliseconds} мс.");
            }
        }
    }
}