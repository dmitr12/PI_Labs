using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Des des = new Des();
            //WriteLine("Введите строку: ");
            //string str = ReadLine();
            //des.Shifr(str);

            WriteLine("Input...");
            string bin = ReadLine();
            string afterSdvig = "";
            StringBuilder sb = new StringBuilder("00");
            sb[1] = '1';
            WriteLine(sb);
            for (int i = 0; i < bin.Length; i++)
            {
                afterSdvig += bin[((i - 2) + bin.Length) % bin.Length];
                WriteLine(afterSdvig);

            }
            WriteLine(afterSdvig);



            //WriteLine("Строка после приведения к верной длине:");
            //str = des.ToRigthLength(str);
            //WriteLine(str);
            //WriteLine("Строка в двоичном формате:");
            //str = des.ToBinaryFormat(str);
            //WriteLine("{0}-{1}",str,str.Length);
            //WriteLine($"Пусть блок: {str}");
            //str = des.FirstPerest(str);
            //WriteLine(str);
            //des.MakeLAndRBlock(str);
            //string key48bit = des.GetKey48Bit(des.KeyTo56bit());
            //WriteLine(key48bit+"-"+key48bit.Length);
            //string rigthTo48bit = des.ExtensionTo48Bit(des.rigthOfBlock);
            //WriteLine("key48bit: " + key48bit);
            //WriteLine("rightTo48bit: "+rigthTo48bit);
            //string xorRightKey = des.Xor(rigthTo48bit, key48bit);
            //WriteLine("rigthTo48bit xor key48bit: " + xorRightKey);
            //string example = "000011";
            //string a = example.Substring(0,1)+example.Substring(5,1);
            //string b = example.Substring(1, 4);
            //WriteLine("example: " + example);
            //WriteLine("a: " + a);
            //WriteLine("b: " + b);
            //int decA = 0, decB=0;
            
            //for (int i = 0; i < a.Length; i++)
            //    if (a[i] == '1')
            //        decA += (int)Math.Pow(2, a.Length-1-i);
            //for (int i = 0; i < b.Length; i++)
            //    if (b[i] == '1')
            //        decB += (int)Math.Pow(2, b.Length-1-i);
            //int res = S.One[16 * decA + decB];
            //WriteLine(res);
            //WriteLine("decA: " + decA);
            //WriteLine("decB: " + decB);
            //List<string> blocks = des.MakeBlocks6bit(xorRightKey);
            //foreach (string block in blocks)
            //    WriteLine(block);
            //string bitResultFromBlocksS = des.Get32bitResultFromBlocksS(blocks);
            //WriteLine("\n\n" +bitResultFromBlocksS +"-"+bitResultFromBlocksS.Length);
            //WriteLine(des.FunctionF(des.rigthOfBlock, des.GetKey48Bit(des.KeyTo56bit())));

        }
    }
}
