// Base class
abstract class MindfulnessActivity
{
    protected int duration;
    protected string description;

    public void Start()
    {
        Console.Clear();
        Console.WriteLine($"Starting {GetType().Name}...");
        Console.WriteLine(description);
        Console.Write("Enter duration in seconds: ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        ShowSpinner(3);
        RunActivity();
        End();
    }

    protected abstract void RunActivity();

    protected void End()
    {
        Console.WriteLine("Good job!");
        Console.WriteLine($"You have completed the {GetType().Name} for {duration} seconds.");
        ShowSpinner(3);
    }

    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

// Derived class for Breathing Activity
class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity()
    {
        description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    protected override void RunActivity()
    {
        int halfDuration = duration / 2;
        for (int i = 0; i < halfDuration; i++)
        {
            Console.WriteLine("Breathe in...");
            ShowCountdown(4);
            Console.WriteLine("Breathe out...");
            ShowCountdown(4);
        }
    }

    private void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

// Derived class for Reflection Activity
class ReflectionActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
    }

    protected override void RunActivity()
    {
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine(prompt);
        ShowSpinner(5);

        int elapsedTime = 0;
        while (elapsedTime < duration)
        {
            string question = questions[rand.Next(questions.Count)];
            Console.WriteLine(question);
            ShowSpinner(5);
            elapsedTime += 5;
        }
    }
}
