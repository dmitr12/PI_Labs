using System;
using System.Collections.Generic;
using System.Text;

namespace Lab7
{
    public static class Base64
    {
        public static List<char> Table = new List<char> {
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O',
            'P','Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d',
            'e','f','g','h','i','j','k','l','m','n','o','p','q','r','s',
            't','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
            '8','9','+','/','='
        };
    }
}