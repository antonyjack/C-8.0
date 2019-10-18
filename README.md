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
> 