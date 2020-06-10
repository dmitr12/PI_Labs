using System;
using System.Collections.Generic;
using System.Text;

namespace Lab9
{
    class MD5
    {
        uint A, B, C, D;
        List<uint> T;

        public MD5()
        {
            T = new List<uint>();
            T.Add(0);
        }

        public string GetHash(string msg)
        {
            string stream = "";
            string bt = "";
            foreach (int c in msg)
            {
                bt = Convert.ToString(c, 2);
                while (bt.Length != 8)
                    bt = '0' + bt;
                stream += bt;
            }
            //Step1
            stream = StreamAlignment(stream);
            //Step2
            stream = AddLengthMsg(stream, msg.Length * 8);
            //Step3
            BufferInit();
            //Step4
            Step4(stream);
            //Step5
            string hash = RowMdToHex(A) + RowMdToHex(B) + RowMdToHex(C) + RowMdToHex(D);
            return hash;
        }

        public string StreamAlignment(string bitStream)
        {
            bitStream += '1';
            while (bitStream.Length % 512 != 448)
                bitStream += '0';
            return bitStream;
        }

        public string AddLengthMsg(string streamAfterAlignment, int length)
        {
            string add = Convert.ToString(length, 2);
            while (add.Length != 64)
                add = '0' + add;
            streamAfterAlignment += add;
            return streamAfterAlignment;
        }

        public void BufferInit()
        {
            A = 0x67452301;
            B = 0xEFCDAB89;
            C = 0x98BADCFE;
            D = 0x10325476;
        }

        public void Step4(string stream)
        {
            for (int i = 1; i <= 64; i++)
                T.Add((uint)(Math.Pow(2, 32) * Math.Abs(Math.Sin(i + 1))));
            uint[] blocks32Bit = GetBlocks32Bit(stream);
            MakeRounds(blocks32Bit);
        }

        public uint[] GetBlocks32Bit(string blockBit)
        {
            int len = blockBit.Length;
            uint[] res = new uint[32 * len];
            for (int i = 0; i < 32 * (len / 512); i++)
            {
                res[i] = GetDecFromString(blockBit.Substring(16 * i, 16));
            }
            return res;
        }

