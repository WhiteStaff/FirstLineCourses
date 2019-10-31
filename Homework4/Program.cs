using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Homework4
{
    class Program
    {
        static void Main(string[] args)
        {
            Compare("хуй Cоси");
        }

        static void Compare(string a)
        {
            for(int i = 0; i < 5; i++) Console.WriteLine($"{a.ToLower()}");
        }
    }
}