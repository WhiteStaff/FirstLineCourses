using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            long a = 0,b = 0,c = 0;
            for (int i = 0; i < 100; i++)
            {
                a = GetAllBytes();
                b = GetHash();
                c = GetOneByte();
                Console.WriteLine($"Все: {a} Хеш: {b} По одному: {c}");
            }
            Console.WriteLine($"получение всех байт: {a /100}");
            Console.WriteLine($"получение хеша: {b / 100}");
            Console.WriteLine($"получение 1 байт: {c / 100}");
        }

        private static long GetAllBytes()
        {
            var timer = new Stopwatch();
            timer.Start();

            var file1 = File.ReadAllBytes("data.xlsx");
            var file2 = File.ReadAllBytes("newdata.xlsx");

            if (file1.Length != file2.Length) return timer.ElapsedTicks;
            var x = !file1.Where((t, i) => t != file2[i]).Any();
            //Console.WriteLine(x);
            return timer.ElapsedTicks;
        }

        private static long GetHash()
        {
            var timer = new Stopwatch();
            timer.Start();
            byte[] x;
            byte[] x1;

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead("data.xlsx"))
                {
                    x = md5.ComputeHash(stream);
                }

                using (var stream = File.OpenRead("data.xlsx"))
                {
                    x1 = md5.ComputeHash(stream);
                }
            }

            //Console.WriteLine(x.Equals(x1));
            return timer.ElapsedTicks;
        }

        private static long GetOneByte()
        {
            var timer = new Stopwatch();
            timer.Start();
            bool diff = true;
            int from1 = 0;
            int from2 = 0;

            using (var stream1 = File.OpenRead("data.xlsx"))
            {
                using (var stream2 = File.OpenRead("newdata.xlsx"))
                {
                    while (diff && stream1.Position != stream1.Length && stream2.Position != stream2.Length)
                    {
                        from1 = stream1.ReadByte();
                        from2 = stream2.ReadByte();
                        diff = from1 == from2;
                    }
                }

                //Console.WriteLine(diff);
                return timer.ElapsedTicks;
            }
        }
    }
}