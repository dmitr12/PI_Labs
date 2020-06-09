using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static System.Console;

namespace Lab2
{
    class Cipher
    {
        public int[,] mass;
        public Cipher(int alphLength)
        {
            mass = new int[alphLength, alphLength];
            for (int i = 0; i < alphLength; i++)
                for (int j = 0; j < alphLength; j++)
                    mass[i, j] = i * alphLength + j + 1;
        }
        public string TsezShifr(string str, int key, string alph)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char item in str)
            {
                if (alph.Contains(item))
                    stringBuilder.Append(alph[(alph.IndexOf(item) + key * 2) % alph.Length]);
                else
                    stringBuilder.Append(item);
            }
            return stringBuilder.ToString();
        }

        public string TsezRash(string zash, int key, string alph)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char item in zash)
            {
                if (alph.Contains(item))
                {
                    int i = alph.IndexOf(item) - key * 2;
                    if (i < 0)
                        while (i < 0)
                            i += alph.Length;
                    stringBuilder.Append(alph[i % alph.Length]);
                }
                else stringBuilder.Append(item);
            }
            return stringBuilder.ToString();
        }

        public List<int> PortSifr(string str, string alph, out bool lastAdded)
        {
            List<int> res = new List<int>();
            if (str.Length % 2 != 0)
            {
                str += alph[0];
                lastAdded = true;
            }
            else lastAdded = false;
            for (int i = 0; i < str.Length; i += 2)
            {
                res.Add(mass[alph.IndexOf(str[i]), alph.IndexOf(str[i + 1])]);
            }
            return res;
        }

        public string PortDeshifr(List<int> list, string alph, bool lastAdded)
        {
            int i, j;
            List<int> lst = new List<int>();
            StringBuilder sb = new StringBuilder();
            for (int q = 0; q < list.Count; q++)
            {
                lst = CheckMass(list[q]);
                sb.Append(alph[lst[0]]);
                sb.Append(alph[lst[1]]);
            }
            if (lastAdded)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        List<int> CheckMass(int item)
        {
            for (int i = 0; i < mass.GetLength(1); i++)
                for (int j = 0; j < mass.GetLength(1); j++)
                    if (item == mass[i, j])
                        return new List<int> { i, j };
            return null;
        }
    }
}