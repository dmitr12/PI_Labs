using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Lab1
{
    public class Explorer
    {
        public void Menu()
        {
            WriteLine("Выберите задание.");
            WriteLine("1. НОД для двух чисел");
            WriteLine("2. НОД для трех чисел");
            WriteLine("3. Поиск всех простых чисел на промежутке [2,n]");
            WriteLine("4. Поиск всех простых чисел на промежутке [m,n]");
            WriteLine("5. Выход");
        }

        public int Nod(List<int> values)
        {
            int x=-1,y,q, r=-1;
            while (values.Count > 1)
            {
                values.Sort();
                x = values[0];y = values[1];
                while(true)
                {
                    q = y / x;
                    r = y - x * q;
                    if (r != 0)
                    {
                        y = x;
                        x = r;
                    }
                    else
                    {
                        values[0] = x;
                        values.RemoveAt(1);
                        break;
                    }
                }
            }
            return Math.Abs(x);
        }

        public List<int> FindSimple(int n, int x = 2)
        {
            List<int> list = new List<int>();
            for (int i = x; i <= n; i++)
                list.Add(i);
            int s = list[0], length=list.Count, counter=0;
            while(true)
            {
                for(int i = s; ; i++)
                {
                    if (i * s > list[list.Count-1])
                        break;
                    else
                        list.Remove(i * s);
                }
                if (length != list.Count)
                {
                    s = list[++counter];
                    length = list.Count;
                }
                else
                    break;
            }
            return list;
        }

        public List<int> FindSimpleProm(int m, int n)
        {
            List<int> res = new List<int>();
            int x = n;
            if (x > 3)
                x = int.Parse(Math.Floor(Math.Sqrt(n)).ToString());
            List<int> listSimpleValues = FindSimple(x);
            for (int i = m; i <= n; i++)
                res.Add(i);
            int counter=0;
            while(true)
            {
                for (int i = 0; i < res.Count; i++)
                    if (res[i] % listSimpleValues[counter] == 0 && !listSimpleValues.Contains(res[i]))
                        res.RemoveAt(i);
                counter++;
                if (counter == listSimpleValues.Count)
                    break;
            }
            if (res.Contains(1))
                res.Remove(1);
            return res;
        }
    }
}
