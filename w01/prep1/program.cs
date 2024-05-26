using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("What is your first name?");
        string firstName = Console.ReadLine();

        Console.WriteLine("What is your last name?");
        string lastName = Console.ReadLine();

        string result = $"Your name is {lastName}, {firstName} {lastName}.";
        Console.WriteLine(result);
    }
}
