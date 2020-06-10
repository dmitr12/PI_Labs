using System;
using static System.Console;

namespace Lab11
{
    class Program
    {
        static void Main(string[] args)
        {
            EK ek = new EK(-1, 1, 751, new int[] { 0, 1 }, 18, 11);
            while (true)
            {
                WriteLine("Введите:");
                WriteLine(ek.Check(ReadLine()));
            }
        }
    }
}
                                                                                                        