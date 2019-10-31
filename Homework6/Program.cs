using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;


namespace Homework6
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new HashSet<string>();
            for (int i = 0; i < 10; i++)
            {
                a.Add(Console.ReadLine());
            }

            foreach (var x in a)
            {
                Console.WriteLine(x);
            }

        }

    }
}