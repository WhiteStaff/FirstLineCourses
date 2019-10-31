using System;
using System.Collections.Generic;
using System.Text;

namespace RunningCatsTotalizator
{
    class Cat
    {
        public static int totalCatsInTheGame = 10;
        public int catNumber;
        public int catSpeed;

        public Cat()
        {
            var rnd = new Random();
            catSpeed = rnd.Next(1, 11);
        }

        public double CatTime(int distance) => (double)distance / catSpeed;

    }
}
