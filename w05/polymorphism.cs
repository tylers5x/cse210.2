using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EternalQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            GoalManager goalManager = new GoalManager();
            goalManager.LoadGoals();
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Eternal Quest Menu");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. Record Event");
                Console.WriteLine("3. Show Goals");
                Console.WriteLine("4. Show Score");
                Console.WriteLine("5. Save and Exit");

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        goalManager.CreateGoal();
                        break;
                    case "2":
                        goalManager.RecordEvent();
                        break;
                    case "3":
                        goalManager.ShowGoals();
                        break;
                    case "4":
                        goalManager.ShowScore();
                        break;
                    case "5":
                        goalManager.SaveGoals();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please choose again.");
                        break;
                }
            }
        }
    }

    abstract class Goal
    {
        protected string Name { get; set; }
        protected string Description { get; set; }
        protected int Points { get; set; }
        public abstract void RecordEvent();
        public abstract bool IsCompleted();
        public abstract void DisplayGoal();
    }

    class SimpleGoal : Goal
    {
        private bool isCompleted;

        public SimpleGoal(string name, string description, int points)
        {
            Name = name;
            Description = description;
            Points = points;
            isCompleted = false;
        }

        public override void RecordEvent()
        {
            isCompleted = true;
        }

        public override bool IsCompleted()
        {
            return isCompleted;
        }

        public override void DisplayGoal()
        {
            Console.WriteLine($"{(isCompleted ? "[X]" : "[ ]")} {Name} - {Description} (Points: {Points})");
        }
    }

    class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
        {
            Name = name;
            Description = description;
            Points = points;
        }

        public override void RecordEvent()
        {
            // Eternal goals are never completed but always gain points
        }

        public override bool IsCompleted()
        {
            return false; // Eternal goals are never completed
        }

        public override void DisplayGoal()
        {
            Console.WriteLine($"[ ] {Name} - {Description} (Points: {Points} per event)");
        }
    }

    class ChecklistGoal : Goal
    {
        private int targetCount;
        private int currentCount;
        private int bonusPoints;

        public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        {
            Name = name;
            Description = description;
            Points = points;
            this.targetCount = targetCount;
            this.bonusPoints = bonusPoints;
            currentCount = 0;
        }

        public override void RecordEvent()
        {
            currentCount++;
        }

        public override bool IsCompleted()
        {
            return currentCount >= targetCount;
        }

        public override void DisplayGoal()
        {
            Console.WriteLine($"{(IsCompleted() ? "[X]" : "[ ]")} {Name} - {Description} (Completed {currentCount}/{targetCount} times, {Points} points per event, Bonus: {bonusPoints} points)");
        }
    }

    class GoalManager
    {
        private List<Goal> goals;
        private int score;

        public GoalManager()
        {
            goals = new List<Goal>();
            score = 0;
        }

        public void CreateGoal()
        {
            Console.WriteLine("Choose goal type:");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");

            Console.Write("Enter choice: ");
            string choice = Console.ReadLine();

            Console.Write("Enter goal name: ");
            string name = Console.ReadLine();

            Console.Write("Enter goal description: ");
            string description = Console.ReadLine();

            Console.Write("Enter points: ");
            int points = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case "1":
                    goals.Add(new SimpleGoal(name, description, points));
                    break;
                case "2":
                    goals.Add(new EternalGoal(name, description, points));
                    break;
                case "3":
                    Console.Write("Enter target count: ");
                    int targetCount = int.Parse(Console.ReadLine());
                    Console.Write("Enter bonus points: ");
                    int bonusPoints = int.Parse(Console.ReadLine());
                    goals.Add(new ChecklistGoal(name, description, points, targetCount, bonusPoints));
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        public void RecordEvent()
        {
            Console.WriteLine("Choose a goal to record an event:");
            for (int i = 0; i < goals.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                goals[i].DisplayGoal();
            }

            Console.Write("Enter choice: ");
            int choice = int.Parse(Console.ReadLine()) - 1;

            if (choice >= 0 && choice < goals.Count)
            {
                goals[choice].RecordEvent();
                score += goals[choice].Points;

                if (goals[choice] is ChecklistGoal checklistGoal && checklistGoal.IsCompleted())
                {
                    score += checklistGoal.bonusPoints;
                }

                Console.WriteLine("Event recorded.");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                Thread.Sleep(2000);
            }
        }

        public void ShowGoals()
        {
            foreach (var goal in goals)
            {
                goal.DisplayGoal();
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        public void ShowScore()
        {
            Console.WriteLine($"Your current score is: {score}");
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        public void SaveGoals()
        {
            using (StreamWriter writer = new StreamWriter("goals.txt"))
            {
                writer.WriteLine(score);
                foreach (var goal in goals)
                {
                    writer.WriteLine($"{goal.GetType().Name}|{goal.Name}|{goal.Description}|{goal.Points}");
                    if (goal is ChecklistGoal checklistGoal)
                    {
                        writer.WriteLine($"{checklistGoal.targetCount}|{checklistGoal.currentCount}|{checklistGoal.bonusPoints}");
                    }
                }
            }
        }

        public void LoadGoals()
        {
            if (File.Exists("goals.txt"))
            {
                using (StreamReader reader = new StreamReader("goals.txt"))
                {
                    score = int.Parse(reader.ReadLine());
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        string type = parts[0];
                        string name = parts[1];
                        string description = parts[2];
                        int points = int.Parse(parts[3]);

                        switch (type)
                        {
                            case nameof(SimpleGoal):
                                goals.Add(new SimpleGoal(name, description, points));
                                break;
                            case nameof(EternalGoal):
                                goals.Add(new EternalGoal(name, description, points));
                                break;
                            case nameof(ChecklistGoal):
                                int targetCount = int.Parse(reader.ReadLine());
                                int currentCount = int.Parse(reader.ReadLine());
                                int bonusPoints = int.Parse(reader.ReadLine());
                                goals.Add(new ChecklistGoal(name, description, points, targetCount, bonusPoints));
                                break;
                        }
                    }
                }
            }
        }
    }
}
