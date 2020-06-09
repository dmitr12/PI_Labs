using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    class Cipher
    {
        char[,] massChar;
        string rs = String.Empty;
        List<int> list1;
        List<int> list2;
        public string RoutePerestShifr(string msg)
        {
            int counter = 0;
            string res=String.Empty;
            char[,] massChar = new char[25, (msg.Length / 25) + 1];
            for(int i=0;i<massChar.GetLength(1);i++)
                for(int j=0;j<massChar.GetLength(0);j++)
                {
                    if (counter < msg.Length)
                        massChar[j,i] = msg[counter];
                    counter++;
                }
            for (int i = 0; i < massChar.GetLength(0); i++)
                for (int j = 0; j < massChar.GetLength(1); j++)
                    res += massChar[i, j];
            return res;
        }

        public string RoutePerestDeshifr(string msg, int length)
        {
            int counter = 0;
            string res=String.Empty;
            char[,] massChar = new char[25, (length / 25) + 1];
            for (int i = 0; i < massChar.GetLength(0); i++)
                for (int j = 0; j < massChar.GetLength(1); j++)
                {
                    if (counter < msg.Length)
                        massChar[i,j] = msg[counter];
                    counter++;
                }
            for (int i = 0; i < massChar.GetLength(1); i++)
                for (int j = 0; j < massChar.GetLength(0); j++)
                    res += massChar[j,i];
            return res.Remove(length);
        }

        public string MultiPerestShifr(string key1, string key2, string mes)
        {
            int counter = 0, cn=0;
            rs = mes;
            bool fl = true;
            key1 = key1.ToLower(); key2 = key2.ToLower();
            char[] c1 = key1.ToLower().ToCharArray();
            char[] c2 = key2.ToLower().ToCharArray();
            string k1 = String.Empty, k2 = String.Empty, res=String.Empty;
            Array.Sort(c1); Array.Sort(c2);
            foreach (char ch in c1)
                k1 += ch;
            foreach (char t in c2)
                k2 += t;
            list1 = new List<int>();
            list2 = new List<int>();
            for(int i=0;i<key1.Length;i++)
            {
                if (list1.Contains(k1.IndexOf(key1[i])))
                {
                    while (fl)
                    {
                        if (!list1.Contains(k1.IndexOf(key1[i]) + cn))
                        {
                            fl = false;
                            list1.Add(k1.IndexOf(key1[i]) + cn);
                        }
                        else cn++;
                    }
                }
                else {
                    list1.Add(k1.IndexOf(key1[i]));
                    fl = true;
                    cn = 0;
                } 
            }
            for (int i = 0; i < key2.Length; i++)
            {
                if (list2.Contains(k2.IndexOf(key2[i])))
                {
                    while (fl)
                    {
                        if (!list2.Contains(k2.IndexOf(key2[i]) + cn))
                        {
                            fl = false;
                            list1.Add(k2.IndexOf(key2[i]) + cn);
                        }
                        else cn++;
                    }
                }
                else
                {
                    list2.Add(k2.IndexOf(key2[i]));
                    fl = true;
                    cn = 0;
                }
            }
            massChar = new char[key2.Length, key1.Length];
            while (counter < mes.Length)
            {
                for (int i = 0; i < massChar.GetLength(0); i++)
                {
                    for (int j = 0; j < massChar.GetLength(1); j++)
                    {
                        if (counter < mes.Length)
                            massChar[i, j] = mes[counter++];
                        else
                            massChar[i, j] = ' ';
                    }

                }
                SwapStolb(ref massChar, massChar.GetLength(0), list1.ToArray());
                SwapStr(ref massChar, massChar.GetLength(1), list2.ToArray());
                for (int i = 0; i < massChar.GetLength(1); i++)
                {
                    for (int j = 0; j < massChar.GetLength(0); j++)
                        res+=massChar[j,i];
                }
            }
            return res;
        }

        public string MultiPerestDeshifr(string key1, string key2, string mes)
        {
            string res = String.Empty;
            int counter = 0;
            Dictionary<int, int> dict1 = new Dictionary<int, int>();
            Dictionary<int, int> dict2 = new Dictionary<int, int>();
            List<int> l1 = new List<int>();
            List<int> l2 = new List<int>();
            List<int> res1 = new List<int>();
            List<int> res2 = new List<int>();
            foreach (int h in list1)
                l1.Add(h);
            foreach (int y in list2)
                l2.Add(y);
            list1.Sort();list2.Sort();
            for (int i = 0; i < l1.Count; i++)
                dict1.Add(l1[i], list1[i]);
            for (int i = 0; i < l2.Count; i++)
                dict2.Add(l2[i], list2[i]);
            for (int i = 0; i < dict1.Count; i++)
                res1.Add(dict1[i]);
            for (int i = 0; i < dict2.Count; i++)
                res2.Add(dict2[i]);
            while (counter < mes.Length)
            {
                for (int i = 0; i < massChar.GetLength(0); i++)
                {
                    for (int j = 0; j < massChar.GetLength(1); j++)
                    {
                        if (counter < mes.Length)
                            massChar[i, j] = mes[counter++];
                        else
                            massChar[i, j] = ' ';
                    }

                }
                SwapStolb(ref massChar, massChar.GetLength(0), res1.ToArray());
                SwapStr(ref massChar, massChar.GetLength(1), res2.ToArray());
                for (int i = 0; i < massChar.GetLength(0); i++)
                {
                    for (int j = 0; j < massChar.GetLength(1); j++)
                        res += massChar[i,j];
                }
            }
            return rs;
        }

        void SwapInt(ref int n1, ref int n2)
        {
            int b = n1;
            n1 = n2;
            n2 = b;
        }
        void SwapStolb(ref char[,] massChar, int countStr, int[] arr)
        {
            char buf;
            for(int i=0;i<arr.Length;i++)
            {
                while(arr[i]!=i)
                {
                    for(int k=0;k<countStr;k++)
                    {
                        buf = massChar[k,i];
                        massChar[k, i] = massChar[k, arr[i]];
                        massChar[k, arr[i]] = buf;
                    }
                    SwapInt(ref arr[i], ref arr[arr[i]]);
                }
            }
        }

        void SwapStr(ref char[,] massChar, int countStlb, int[] arr)
        {
            char buf;
            for (int i = 0; i < arr.Length; i++)
            {
                while (arr[i] != i)
                {
                    for (int k = 0; k < countStlb; k++)
                    {
                        buf = massChar[i,k];
                        massChar[i,k] = massChar[arr[i], k];
                        massChar[arr[i], k] = buf;
                    }
                    SwapInt(ref arr[i], ref arr[arr[i]]);
                }
            }
        }
    }
}
