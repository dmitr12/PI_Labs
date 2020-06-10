using System;
using System.Collections.Generic;
using System.Text;

namespace Lab11
{
    class EK
    {
        int a;
        int b;
        int p;
        int[] G;
        int d;
        int k;
        public List<int[]> listPoints;
        public static char[] alph = {'a', 'b', 'c', 'd', 'e', 'f', 'g',
                                        'h', 'i', 'j', 'k', 'l', 'm', 'n',
                                        'o', 'p', 'q', 'r', 's', 't', 'u',
                                        'v', 'w', 'x', 'y', 'z'};

        public EK(int a, int b, int p, int[] G, int d, int k)
        {
            this.a = a;
            this.b = b;
            this.p = p;
            this.G = G;
            this.d = d;
            this.k = k;
        }

        public string Check(string text)
        {
            int[] Q = Mult(G, d);
            listPoints = CreatePoints(516, 550);
            List<int[]> textPoints = new List<int[]>();
            for(int i=0;i<text.Length;i++)
            {
                int pos = 0;
                for (int j = 0; j < alph.Length; j++)
                    if (alph[j] == text[i])
                        pos = j;
                textPoints.Add(listPoints[pos]);
            }

            List<int[]> encodedTextPoints = Encrypt(textPoints, Q);
            return Decrypt(encodedTextPoints);            
        }

        List<int[]> Encrypt(List<int[]> textPoints, int[] Q)
        {
            List<int[]> encodedTextPoints = new List<int[]>();
            for (int i = 0; i < textPoints.Count; i++)
            {
                int[] C1 = Mult(G, k);
                int[] kQ = Mult(Q, k);
                int[] C2 = Addiction(textPoints[i], kQ);
                encodedTextPoints.Add(new int[] { C1[0], C1[1], C2[0], C2[1] });
            }
            foreach (int[] i in encodedTextPoints)
                Console.WriteLine("[" + i[0] + ", " + i[1] + "], [" + i[2] + ", " + i[3] + "] ");
            return encodedTextPoints;
        }

        string Decrypt(List<int[]> encodedTextPoints) 
        {
            string res = "";
            List<int[]> decodedTextPoints = new List<int[]>();
            for (int i = 0; i < encodedTextPoints.Count; i++)
            {
                int[] C1 = Mult(new int[] { encodedTextPoints[i][0], encodedTextPoints[i][1] }, d);
                int[] C1_minus = { C1[0], (-(C1[1]) + p) % p };
                int[] C2 = { encodedTextPoints[i][2], encodedTextPoints[i][3] };
                int[] P = Addiction(C2, C1_minus);
                int pos = 0;
                for (int j = 0; j < listPoints.Count; j++)
                    if (listPoints[j][0] == P[0] && listPoints[j][1] == P[1])
                        pos = j;
                res += alph[pos];
            }
            return res;
        }

        List<int[]> CreatePoints(int min, int max)
        {
            List<int[]> cyclicResidues = new List<int[]>();
            List<int[]> points = new List<int[]>();
            for(int i=0;i<p;i++)
                cyclicResidues.Add(new int[] { i, (int)(Math.Pow(i, 2)) % p });
            for(int i=min;i<max;i++)
            {
                int y = (int)(Math.Pow(i, 3)) + (a * i) + b;
                if (y < 0)
                    y += p;
                y = y % p;
                for (int j = 0; j < cyclicResidues.Count; j++)
                    if (cyclicResidues[j][1] == y)
                        points.Add(new int[] { i, cyclicResidues[j][0] });
            }
            return points;
        }

        public string ToBin(int x) => Convert.ToString(x, 2);

        public int[] Mult(int[] P, int z)
        {
            string bits = ToBin(z);
            List<int[]> arrP = new List<int[]>();
            arrP.Add(P);
            for (int i = 1; i < bits.Length; i++)
                arrP.Add(Addiction(arrP[i - 1], arrP[i - 1]));
            int counter = -1;
            int[] res = new int[2];
            for(int i=0;i<bits.Length;i++)
            {
                if(bits[bits.Length-1-i]=='1')
                {
                    res = arrP[i];
                    counter = i;
                    break;
                }
            }
            for (int i = counter + 1; i < bits.Length; i++)
                if (bits[bits.Length - i - 1] == '1')
                {
                    int[] tmp = Addiction(res, arrP[i]);
                    res = tmp;
                }
            return res;
        }

        public int[] Addiction(int[] P, int[] Q)
        {
            long lambda = 0;
            if(P[0]==Q[0] && P[1] == Q[1])
            {
                int x_tmp = (3 * (Convert.ToInt32(Math.Pow(P[0], 2))) + a) % p;
                if (x_tmp < 0)
                    x_tmp += p;
                int y_tmp = 2 * P[1];
                int eul = Euler(p);
                int tmp = 1;
                for (int i = 0; i < eul - 1; i++)
                {
                    tmp *= y_tmp;
                    tmp %= p;
                }
                y_tmp = tmp;
                lambda = ((x_tmp % p) * (y_tmp % p)) % p;
            }
            else
            {
                int x_tmp = (Q[1] - P[1]) % p;
                if (x_tmp < 0)
                    x_tmp += p;
                int y_tmp = (Q[0] - P[0]);
                int eul = Euler(p);
                int tmp = 1;
                for (int i = 0; i < eul - 1; i++)
                {
                    tmp *= y_tmp;
                    tmp %= p;
                }
                y_tmp = tmp;
                lambda = ((x_tmp % p) * (y_tmp % p)) % p;
            }
            int x = (Convert.ToInt32(Math.Pow(lambda, 2)) - P[0] - Q[0]) % p;
            if (x < 0)
                x += p;
            int y = (Convert.ToInt32(lambda) * (P[0] - x) - P[1]) % p;
            if (y < 0)
                y += p;
            return new int[] { x, y };
        }

        public int Euler(int n)
        {
            int r = n;
            int i = 2;
            while (Math.Pow(i, 2) <= n)
            {
                if (n % i == 0)
                {
                    while (n % i == 0)
                        n /= i;
                    r -= r / i;
                }
                else
                    i += 1;
            }
            if (n > 1)
                r -= r / n;
            return r;
        }
    }
}
