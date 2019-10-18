using System;
using System.IO;

namespace UsingDeclaration
{
    class Program
    {
        static void Main(string[] args)
        {            
            //OldWayUsingBlock();
            NewWayUsingBlock();
        }

        public static void OldWayUsingBlock()
        {
            using(var file = new StreamReader("Versions.txt"))
            {
                var line = string.Empty;
                while((line = file.ReadLine()) != null)
                {
                    System.Console.WriteLine(line);
                }
                file.Close();                
            } // file disposed here

        }

        public static void NewWayUsingBlock()
        {
            using var file = new StreamReader("Versions.txt");
            var line = string.Empty;
            while((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
            }
            file.Close();
        }
    }
}
