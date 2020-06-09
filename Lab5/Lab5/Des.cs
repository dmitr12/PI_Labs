using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Lab5
{
    class Des
    {
        
        readonly int sizeOfBlock = 64;
        readonly int sizeOfChar = 16;
        readonly int countOfRounds = 16;
        string key = "home";
        public string leftOfBlock, rigthOfBlock;
        List<int> tableRash = new List<int>();
        List<int> tableP = new List<int>();
        List<int> tableFirstPerestBit56Key = new List<int>();

        public Des()
        {
            tableRash.AddRange(new int[]
            {
                32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9,
                8, 9, 10, 11, 12, 13, 12, 13, 14, 15, 16, 17,
                16, 17, 18, 19, 20, 21, 20, 21, 22, 23, 24, 25,
                24, 25, 26, 27, 28, 29, 28, 29, 30, 31, 32, 1
            });
            tableP.AddRange(new int[]
            {
                16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23, 26, 5, 18, 31, 10,
                 2, 8, 24, 14, 32, 27, 3, 9, 19, 13, 30, 6, 22, 11, 4, 25
            });
            tableFirstPerestBit56Key.AddRange(new int[]
            {
                57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18,
                10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36,
                63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22,
                14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4
            });
        }

        public string Shifr(string input)
        {
            string inp = input;
            Console.WriteLine("Proccess:");
            Console.WriteLine("Input string: " + input);
            input = ToRigthLength(input);
            Console.WriteLine($"To right length: {inp}->{input}");
            input = ToBinaryFormat(input);
            Console.WriteLine($"To binnary format: {input}");
            input = FirstPerest(input);
            Console.WriteLine($"After first perest: {input}");
            MakeLAndRBlock(input);
            Console.WriteLine($"Left block: {leftOfBlock}");
            Console.WriteLine($"Rigth block: {rigthOfBlock}");
            Console.WriteLine($"Key: {key}");
            string binKey = ToBinaryFormat(key);
            Console.WriteLine($"Key to binnary format: {binKey}");
            string key56Bit = KeyTo56bit(binKey);
            Console.WriteLine($"Key 56 bit: {key56Bit}");

            //16 раз будет выполняться цикл
            
            return null;
        }

        public string KeyTo56bit(string binKey)
        {
            string correctKey = String.Empty;
            for (int i = 0; i < tableFirstPerestBit56Key.Count; i++)
                correctKey += binKey[tableFirstPerestBit56Key[i]-1];
            return correctKey;
        }

        public string GetKey48Bit(string key56Bit)
        {
            return key56Bit.Substring(8);
        }

        public string FunctionF(string right32bit, string key48bit)
        {
            string right48bit = ExtensionTo48Bit(right32bit);
            string xorRightKey = Xor(right48bit, key48bit);
            Console.WriteLine(xorRightKey);
            List<string> blocks6Bit = MakeBlocks6bit(xorRightKey);
            string res32Bit = Get32bitResultFromBlocksS(blocks6Bit);
            return res32Bit;
        }

        public string ExtensionTo48Bit(string halfBlock)
        {
            string res = String.Empty;
            foreach (int i in tableRash)
                res += halfBlock[i-1];
            return res;
        }

        public string ToRigthLength(string input)
        {
            while (((input.Length * sizeOfChar) % sizeOfBlock) != 0)
                input += "$";
            return input;
        }

        public string ToBinaryFormat(string input)
        {
            string result = String.Empty;
            for(int i = 0; i < input.Length; i++)
            {
                string bin = Convert.ToString(input[i], 2);
                while (bin.Length < sizeOfChar)
                    bin = "0" + bin;
                result += bin;
            }
            return result;
        }

        public string FirstPerest(string block)
        {
            int firstBit = 58;
            string res = String.Empty;
            int index;
            while(true)
            {
                if (firstBit < 0)
                    firstBit = 64 + firstBit + 2;
                if (firstBit == 0)
                    firstBit = 57;
                res += block[firstBit-1];
                if (firstBit == 7)
                    break;
                firstBit -= 8;
            }
            return res;
        }

        public void MakeLAndRBlock(string afterFirstPerest)
        {
            for (int i = 0; i < afterFirstPerest.Length; i++)
            {
                if (i < 32)
                    leftOfBlock += afterFirstPerest[i];
                else
                    rigthOfBlock += afterFirstPerest[i];
            }
        }

        public string Xor(string operand1, string operand2)
        {
            string res = String.Empty;
            for(int i=0;i<operand1.Length;i++)
            {
                bool a = Convert.ToBoolean(Convert.ToInt32(operand1[i].ToString()));
                bool b = Convert.ToBoolean(Convert.ToInt32(operand2[i].ToString()));
                if (a ^ b)
                    res += "1";
                else
                    res += "0";
            }
            return res;
        }

        public List<string> MakeBlocks6bit(string resultXor)
        {
            List<string> blocks = new List<string>();
            string block = String.Empty;
            for(int i = 0; i < resultXor.Length; i++)
            {
                block += resultXor[i];
                if ((i + 1) % 6 == 0)
                {
                    blocks.Add(block);
                    block = String.Empty;
                }
            }
            return blocks;
        }

        public string Get32bitResultFromBlocksS(List<string> blocks)
        {
            string result = String.Empty, bin;
            int decA, decB;
            for(int i = 0; i < blocks.Count; i++)
            {
                GetDecFromBin(blocks[i].Substring(0, 1) + blocks[i].Substring(5, 1), out decA);
                GetDecFromBin(blocks[i].Substring(1, 4), out decB);
                bin = Convert.ToString(S.DictionarySBlocks[i + 1][16 * decA + decB], 2);
                while (bin.Length < 4)
                    bin = "0" + bin;
                result += bin;
            }
            return result;
        }

        void GetDecFromBin(string strBin, out int dec)
        {
            dec = 0;
            for (int i = 0; i < strBin.Length; i++)
                if (strBin[i] == '1')
                    dec += (int)Math.Pow(2, strBin.Length - 1 - i);
        }
    }
}
