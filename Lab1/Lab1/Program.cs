using System;
using System.Collections.Generic;
using static System.Console;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Explorer exp = new Explorer();
            Router router = new Router(exp);
            router.Start();
        }
    }
}
