# C# 8.0 Features

## Readonly members

> You can apply `readonly` modified any member of the `struct`. It idicates that the member does not modified state.

```csharp
public struct Employee
{
    public Employee(string first, string last) 
    {
        this.FirstName = first;
        this.LastName = last;
    }

    public readonly string FirstName { get; }

    public readonly string LastName { get; } 

    public string FullName => $"{FirstName} {LastName}";     

    public override string ToString() => $"Name : {FullName}";
}
```

> The above example `ToString()` doesn't modified state. You should idicate that by adding the `readonly` modified to the declaration of `ToString()`.

```csharp
public struct Employee
{
    public Employee(string first, string last) 
    {
        this.FirstName = first;
        this.LastName = last;
    }

    public readonly string FirstName { get; }

    public readonly string LastName { get; } 

    public string FullName => $"{FirstName} {LastName}";     

    public readonly override string ToString() => $"Name : {FullName}";
}
```

> If you compile the program, it throws warning like `non readonly members "FullName.get" from readonly member result`

> Which means `FullName` does not modified state. So the compiled warns you `readonly` modified necessary on read only property. 

> If you adding `readonly` modifier in `FullName` property, the warning does not shown.

```csharp
public struct Employee
{
    public Employee(string first, string last) 
    {
        this.FirstName = first;
        this.LastName = last;
    }

    public readonly string FirstName { get; }

    public readonly string LastName { get; } 

    public readonly string FullName => $"{FirstName} {LastName}";     

    public readonly override string ToString() => $"Name : {FullName}";
}
```

## Default Interface Methods

> Today, once you publish an interface it’s game over: you can’t add members to it without breaking all the existing implementers of it.

> In C# 8.0 we let you provide a body for an interface member. Thus, if somebody doesn’t implement that member (perhaps because it wasn’t there yet when they wrote the code), they will just get the default implementation instead.

```csharp
public interface IEmployee
{
    public string FirstName { get; }
    public string LastName { get; }
    //Default implementation
    public string FullName() => $"{FirstName} {LastName}";
}

//Interface implemenation
public class Employee : IEmployee
{
    public Employee(string first, string last) 
        => (this.FirstName, this.LastName) = (first, last);

    public string FirstName { get; }

    public string LastName { get; }             
}

//Get default implementation 
IEmployee employee = new Employee("James", "Raj");
```

> You can override the default implementation. 

```csharp
public class Employee : IEmployee
{
    public Employee(string first, string last) 
        => (this.FirstName, this.LastName) = (first, last);

    public string FirstName { get; }

    public string LastName { get; }

    // Overload default implementation
    public string FullName() => $"Mr. {FirstName} {LastName}";                
}
```

## More patterns in more places

- Switch expression
- Property pattern
- Tuple pattern
- Positional pattern
- Discard pattern

### Switch expressions

*Previous version*
```csharp
//Switch pattern expression
static string GetDesignationDescription(Employee emp) 
{
    switch(emp.Designation)
    {
        case "SE":
            return "Software Engineer";       
        case "SSE":
            return "Senior Software Engineer";
        case "TL":
            return "Team Lead";
        case "PM":
            return "Project Manager";
        default:
            "Disignation not found";
    }
}    
```

> The above switch statement is converted as expression.

```csharp
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
```

- The `switch` keyword is “infix” between the tested value and the `{...}` list of cases. That makes it more compositional with other expressions, and also easier to tell apart visually from a switch statement.
- The `case` keyword and the `:` have been replaced with a lambda arrow `=>` for brevity.
- `default` has been replaced with the `_` discard pattern for brevity.
- The bodies are expressions! The result of the selected body becomes the result of the switch expression

### Property patterns
> Property pattern by adding {...}‘s containing nested patterns to apply to the value’s accessible properties or fields. 

```csharp
static double GetTaxAmount(Employee emp) =>
    emp switch
    {
        { Salary: { } salary } when salary < 2500000 && salary > 1000000 => (salary * 10) / 100,
        { Salary: { } salary } when salary < 5000000 && salary > 2500000 => (salary * 20) / 100,
        { Salary: { } salary } when salary < 1000000 => 0,
        _ => 0
    };
```

### Positional patterns
> If the matched type is a tuple type or has a deconstructor, we can use positional patterns as a compact way of applying recursive patterns without having to name properties.

> The following `Employee` class having `Deconstruct` method. Deconstructors allowed a value to be deconstructed on assignment. So that you could write :

