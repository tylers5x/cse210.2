using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num == 0)
            {
                break;
            }
            numbers.Add(num);
        }

        if (numbers.Count > 0)
        {
            int sum = numbers.Sum();
            double average = numbers.Average();
            int max = numbers.Max();

            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {max}");

            // Stretch challenges
            int smallestPositive = numbers.Where(n => n > 0).DefaultIfEmpty().Min();
            numbers.Sort();

            Console.WriteLine(smallestPositive > 0 ? $"The smallest positive number is: {smallestPositive}" : "No positive numbers entered.");
            Console.WriteLine("The sorted list is:");
            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }
        }
        else
        {
            Console.WriteLine("No numbers were entered.");
        }
    }
}
