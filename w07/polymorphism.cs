using System;
using System.Collections.Generic;

class Activity
{
    public DateTime Date { get; private set; }
    public int Length { get; private set; } // Length in minutes

    public Activity(DateTime date, int length)
    {
        Date = date;
        Length = length;
    }

    public virtual double GetDistance()
    {
        return 0;
    }

    public virtual double GetSpeed()
    {
        return 0;
    }

    public virtual double GetPace()
    {
        return 0;
    }

    public virtual string GetSummary()
    {
        return $"{Date.ToShortDateString()} Activity ({Length} min)";
    }
}

class Running : Activity
{
    public double Distance { get; private set; } // Distance in miles

    public Running(DateTime date, int length, double distance) : base(date, length)
    {
        Distance = distance;
    }

    public override double GetDistance()
    {
        return Distance;
    }

    public override double GetSpeed()
    {
        return (Distance / Length) * 60;
    }

    public override double GetPace()
    {
        return Length / Distance;
    }

    public override string GetSummary()
    {
        return $"{Date.ToShortDateString()} Running ({Length} min) - Distance: {Distance} miles, Speed: {GetSpeed():0.0} mph, Pace: {GetPace():0.0} min per mile";
    }
}

class Cycling : Activity
{
    public double Speed { get; private set; } // Speed in mph

    public Cycling(DateTime date, int length, double speed) : base(date, length)
    {
        Speed = speed;
    }

    public override double GetDistance()
    {
        return (Speed * Length) / 60;
    }

    public override double GetSpeed()
    {
        return Speed;
    }

    public override double GetPace()
    {
        return 60 / Speed;
    }

    public override string GetSummary()
    {
        return $"{Date.ToShortDateString()} Cycling ({Length} min) - Speed: {Speed:0.0} mph, Distance: {GetDistance():0.0} miles, Pace: {GetPace():0.0} min per mile";
    }
}

class Swimming : Activity
{
    public int Laps { get; private set; } // Number of laps

    public Swimming(DateTime date, int length, int laps) : base(date, length)
    {
        Laps = laps;
    }

    public override double GetDistance()
    {
        return Laps * 50 / 1000 * 0.62; // Distance in miles (50 meters per lap)
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Length) * 60;
    }

    public override double GetPace()
    {
        return Length / GetDistance();
    }

    public override string GetSummary()
    {
        return $"{Date.ToShortDateString()} Swimming ({Length} min) - Laps: {Laps}, Distance: {GetDistance():0.0} miles, Speed: {GetSpeed():0.0} mph, Pace: {GetPace():0.0} min per mile";
    }
}

class Program
{
    static void Main()
    {
        // Create activity instances
        Running running = new Running(new DateTime(2024, 6, 1), 30, 3.0);
        Cycling cycling = new Cycling(new DateTime(2024, 6, 2), 45, 15.0);
        Swimming swimming = new Swimming(new DateTime(2024, 6, 3), 60, 20);

        // Create list of activities
        List<Activity> activities = new List<Activity> { running, cycling, swimming };

        // Display activity summaries
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
            Console.WriteLine("-----");
        }
    }
}
