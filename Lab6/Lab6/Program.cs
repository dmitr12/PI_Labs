using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static System.Console;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            //Generator
            WriteLine("Линейный конгруэнтный генератор...");
            Generator generator = new Generator();
            WriteLine("Полученная последовательность:");
            sw.Start();
            IEnumerable<int> sequence = generator.Lkg(421, 1663, 7875, 1000);
            sw.Stop();
            foreach (int q in sequence)
                Write($"{q}, ");
            WriteLine($"\nЗатрачено времени: {sw.ElapsedTicks} тиков");
            //RC4
            WriteLine("\n\n\nRC4:");
            RC4 rc4 = new RC4(6, new List<int> { 15, 14, 13, 12, 11, 10 });
            WriteLine("Введите слово:");
            string str = ReadLine();
            List<string> encodeList = rc4.Encode(rc4.GetNBinBlocks(rc4.StrToBin(str)));
            List<string> decodeList = rc4.Decode(encodeList);
            WriteLine("Зашифрованное слово:");
            string sh = "";
            foreach (string w in encodeList)
                sh += w;
            WriteLine(sh);
            WriteLine("Расшифрованное слово:");
            WriteLine(rc4.GetTrueText(decodeList));
        }
    }
}