        public void MakeRounds(uint[] buf)
        {
            for (int n = 0; n < buf.Length; n += 16)
            {
                uint AA = A;
                uint BB = B;
                uint CC = C;
                uint DD = D;

                //Step1
                A = RoundStep1(A, B, C, D, n + 0, 7, 1, buf); D = RoundStep1(D, A, B, C, n + 1, 12, 2, buf);
                C = RoundStep1(C, D, A, B, n + 2, 17, 3, buf); B = RoundStep1(B, C, D, A, n + 3, 22, 4, buf);
                A = RoundStep1(A, B, C, D, n + 4, 7, 5, buf); D = RoundStep1(D, A, B, C, n + 5, 12, 6, buf);
                C = RoundStep1(C, D, A, B, n + 6, 17, 7, buf); B = RoundStep1(B, C, D, A, n + 7, 22, 8, buf);
                A = RoundStep1(A, B, C, D, n + 8, 7, 9, buf); D = RoundStep1(D, A, B, C, 9, n + 12, 10, buf);
                C = RoundStep1(C, D, A, B, n + 10, 17, 11, buf); B = RoundStep1(B, C, D, A, n + 11, 22, 22, buf);
                A = RoundStep1(A, B, C, D, n + 12, 7, 13, buf); D = RoundStep1(D, A, B, C, n + 13, 12, 14, buf);
                C = RoundStep1(C, D, A, B, n + 14, 17, 15, buf); B = RoundStep1(B, C, D, A, n + 15, 22, 16, buf);
                //Step2
                A = RoundStep2(A, B, C, D, n + 1, 5, 17, buf); D = RoundStep2(D, A, B, C, n + 6, 9, 18, buf);
                C = RoundStep2(C, D, A, B, n + 11, 14, 19, buf); B = RoundStep2(B, C, D, A, n + 0, 20, 20, buf);
                A = RoundStep2(A, B, C, D, n + 5, 5, 21, buf); D = RoundStep2(D, A, B, C, n + 10, 9, 22, buf);
                C = RoundStep2(C, D, A, B, n + 15, 14, 23, buf); B = RoundStep2(B, C, D, A, n + 4, 20, 24, buf);
                A = RoundStep2(A, B, C, D, n + 9, 5, 25, buf); D = RoundStep2(D, A, B, C, n + 14, 9, 26, buf);
                C = RoundStep2(C, D, A, B, n + 3, 14, 27, buf); B = RoundStep2(B, C, D, A, n + 8, 20, 28, buf);
                A = RoundStep2(A, B, C, D, n + 13, 5, 29, buf); D = RoundStep2(D, A, B, C, n + 2, 9, 30, buf);
                C = RoundStep2(C, D, A, B, n + 7, 14, 31, buf); B = RoundStep2(B, C, D, A, n + 12, 20, 32, buf);
                //Step3
                A = RoundStep3(A, B, C, D, n + 5, 4, 33, buf); D = RoundStep3(D, A, B, C, n + 8, 11, 34, buf);
                C = RoundStep3(C, D, A, B, n + 11, 16, 35, buf); B = RoundStep3(B, C, D, A, n + 14, 23, 36, buf);
                A = RoundStep3(A, B, C, D, n + 1, 4, 37, buf); D = RoundStep3(D, A, B, C, n + 4, 11, 38, buf);
                C = RoundStep3(C, D, A, B, n + 7, 16, 39, buf); B = RoundStep3(B, C, D, A, n + 10, 23, 40, buf);
                A = RoundStep3(A, B, C, D, n + 13, 4, 41, buf); D = RoundStep3(D, A, B, C, n + 0, 11, 42, buf);
                C = RoundStep3(C, D, A, B, n + 3, 16, 43, buf); B = RoundStep3(B, C, D, A, n + 6, 23, 44, buf);
                A = RoundStep3(A, B, C, D, n + 9, 4, 45, buf); D = RoundStep3(D, A, B, C, n + 12, 11, 46, buf);
                C = RoundStep3(C, D, A, B, n + 15, 16, 47, buf); B = RoundStep3(B, C, D, A, n + 2, 23, 48, buf);
                //Step4
                A = RoundStep4(A, B, C, D, n + 0, 6, 49, buf); D = RoundStep4(D, A, B, C, n + 7, 10, 50, buf);
                C = RoundStep4(C, D, A, B, n + 14, 15, 51, buf); B = RoundStep4(B, C, D, A, n + 5, 21, 52, buf);
                A = RoundStep4(A, B, C, D, n + 12, 6, 53, buf); D = RoundStep4(D, A, B, C, n + 3, 10, 54, buf);
                C = RoundStep4(C, D, A, B, n + 10, 15, 55, buf); B = RoundStep4(B, C, D, A, n + 1, 21, 56, buf);
                A = RoundStep4(A, B, C, D, n + 8, 6, 57, buf); D = RoundStep4(D, A, B, C, n + 15, 10, 58, buf);
                C = RoundStep4(C, D, A, B, n + 6, 15, 59, buf); B = RoundStep4(B, C, D, A, n + 13, 21, 60, buf);
                A = RoundStep4(A, B, C, D, n + 4, 6, 61, buf); D = RoundStep4(D, A, B, C, n + 11, 10, 62, buf);
                C = RoundStep4(C, D, A, B, n + 2, 15, 63, buf); B = RoundStep4(B, C, D, A, n + 9, 21, 64, buf);

                A += AA;
                B += BB;
                C += CC;
                D += DD;
            }
        }

        public uint RotateLeft(uint x, int n) => ((x << n) | (x >> (32 - n)));

        public uint RoundStep1(uint a, uint b, uint c, uint d, int k, int s, int i, uint[] buf)
            => (b + RotateLeft(a + FuncF(b, c, d) + buf[k] + T[i], s));
        public uint RoundStep2(uint a, uint b, uint c, uint d, int k, int s, int i, uint[] buf)
           => (b + RotateLeft(a + FuncG(b, c, d) + buf[k] + T[i], s));
        public uint RoundStep3(uint a, uint b, uint c, uint d, int k, int s, int i, uint[] buf)
           => (b + RotateLeft(a + FuncH(b, c, d) + buf[k] + T[i], s));
        public uint RoundStep4(uint a, uint b, uint c, uint d, int k, int s, int i, uint[] buf)
        => (b + RotateLeft(a + FuncI(b, c, d) + buf[k] + T[i], s));

        public uint FuncF(uint X, uint Y, uint Z) => ((X & Y) | (~X & Z));
        public uint FuncG(uint X, uint Y, uint Z) => ((X & Z) | (~Z & Y));
        public uint FuncH(uint X, uint Y, uint Z) => (X ^ Y ^ Z);
        public uint FuncI(uint X, uint Y, uint Z) => (Y ^ (~Z | X));

        uint GetDecFromString(string str)
        {
            uint res = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[str.Length - 1 - i] == '1')
                    res += (uint)Math.Pow(2, i);
            }
            return res;
        }

        public string RowMdToHex(uint v) => Convert.ToString(v, 16);
    }
}