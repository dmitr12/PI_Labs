using System;

namespace Lab1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите число: ");
            int num = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nВведите модуль: ");
            int modulo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nЧисло обратное по модулю: " + Invers_Modulo(num,modulo));
        }
        static double? Invers_Modulo(int num, int modulo)
        {
            int x, y;
            int g = GCD(num, modulo, out x, out y);
            if(g != 1)
            {
                return null;
            }
            return (x % modulo + modulo) % modulo;
        }
        static int GCD(int a, int b, out int x, out int y)
        {
            if(a==0)
            {
                x = 0;
                y = 1;
                return b;
            }
            int x1, y1;
            int d = GCD(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }
    }
}
