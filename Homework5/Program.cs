using System;

namespace Homework5
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Round(1,1);
        }
        
    }

    struct Round
    {
        private int x;
        private int y;
        private int rad;
        private int tolsh;
        private string color;

        public Round(int x, int y)
        {
            this.x = x;
            this.y = y;
            rad = 1;
            tolsh = 1;
            color = "black";
        }
        

        
    }
    
}
