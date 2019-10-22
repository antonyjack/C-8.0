using System;

namespace StaticLocalFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Enter numbers to add:");
            
            System.Console.WriteLine("Enter number 1 : ");
            var number1 = Convert.ToInt32(Console.ReadLine());
            
            System.Console.WriteLine("Enter number 2 : ");
            var number2 = Convert.ToInt32(Console.ReadLine());

            Add(number1, number2);

            static void Add(int number1, int number2)
            {
                System.Console.WriteLine($"{number1} + {number2} = {number1 + number2}");
            }
        }
    }
}