```csharp
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

Employee employee = new Employee("James", 3000000, "TL");            
var (name, salary, designation) = employee;
```

```csharp
//Positional pattern expression

static string ToStringObject(Employee employee) =>
    employee switch 
    {
        var (name, salary, designation) => $"Name : {name}, Designation : {designation}",   
        _ => "Employee not found"            
    };
```

## Tuple patterns
> Tuple patterns are great for testing multiple pieces of input at the same time. Here is a simple implementation of a state machine:

```csharp
static int TuplePatternsOr(int a, int b) =>
    (a, b) switch
    {
        (1, 1) => 1,
        (1, 0) => 1,
        (0, 1) => 1,
        (0, 0) => 0,
        _ => 0
    };    
```

## Using Declaration
> A `using declaration` is a variable declaration preceded by the `using` keyword. It tells the compiler that the variable being declared should be disposed at the end of the enclosing scope.

```csharp
 using var file = new StreamReader("Versions.txt");
var line = string.Empty;
while((line = file.ReadLine()) != null)
{
    System.Console.WriteLine(line);
}
file.Close();
```

## Static Local Functions
> You can now add the `static` modifier to local functions to ensure that local function doesn't capture (reference) any variables from the enclosing scope. 

```csharp
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
```

## Asynchronous streams
> we can create and consume streams asynchronously. A method that returns an asynchronous stream has three properties:
- It's declared with the `async` modifier.
- It returns an `IAsyncEnumerable<T>`.
- The method contains yield return statements to return successive elements in the asynchronous stream.

```csharp
await foreach (var item in GenerateStingsAsync())
{
    System.Console.WriteLine(item);
}

static async IAsyncEnumerable<string> GenerateStingsAsync()
{
    string[] names = new [] {"James", "Raj", "Andrews", "Arun", "Aravinda", "Viki", "Mathi"};
    foreach (var item in names)
    {
        await Task.Delay(5);
        yield return item;
    }
}
```

## Indices and ranges

> This features provide syntex for accessing the single element or range from the seqence.
- `System.Index` represents the `Index`
- The index from end operator `^`, which specifies that an index is relative to the end of the sequence.
- `System.Range` represents a sub range of a sequence.
- The range operator `..`, which specifies the start and end of a range as its operands.

```csharp
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
```

## Null-coalescing assignment
> null-coalescing assignment operator `??=` used to assign the value of its right-hand operand to its left-hand operand only if the left-hand operand evaluates to `null`.

> Previous C# versions we can evaluates `null` using following ways:
```csharp
string Name = null;

//Before C# 8.0 
if(string.IsNullOrEmpty(Name))
    Name = "James";

Name = Name ?? "James";

System.Console.WriteLine($"Before C# 8.0 => Name = Name ?? \"James\" : Result = {Name}");
```

> With the help of null-coalescing assignment operator `??=`, we can simplify the above code.

```csharp
string Name = null;
//After C# 8.0
Name ??= "James";

System.Console.WriteLine($"After C# 8.0 => Name ??= \"James\" : Result = {Name}");
```

## Enhancement of interpolated verbatim strings
> Order of the `$` and `@` tokens in interpolated verbatim strings can be any: both `$@"..."` and `@$"..."` are valid interpolated verbatim strings. In earlier C# versions, the `$` token must appear before the `@` token.

```csharp
//Before C# 8.0 Interpolation
string PathBuild = $@"c:\test\{Name}";

System.Console.WriteLine($"Before C# 8.0 => {PathBuild}");

PathBuild = null;

//After C# 8.0 Interpolation
PathBuild = @$"c:\test\{Name}";

System.Console.WriteLine($"Before C# 8.0 => {PathBuild}");
```

## Non-Nullable reference types

> For nonnullable reference types, the compiler uses flow analysis to ensure that local variables are initialized to a non-null value when declared. Fields must be initialized during construction. The compiler generates a warning if the variable is not set by a call to any of the available constructors or by an initializer. Furthermore, nonnullable reference types can't be assigned a value that could be null.

#### Enable Non-Nullable reference types feature:
> To enable non-nullable reference type feature in your project level. Need to add the following lines in your `.csproj` file.

```xml
<PropertyGroup>
    <Nullable>enable</Nullable>
</PropertyGroup>
```

> To enable the non-nullable reference type feature only in class level in your project. 

```csharp
#nullable enable
public class Customer
{        
}
#nullable restore

```csharp
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
```