using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static System.Console;
using System.Diagnostics;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = System.Text.Encoding.UTF8;
            Stopwatch sw = new Stopwatch();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pl-PL");
            StringBuilder strB = new StringBuilder();
            string alph = "AaĄąBbCcĆćDdEeĘęFfGgHhIiJjKkLlŁłMmNnŃńOoÓóPpRrSsŚśTtUuWwYyZzŹźŻż";
            Cipher cipher = new Cipher(alph.Length);
            using (StreamReader sr = new StreamReader("text.txt"))
            {
                bool lastAdded;
                string s = sr.ReadToEnd();
                StringBuilder sb = new StringBuilder();
                Regex reg = new Regex(@"[AaĄąBbCcĆćDdEeĘęFfGgHhIіJjKkLlŁłMmNnŃńOoÓóPpRrSsŚśTtUuWwYyZzŹźŻż]");
                MatchCollection matches = reg.Matches(s);
                foreach (Match q in matches)
                    sb.Append(q.Value);
                WriteLine("ШИФР ЦЕЗАРЯ:\n");
                WriteLine("Исходный тескт:\n" + s);
                sw.Start();
                string shifrTsez = cipher.TsezShifr(s, 28, alph);
                sw.Stop();
                WriteLine("\nЗашифрованный текст:\n" + shifrTsez);
                WriteLine("\nВремя: " + sw.ElapsedTicks);
                sw.Restart();
                string rashTsez = cipher.TsezRash(shifrTsez, 28, alph);
                sw.Stop();
                WriteLine("\nРасшифрованный текст:\n" + rashTsez);
                WriteLine("\nВремя: " + sw.ElapsedTicks);
                WriteLine("\nШИФР ПОРТЫ:\n");
                sw.Restart();
                List<int> shifrPort = cipher.PortSifr(sb.ToString(), alph, out lastAdded);
                sw.Stop();
                WriteLine("Шифр:");
                foreach (int q in shifrPort)
                {
                    if (q < 10) Write("00" + q);
                    else if (q < 100) Write("0" + q);
                    else Write(q);
                }
                WriteLine("\n\nВремя: " + sw.ElapsedTicks);
                sw.Restart();
                string rashPort = cipher.PortDeshifr(shifrPort, alph, lastAdded);
                sw.Stop();
                WriteLine("\nДешифр:\n" + rashPort);
                WriteLine("\nВремя: " + sw.ElapsedTicks);
            }
        }

        static int Counter(char c, string s)
        {
            int counter = 0;
            foreach (char ch in s)
                if (c == ch)
                    counter++;
            return counter;
        }
    }
}