using System;

class Program
{
    static void Main()
    {
        Random random = new Random();
        bool playAgain = true;
        while (playAgain)
        {
            int magicNumber = random.Next(1, 101); // Generate random number between 1 and 100
            int guess = 0;
            int numberOfGuesses = 0;

            Console.WriteLine("Guess the magic number between 1 and 100.");

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = Convert.ToInt32(Console.ReadLine());
                numberOfGuesses++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                    Console.WriteLine($"It took you {numberOfGuesses} guesses.");
                }
            }

            Console.Write("Do you want to play again (yes/no)? ");
            playAgain = Console.ReadLine().ToLower() == "yes";
        }
    }
}
