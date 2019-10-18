using System;

namespace ReadonlyMembers
{
    class Program
    {
        static void Main(string[] args)
        {            
            Employee emp = new Employee("James", "Raj");                        
            var Fullname = emp.ToString();
            Console.WriteLine(Fullname);
        }
    }


    public struct Employee
    {
        public Employee(string first, string last) 
        {
            this.FirstName = first;
            this.LastName = last;
        }

        public readonly string FirstName { get; }

        public readonly string LastName { get; } 

        /* 
            If full name is not readonly property 
            it throws warning like non readonly members "property name" from readonly member result 
        */
        //public string FullName => $"{FirstName} {LastName}"; 
        public readonly string FullName => $"{FirstName} {LastName}";        


        public readonly override string ToString() => $"Name : {FullName}";
    }
}
