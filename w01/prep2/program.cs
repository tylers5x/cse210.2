using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter your grade percentage: ");
        int percentage = Convert.ToInt32(Console.ReadLine());

        string letter = "";
        string sign = "";

        // Determine the letter grade
        if (percentage >= 90)
        {
            letter = "A";
        }
        else if (percentage >= 80)
        {
            letter = "B";
        }
        else if (percentage >= 70)
        {
            letter = "C";
        }
        else if (percentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Determine the sign for grades except F
        if (letter != "F")
        {
            int lastDigit = percentage % 10;
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }

        // Special case for A+ and F grades
        if (letter == "A" && sign == "+")
        {
            sign = "";  // No A+ grade
        }

        // Output the letter grade with sign
        Console.WriteLine($"Your grade is: {letter}{sign}");

        // Determine if the student passed or failed
        if (percentage >= 70)
        {
            Console.WriteLine("Congratulations, you passed the class!");
        }
        else
        {
            Console.WriteLine("Unfortunately, you did not pass. Better luck next time!");
        }
    }
}
