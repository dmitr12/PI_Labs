using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6
{
    public class Generator
    {
        public IEnumerable<int> Lkg(int a, int c, int n, int countIterations)
        {
            int xPrev = 1, xCur;
            List<int> sequence = new List<int>();
            for(int i=0;i<countIterations;i++)
            {
                xCur = (a * xPrev + c) % n;
                sequence.Add(xCur);
                xPrev = xCur;
            }
            return sequence;
        }
    }
}
