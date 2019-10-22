using System;

namespace IndicesRanges
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = new string[]
            {
                "James", // [0] [^7]
                "Raj", // [1] [^6]
                "Antony", // [2] [^5]
                "Samy", // [3] [^4]
                "Joseph", // [4] [^3]
                "Andrews", // [5] [^2]
                "React", // [6] [^1]
            };

            System.Console.WriteLine(string.Join(",", names[1..4])); // return Raj, Antony, Samy
            System.Console.WriteLine(string.Join(",", names[^3..^1])); // return Joseph, Andrews
            
            System.Console.WriteLine(string.Join(",", names[..])); // return all the element in the array
            System.Console.WriteLine(string.Join(",", names[..4])); // return James, Raj, Antony, Samy
            System.Console.WriteLine(string.Join(",", names[4..])); //return Joseph, Andrews, React

            System.Console.WriteLine(string.Join(",", names[..^4])); // return James, Raj, Antony
            System.Console.WriteLine(string.Join(",", names[^4..])); // return Samy, Joseph, Andrews, React

            Range range = 2..5;
            System.Console.WriteLine(string.Join(",", names[range]));

            //Fetch 2nd last element from the array
            System.Console.WriteLine(names[^2]);
        }
    }
}
