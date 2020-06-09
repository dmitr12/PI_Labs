using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6
{
    public class RC4
    {
        List<int> key;
        int[] S;
        int[] K;
        int n,x,y;
        public RC4(int n, List<int>key)
        {
            this.key = key;
            this.n = n;
            S = new int[Convert.ToInt32(Math.Pow(2,n))];
            K = new int[Convert.ToInt32(Math.Pow(2, n))];
            InitSBlock();
        }

        private void InitSBlock()
        {
            int counter = 0;
            for (int i = 0; i < S.Length; i++)
            {
                S[i] = i;
                K[i] = key[counter++];
                if (counter == key.Count)
                    counter = 0;
            }
            int j = 0;
            for (int i = 0; i < S.Length; i++)
            {
                j = (j + S[i] + K[i]) % S.Length;
                Swap(ref S[i], ref S[j]);
            }
        }

        public List<string> Encode(List<string> blocks)
        {
            List<string> encryptList = new List<string>();
            for (int i = 0; i < blocks.Count; i++)
                encryptList.Add(Xor(blocks[i], GenerateFromKey()));
            return encryptList;
        }

        public List<string> Decode(List<string> blocks)
        {
            return new RC4(n, key).Encode(blocks);
        }

        string GenerateFromKey()
        {
            x = (x + 1) % (n * n);
            y = (y + S[x]) % (n * n);
            Swap(ref S[x], ref S[y]);
            return ToBinN(S[(S[x] + S[y]) % S.Length]);
        }

        public string StrToBin(string str)
        {
            string result = String.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                string bin = Convert.ToString(str[i], 2);
                while (bin.Length < 16)
                    bin = "0" + bin;
                result += bin;
            }
            return result;
        }
        public List<string> GetNBinBlocks(string txt)
        {
            List<string> blocks = new List<string>();
            int counter = txt.Length %n== 0 ? txt.Length / n : (txt.Length/n) + 1;
            for(int i = 0; i < counter; i++)
            {
                if (i + 1 == counter)
                    blocks.Add(txt.Substring(i * n));
                else
                    blocks.Add(txt.Substring(i * n, n));
            }
            return blocks;
        }

        public string GetStrFromBin(List<string> binList)
        {
            string res = "";
            string str = "";
            foreach (string s in binList)
                str += s;
            int counter = str.Length % 16 == 0 ? str.Length / 16 : (str.Length / 16) + 1;
            List<string> bl = new List<string>();
            for(int i=0;i<counter;i++)
            {
                if (i + 1 == counter)
                {
                    string iter = str.Substring(i * 16);
                    while (iter.Length != 16)
                        iter = "0" + iter;
                    bl.Add(iter);
                }
                else
                    bl.Add(str.Substring(i * 16, 16));
            }
            foreach (string sq in bl)
                res += (char)GetDecFromBin(sq);
            return res;
        }

        string ToBinN(int value)
        {
            string bin = Convert.ToString(value, 2);
            while (bin.Length != n)
                bin = "0" + bin;
            return bin;
        }

        public string GetTrueText(List<string> decodeResult)
        {
            string decodeText = "";
            decodeResult[decodeResult.Count - 1] =
                decodeResult[decodeResult.Count - 1].Remove(0, (decodeResult.Count*n) % 16);
            string res = "";
            foreach (string str in decodeResult)
                res += str;
            foreach (string s in Get16Blocks(res))
                decodeText += (char)GetDecFromBin(s);
            return decodeText;
        }

        List<string> Get16Blocks(string str)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < str.Length / 16; i++)
                list.Add(str.Substring(i * 16, 16));
            return list;
        }

        int GetDecFromBin(string strBin)
        {
            int dec = 0;
            for (int i = 0; i < strBin.Length; i++)
                if (strBin[i] == '1')
                    dec += (int)Math.Pow(2, strBin.Length - 1 - i);
            return dec;
        }

        private void Swap(ref int a, ref int b)
        {
            int t = a;
            a = b;
            b = t;
        }

        string Xor(string operand1, string operand2)
        {
            string res = String.Empty;
            if (operand1.Length < operand2.Length)
                while (operand1.Length != operand2.Length)
                    operand1 = "0" + operand1;
            else
                while (operand2.Length != operand1.Length)
                    operand2 = "0" + operand2;
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
    }
}
