using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace DES
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            Des des = new Des();
            WriteLine("Input string: ");
            string str = ReadLine();
            str = des.ToRigthLength(str);
            Console.WriteLine($"input to right length: {str}");
            string shifr = "";
            foreach (string s in des.MakeBlocks4Letter(str))
                shifr += des.Shifr(s, des.SdvigLeft, "home");
            WriteLine(shifr);
            WriteLine("\n\n");
            WriteLine("desh");
            string deshifr = "";
            List<string> dmb = des.MakeBlocks64Bit(shifr);
            for (int i = 0; i < dmb.Count; i++)
            {
                deshifr += des.Desifr(dmb[i], des.SdvigLeft, i);
            }
            WriteLine(deshifr + "\n\n\n");

            StringBuilder check1 = new StringBuilder();
            StringBuilder check2 = new StringBuilder();
            for (int i = 0; i < 64; i++)
            {
                check1.Append('0');
                check2.Append('0');
            }
            check2[63] = '1';
            des.CheckL(check1.ToString(), check2.ToString(), "home");
        }
    }
}