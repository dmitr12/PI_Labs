using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Lab1
{
    public class Router
    {
        Explorer exp;
        int choice=0, x, y, z, n, m;
        public Router(Explorer exp) => this.exp = exp;

        public void Start()
        {  
            do
            {
                exp.Menu();
                choice = int.Parse(ReadLine());
                switch (choice)
                {
                    case 1:
                        WriteLine("Введите первое число:");
                        x = int.Parse(ReadLine());
                        WriteLine("Введите второе число:");
                        y = int.Parse(ReadLine());
                        WriteLine($"НОД({x},{y})="+exp.Nod(new List<int> { x, y }));
                        break;
                    case 2:
                        WriteLine("Введите первое число:");
                        x = int.Parse(ReadLine());
                        WriteLine("Введите второе число:");
                        y = int.Parse(ReadLine());
                        WriteLine("Введите третье число:");
                        z = int.Parse(ReadLine());
                        WriteLine($"НОД({x},{y},{z})=" + exp.Nod(new List<int> { x, y, z }));
                        break;
                    case 3:
                        WriteLine("Введите n:");
                        n = int.Parse(ReadLine());
                        if(n<2)
                        {
                            WriteLine("Введенное n < 2");
                            break;
                        }
                        WriteLine($"Простые числа на промежутке [2,{n}]:");
                        foreach (int item in exp.FindSimple(n))
                            Write(item + " ");
                        WriteLine();
                        break;
                    case 4:
                        WriteLine("Введите m");
                        m = int.Parse(ReadLine());
                        WriteLine("Введите n");
                        n = int.Parse(ReadLine());
                        if(n<m || n<2 || m<0)
                        {
                            WriteLine("Промежуток введен неверно!");
                            break;
                        }
                        WriteLine($"Простые числа на промежутке [{m},{n}]:");
                        foreach (int item in exp.FindSimpleProm(m,n))
                            Write(item + " ");
                        break;
                    case 5:break;
                }
            } while (choice != 5);
        }
    }
}
