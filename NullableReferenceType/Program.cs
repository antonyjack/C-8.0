﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
// using JetBrains.Annotations;

namespace NullableReferenceType
{
    //#nullable enable
    class Program
    {        
        static void Main(string[] args)
        {
            Customer customer = null;  // warning CS8600: Converting null literal or possible null value to non-nullable type.    
            Customer customer1 = new Customer();
            //customer.Display(); // warning CS8602: Dereference of a possibly null reference.
            customer?.Display();
            customer1.Display();
            //NotNullMethod(customer); // warning CS8600: Converting null literal or possible null value to non-nullable type.
            NotNullMethod(customer1);
            CanBeNullMethod(customer);
        }
                
        [return: NotNull]
        static string NotNullMethod([DisallowNull]Customer customer)
        {            
            return customer.ToString();
        }
                
        [return: MaybeNull] //warning CS8603: Possible null reference return.
        static string CanBeNullMethod([AllowNull]Customer customer)
        {
            return customer?.ToString();
        }    
    }
    //#nullable restore
    
    //#nullable enable
    class Customer
    {

        [AllowNull]
        public string Name { get; set; } = default!;
        public int Age { get; set; } = default;

        public Customer() {}
        public Customer(string name, int age)
            => (Name, Age) = (name, age);

        public void Display()
        {
            System.Console.WriteLine($"Welcome {Name ?? "None"}, Age : {Age}");
        }

        public override string ToString() => $"Name : {Name}";
    }
    //#nullable restore
}
