using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_7_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Число 1: ");
            string num1 = Console.ReadLine();
            Console.WriteLine("Число 2: ");
            string num2 = Console.ReadLine();
            RationalNumbers number = RationalNumbers.Parse(num1);
            RationalNumbers number1 = RationalNumbers.Parse(num2);
            RationalNumbers err;
            Console.WriteLine(RationalNumbers.TryParse("123ewf", out err));
            Console.WriteLine("--------------");
            Console.WriteLine("int: " + ((int)number + (int)number1));
            Console.WriteLine("double: " + ((double)number / (double)number1));
            Console.WriteLine("decimal: " + ((decimal)number * (decimal)number1));
            Console.WriteLine("bool: " + Convert.ToBoolean(number));
            Console.WriteLine("--------------");
            Console.WriteLine("==?" + (number == number1));
            Console.WriteLine(">?" + (number > number1));
            Console.WriteLine("<?" + (number < number1));
            Console.WriteLine(number.CompareTo(number1));
            Console.WriteLine(number.Equals(number1));
            Console.WriteLine("--------------");
            Console.WriteLine(number.ToString("F"));
            Console.WriteLine(number.ToString("I"));
            Console.WriteLine(number.ToString("IF"));
            Console.WriteLine(number.ToString("D"));
            Console.WriteLine("--------------");
            Console.WriteLine(number1.GetHashCode());
            Console.WriteLine(number.GetHashCode());
            Console.ReadLine();
        }
    }
}
