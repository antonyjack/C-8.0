using System;

namespace Null_coalescing_assignment
{
    class Program
    {
        static void Main(string[] args)
        {            
            string Name = null;

            //Before C# 8.0 
            if(string.IsNullOrEmpty(Name))
                Name = "James";

            Name = Name ?? "James";

            System.Console.WriteLine($"Before C# 8.0 => Name = Name ?? \"James\" : Result = {Name}");

            Name = null;
            //After C# 8.0
            Name ??= "James";

            System.Console.WriteLine($"After C# 8.0 => Name ??= \"James\" : Result = {Name}");

            //Before C# 8.0 Interpolation
            string PathBuild = $@"c:\test\{Name}";

            System.Console.WriteLine($"Before C# 8.0 => {PathBuild}");

            PathBuild = null;

            //After C# 8.0 Interpolation
            PathBuild = @$"c:\test\{Name}";

            System.Console.WriteLine($"Before C# 8.0 => {PathBuild}");

        }
    }
}
