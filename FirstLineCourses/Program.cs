using System;
using System.Collections.Generic;

namespace FirstLineCourses
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10000000; i++)
            {
                new Cat();
            }
        }
    }
    internal class Cat
    {
        private static int count;
        private int id;

        public Cat()
        {
            id = count++;
            Console.WriteLine($"создан новый {id}");
        }

        ~Cat()
        {
            new Cat();
            new Cat();
            Console.WriteLine($"Удалено {id}, создано {count}");
        }
    }
}
