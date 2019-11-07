using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork8
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "собачка;котик;123мишка;шишка;лента;123;кристал;полиморф;ксеноморф;шб;крапивка;игрушка;валенок;задача;123123123;красота";
            foreach (var item in s.Split(';'))
            {
                if (ulong.TryParse(item, out var q)) Console.WriteLine(item);
            }
        }

        //public static string AllByZero(int a)
        //{
        //    try
        //    {
        //        return (a/0).ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        return new StackTrace().ToString();
        //    }


        //}
        //public static int DivisionBy(int x, int y)
        //{
        //    while (y == 0)
        //    {
        //        Console.WriteLine("Делить на ноль НЕЛЬЗЯ!!! Пожалуйста введите другое число");
        //        int.TryParse(Console.ReadLine(), out y);
        //    }

        //    ;
        //    return x / y;
        //}
    }
}