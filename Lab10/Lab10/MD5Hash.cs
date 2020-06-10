using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Lab10
{
    class MD5Hash
    {
        public string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
}