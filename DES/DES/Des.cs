using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace DES
{
    class Des
    {

        readonly int sizeOfBlock = 64;
        readonly int sizeOfChar = 16;
        readonly int countOfRounds = 16;
        //string key = "home";
        List<string> d = new List<string>();
        string ds = "";
        public string LastKey { get; private set; }
        string keyForDesh = String.Empty;
        public string leftOfBlock, rigthOfBlock;
        List<int> tableRash = new List<int>();
        List<int> tableP = new List<int>();
        List<int> tableFirstPerestBit56Key = new List<int>();
        List<int> tableSzhimPerestKey = new List<int>();
        List<int> lastPerest = new List<int>();
        List<int> firstPerest = new List<int>();
        List<string> keys = new List<string>();

        Dictionary<int, int> SdvigKeyEveryRound = new Dictionary<int, int>
        {
            {1,1},{2,1},{3,2},{4,2},{5,2},{6,2},{7,2},{8,2},
            {9,1},{10,2},{11,2},{12,2},{13,2},{14,2},{15,2},{16,1}
        };

        public Des()
        {
            firstPerest.AddRange(new int[]
            {
                58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4,
                62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8,
                57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3,
                61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7
            });
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
            tableSzhimPerestKey.AddRange(new int[]
            {
                14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10,
                23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2,
                41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48,
                44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32
            });
            lastPerest.AddRange(new int[]
            {
                40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31,
                38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29,
                36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27,
                34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25
            });
        }

        public string Shifr(string input, Func<string, int, string> sdvig, string inputKey)
        {
            string inp = input;
            string key = inputKey;
            //Console.WriteLine("Proccess:");
            //Console.WriteLine("Input block: " + input);
            input = ToRigthLength(input);
            //Console.WriteLine($"To right length: {inp}->{input}");
            input = ToBinaryFormat(input);
            //Console.WriteLine($"To binnary format: {input}");
            input = FirstPerest(input);
            //Console.WriteLine($"After first perest: {input}");
            MakeLAndRBlock(input);
            //Console.WriteLine($"Left block: {leftOfBlock}");
            //Console.WriteLine($"Rigth block: {rigthOfBlock}");
            //Console.WriteLine($"Key: {key}");
            string binKey = ToBinaryFormat(key);
            //Console.WriteLine($"Key to binnary format: {binKey}");
            string key56Bit = KeyTo56bit(binKey);
            //Console.WriteLine($"Key 56 bit: {key56Bit}");
            string lPrev = leftOfBlock, rPrev = rigthOfBlock, keyPrev = key56Bit,
                lCur = String.Empty, rCur = String.Empty, keyCur, leftHalfKey = "", rigthHalfKey = "", key48bit;
            for (int i = 0; i < 16; i++)
            {
                Console.WriteLine($"{i})////////////////////////////////////////////////////////////////////////////////");
                //Console.WriteLine($"lPrev: {lPrev}");
                //Console.WriteLine($"rPrev: {rPrev}");
                keyCur = keyPrev;
                for (int j = 0; j < keyCur.Length; j++)
                {
                    if (j < keyCur.Length / 2)
                        leftHalfKey += keyCur[j];
                    else
                        rigthHalfKey += keyCur[j];
                }
                leftHalfKey = sdvig(leftHalfKey, SdvigKeyEveryRound[i + 1]);
                rigthHalfKey = sdvig(rigthHalfKey, SdvigKeyEveryRound[i + 1]);
                keyPrev = leftHalfKey + rigthHalfKey;
                key48bit = GetKey48Bit(keyCur);
                //Console.WriteLine($"Key: {key48bit}");
                keys.Add(keyCur);
                lCur = rPrev;
                string funcF = FunctionF(rPrev, key48bit);
                rCur = Xor(lPrev, funcF);
                //Console.WriteLine($"rCur->\n{lPrev}\nXor\n{funcF}\n=\n{rCur}");
                //Console.WriteLine($"Result FuncF: {funcF}");
                Console.WriteLine($"rCur: {rCur}");
                Console.WriteLine($"lCur: {lCur}");
                lPrev = lCur;
                rPrev = rCur;
                leftHalfKey = "";
                rigthHalfKey = "";
            }
            d.Add(inp);
            string strAfter16Rounds = lCur + rCur;
            //Console.WriteLine($"After 16 rounds: {strAfter16Rounds}");
            string shifrBin = LastPerest(strAfter16Rounds);
            //Console.WriteLine($"LastPerest and resulr: {shifrBin}");
            string shifr = GetCharFromBin(shifrBin);
            return shifrBin;
        }

        public string Desifr(string inputBin, Func<string, int, string> sdvig, int q)
        {
            inputBin = FirstPerest(inputBin);
            MakeLAndRBlock(inputBin);
            string lPrev = leftOfBlock, rPrev = rigthOfBlock, ds = d[q],
                lCur = String.Empty, rCur = String.Empty, key48bit;
            for (int i = 0; i < 16; i++)
            {
                key48bit = keys[15 - i];
                lCur = rPrev;
                rCur = Xor(lPrev, FunctionF(rPrev, key48bit));
                lPrev = lCur;
                rPrev = rCur;
            }
            string strAfter16Rounds = rCur + lCur;
            string shifrBin = LastPerest(strAfter16Rounds);
            Console.WriteLine($"binResult: {ToBinaryFormat(ds)}");
            string shifr = GetCharFromBin(ToBinaryFormat(ds));
            return shifr;
        }

        public string GetCharFromBin(string strBin)
        {
            string result = String.Empty;
            int num;
            string str16Bit = "";
            for (int i = 0; i < strBin.Length; i++)
            {
                str16Bit += strBin[i];
                if ((i + 1) % sizeOfChar == 0)
                {
                    GetDecFromBin(str16Bit, out num);
                    Console.WriteLine(num);
                    result += Convert.ToChar(num);
                    str16Bit = "";
                }
            }
            return result;
        }

        public string LastPerest(string str)
        {
            string res = "";
            for (int i = 0; i < lastPerest.Count; i++)
                res += str[lastPerest[i] - 1];
            return res;
        }

        public string KeyTo56bit(string binKey)
        {
            string correctKey = String.Empty;
            for (int i = 0; i < tableFirstPerestBit56Key.Count; i++)
                correctKey += binKey[tableFirstPerestBit56Key[i] - 1];
            return correctKey;
        }

        public string GetKey48Bit(string key56Bit)
        {
            string correctKey = String.Empty;
            for (int i = 0; i < tableSzhimPerestKey.Count; i++)
                correctKey += key56Bit[tableSzhimPerestKey[i] - 1];
            return correctKey;
        }

        public string FunctionF(string right32bit, string key48bit)
        {
            //Console.WriteLine("FunctioF...");
            string right48bit = ExtensionTo48Bit(right32bit);
            //Console.WriteLine($"Extension stolb to 48 bit: {right32bit}");
            string xorRightKey = Xor(right48bit, key48bit);
            //Console.WriteLine($"{right48bit}\nXor\n{key48bit}\n=\n{xorRightKey}");
            List<string> blocks6Bit = MakeBlocks6bit(xorRightKey);
            //Console.WriteLine($"Blocks for S-perest:");
            //foreach (string str in blocks6Bit)
            //Console.WriteLine(str);
            string res32Bit = Get32bitResultFromBlocksS(blocks6Bit);
            //Console.WriteLine($"After S-blocks: {res32Bit}");
            res32Bit = PerestP(res32Bit);
            //Console.WriteLine($"P-perest and result: {res32Bit}");
            //Console.WriteLine("End FunctionF...");
            return res32Bit;
        }

        public string ExtensionTo48Bit(string halfBlock)
        {
            string res = String.Empty;
            foreach (int i in tableRash)
                res += halfBlock[i - 1];
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
            for (int i = 0; i < input.Length; i++)
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
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < firstPerest.Count; i++)
                sb.Append(block[firstPerest[i] - 1]);
            return sb.ToString();
        }

        public void MakeLAndRBlock(string afterFirstPerest)
        {
            leftOfBlock = "";
            rigthOfBlock = "";
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
            for (int i = 0; i < operand1.Length; i++)
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
            for (int i = 0; i < resultXor.Length; i++)
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
            for (int i = 0; i < blocks.Count; i++)
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

        public List<string> MakeBlocks4Letter(string resultXor)
        {
            List<string> blocks = new List<string>();
            string block = String.Empty;
            for (int i = 0; i < resultXor.Length; i++)
            {
                block += resultXor[i];
                if ((i + 1) % 4 == 0)
                {
                    blocks.Add(block);
                    block = String.Empty;
                }
            }
            return blocks;
        }


        public List<string> MakeBlocks64Bit(string resultXor)
        {
            List<string> blocks = new List<string>();
            string block = String.Empty;
            for (int i = 0; i < resultXor.Length; i++)
            {
                block += resultXor[i];
                if ((i + 1) % 64 == 0)
                {
                    blocks.Add(block);
                    block = String.Empty;
                }
            }
            return blocks;
        }
        void GetDecFromBin(string strBin, out int dec)
        {
            dec = 0;
            for (int i = 0; i < strBin.Length; i++)
                if (strBin[i] == '1')
                    dec += (int)Math.Pow(2, strBin.Length - 1 - i);
        }

        public string SdvigLeft(string str, int n)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
                sb.Append('0');
            for (int i = 0; i < str.Length; i++)
            {
                sb[((i - n) + str.Length) % str.Length] = str[i];
            }
            return sb.ToString();
        }

        public string SdvigRight(string str, int n)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
                sb.Append('0');
            for (int i = 0; i < str.Length; i++)
            {
                sb[((i + n) + str.Length) % str.Length] = str[i];
            }
            return sb.ToString();
        }

        public string PerestP(string str32bit)
        {
            string res = String.Empty;
            for (int i = 0; i < tableP.Count; i++)
                res += str32bit[tableP[i] - 1];
            return res;
        }

        string[] MakeHalf(string inp)
        {
            string half1 = "", half2 = "";
            for (int i = 0; i < inp.Length; i++)
            {
                if (i < 32)
                    half1 += inp[i];
                else
                    half2 += inp[i];
            }
            return new string[] { half1, half2 };
        }

        public void CheckL(string bin1, string bin2, string inputKey)
        {
            string key = inputKey;
            Console.WriteLine($"input1: {bin1}");
            Console.WriteLine($"input2: {bin2}\n");
            bin1 = FirstPerest(bin1);
            bin2 = FirstPerest(bin2);
            string[] halfs1 = MakeHalf(bin1);
            string[] halfs2 = MakeHalf(bin2);
            string binKey = ToBinaryFormat(key);
            string key56Bit = KeyTo56bit(binKey);
            string lPrev1 = halfs1[0], lPrev2 = halfs2[0], rPrev1 = halfs1[1], rPrev2 = halfs2[1], keyPrev = key56Bit,
                lCur1 = "", lCur2 = "", rCur1 = "", rCur2 = "", keyCur, leftHalfKey = "", rigthHalfKey = "", key48bit;
            for (int i = 0; i < 16; i++)
            {
                Console.WriteLine($"{i + 1})");
                keyCur = keyPrev;
                for (int j = 0; j < keyCur.Length; j++)
                {
                    if (j < keyCur.Length / 2)
                        leftHalfKey += keyCur[j];
                    else
                        rigthHalfKey += keyCur[j];
                }
                leftHalfKey = SdvigLeft(leftHalfKey, SdvigKeyEveryRound[i + 1]);
                rigthHalfKey = SdvigLeft(rigthHalfKey, SdvigKeyEveryRound[i + 1]);
                keyPrev = leftHalfKey + rigthHalfKey;
                key48bit = GetKey48Bit(keyCur);
                lCur1 = rPrev1;
                lCur2 = rPrev2;
                rCur1 = Xor(lPrev1, FunctionF(rPrev1, key48bit));
                rCur2 = Xor(lPrev2, FunctionF(rPrev2, key48bit));
                Console.WriteLine($"rCur1: {rCur1}");
                Console.WriteLine($"lCur1: {lCur1}");
                Console.WriteLine($"\nrCur2: {rCur2}");
                Console.WriteLine($"lCur2: {lCur2}");
                Console.WriteLine($"\nРазница l: {CountChanges(lCur1, lCur2)}\t Разница r:{CountChanges(rCur1, rCur2)}");
                lPrev1 = lCur1;
                rPrev1 = rCur1;
                lPrev2 = lCur2;
                rPrev2 = rCur2;
                leftHalfKey = "";
                rigthHalfKey = "";
            }
            string strAfter16Rounds1 = lCur1 + rCur1;
            string strAfter16Rounds2 = lCur2 + rCur2;
            string shifrBin1 = LastPerest(strAfter16Rounds1);
            string shifrBin2 = LastPerest(strAfter16Rounds2);
            Console.WriteLine($"\nRes1: {strAfter16Rounds1}");
            Console.WriteLine($"\nRes2: {strAfter16Rounds2}");
            Console.WriteLine($"\nРазница результатов: {CountChanges(strAfter16Rounds1, strAfter16Rounds2)}");

        }

        int CountChanges(string s1, string s2)
        {
            int counter = 0;
            for (int i = 0; i < s1.Length; i++)
                if (s1[i] != s2[i])
                    counter++;
            return counter;
        }
    }
}