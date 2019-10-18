using System;

namespace DefaultInterfaceMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            IEmployee employee = new Employee("James", "Raj");

            Console.WriteLine(employee.FullName());
        }
    }
}
