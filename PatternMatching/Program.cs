using System;

namespace PatternMatching
{
    class Program
    {
        static void Main(string[] args)
        {            
            Employee employee = new Employee("James", 3000000, "TL");            
            var (name, salary, designation) = employee;

            //Positional pattern
            System.Console.WriteLine(ToStringObject(employee));            
            //Switch pattern
            System.Console.WriteLine($"Name : {name}, Designation : {GetDesignationDescription(employee)}");
            //Property pattern
            System.Console.WriteLine($"Name : {name}, Tax : {GetTaxAmount(employee)}");
            //Property pattern
            System.Console.WriteLine($"Name : {name}, Bonus : {GetBonusAmount(employee)}");
            //Tuple pattern
            System.Console.WriteLine($"Result : {TuplePatternsOr(1,0)}");
        }

        //Switch pattern expression
        static string GetDesignationDescription(Employee emp) =>
            emp.Designation switch
            {
                "SE" => "Software Engineer",
                "SSE" => "Senior Software Engineer",
                "TL" => "Team Lead",
                "PM" => "Project Manager",
                _ => "Disignation not found" //discard pattern
            };
        
        static double GetTaxAmount(Employee emp) =>
            emp switch
            {
                { Salary: { } salary } when salary < 2500000 && salary > 1000000 => (salary * 10) / 100,
                { Salary: { } salary } when salary < 5000000 && salary > 2500000 => (salary * 20) / 100,
                { Salary: { } salary } when salary < 1000000 => 0,
                _ => 0
            };

        //Positional pattern expression

        static string ToStringObject(Employee employee) =>
            employee switch 
            {
                var (name, salary, designation) => $"Name : {name}, Designation : {designation}",                
                _ => "Employee not found"            
            };

        //Property pattern expression
        static double GetBonusAmount(Employee emp) => 
            emp switch
            {
                { Designation: "SE" } => 10000,
                { Designation: "SSE" } => 20000,
                { Designation: "TL" } => 25000,
                _ => 0
            };       

        //Tuple patterns
        static int TuplePatternsOr(int a, int b) =>
            (a, b) switch
            {
                (1, 1) => 1,
                (1, 0) => 1,
                (0, 1) => 1,
                (0, 0) => 0,
                _ => 0
            };
    }

    public class Employee 
    {
        public string Name { get; set; }
        public double Salary { get; set; }
        public string Designation { get; set; } 

        public Employee() {}

        public Employee(string name, double salary) =>
            (Name, Salary) = (name, salary);

        public Employee(string name, double salary, string designation) =>
            (Name, Salary, Designation) = (name, salary, designation);

        public void Deconstruct(out string name, out double salary, out string designation) =>
            (name, salary, designation) = (this.Name, this.Salary, this.Designation);
    }
}
