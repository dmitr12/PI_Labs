using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using static System.Console;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = System.Text.Encoding.UTF8;
            Stopwatch sw = new Stopwatch();
            Cipher cipher = new Cipher();
            string alph = "aąbcćdeęfghijklłmnńoóprsśtuwyzźż";
            using (StreamReader sr=new StreamReader("text.txt"))
            {
                string s = sr.ReadToEnd();
                sw.Start();
                string shRoute = cipher.RoutePerestShifr(s);
                sw.Stop();
                WriteLine("Маршрутная перестановка:\n");
                WriteLine("Исходный текст:\n"+s);
                WriteLine("\nЗашифрованный текст:\n" + shRoute);
                WriteLine("\nВремя: " + sw.ElapsedTicks);
                sw.Restart();
                string deshRoute = cipher.RoutePerestDeshifr(shRoute, s.Length);
                sw.Stop();
                for(int i=0;i<alph.Length;i++)
                {
                    WriteLine("{0}-{1}", alph[i], Counter(alph[i], shRoute));
                }
                WriteLine("\nРасшифрованный текст:\n" + deshRoute);
                WriteLine("\nВремя: " + sw.ElapsedTicks);
                WriteLine("\nМножественная перестановка:\n");
                sw.Restart();
                string shMulti = cipher.MultiPerestShifr("Дырда", "Дима", s);
                sw.Stop();
                sw.Restart();
                string deshMulti = cipher.MultiPerestDeshifr("Дырда", "Дима", shMulti);
                sw.Stop();
                WriteLine("Зашифрованный текст:\n" +shMulti);
                WriteLine("\nВремя: " + sw.ElapsedTicks);
                WriteLine("\nРасшифрованный текст:\n" + deshMulti);
                WriteLine("\nВремя: " + sw.ElapsedTicks);
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
}
