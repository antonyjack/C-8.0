using System;

namespace DefaultInterfaceMethods
{
    public class Employee : IEmployee
    {
        public Employee(string first, string last) 
            => (this.FirstName, this.LastName) = (first, last);

        public string FirstName { get; }

        public string LastName { get; }

        // Overload default implementation
        public string FullName() => $"Mr. {FirstName} {LastName}";                
    }
}