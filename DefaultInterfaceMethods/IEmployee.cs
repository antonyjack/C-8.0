using System;

namespace DefaultInterfaceMethods
{
    public interface IEmployee
    {
        public string FirstName { get; }
        public string LastName { get; }
        //Default implementation
        public string FullName() => $"{FirstName} {LastName}";
    }
}