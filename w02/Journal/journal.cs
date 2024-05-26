using System;
using System.Collections.Generic;
using System.IO;

public class Entry {
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public Entry(string prompt, string response, string date) {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public override string ToString() {
        return $"{Prompt} | {Response} | {Date}";
    }
}

public class Journal {
    public List<Entry> Entries { get; } = new List<Entry>();

    public void AddEntry(string prompt, string response, string date) {
        Entries.Add(new Entry(prompt, response, date));
    }

    public void DisplayJournal() {
        foreach (var entry in Entries) {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string filename) {
        using (var writer = new StreamWriter(filename)) {
            foreach (var entry in Entries) {
                writer.WriteLine(entry);
            }
        }
    }

    public void LoadFromFile(string filename) {
        Entries.Clear();
        using (var reader = new StreamReader(filename)) {
            string line;
            while ((line = reader.ReadLine()) != null) {
                var parts = line.Split('|');
                AddEntry(parts[0].Trim(), parts[1].Trim(), parts[2].Trim());
            }
        }
    }
}

public class App {
    private Journal journal = new Journal();
    private List<string> prompts = new List<string> {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public void Run() {
        bool exit = false;
        while (!exit) {
            exit = ShowMenu();
        }
    }

    private bool ShowMenu() {
        Console.WriteLine("1. Write new entry");
        Console.WriteLine("2. Display journal");
        Console.WriteLine("3. Save journal");
        Console.WriteLine("4. Load journal");
        Console.WriteLine("5. Exit");
        Console.Write("Choose an option: ");
        switch (Console.ReadLine()) {
            case "1":
                HandleNewEntry();
                break;
            case "2":
                HandleDisplayJournal();
                break;
            case "3":
                HandleSaveJournal();
                break;
            case "4":
                HandleLoadJournal();
                break;
            case "5":
                return true;
            default:
                Console.WriteLine("Invalid option, please try again.");
                break;
        }
        return false;
    }

    private void HandleNewEntry() {
        var random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine($"Today's prompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();
        string date = DateTime.Now.ToString("yyyy-MM-dd");
        journal.AddEntry(prompt, response, date);
    }

    private void HandleDisplayJournal() {
        journal.DisplayJournal();
    }

    private void HandleSaveJournal() {
        Console.Write("Enter filename to save: ");
        string filename = Console.ReadLine();
        journal.SaveToFile(filename);
        Console.WriteLine("Journal saved.");
    }

    private void HandleLoadJournal() {
        Console.Write("Enter filename to load: ");
        string filename = Console.ReadLine();
        journal.LoadFromFile(filename);
        Console.WriteLine("Journal loaded.");
    }
}

class Program {
    static void Main(string[] args) {
        var app = new App();
        app.Run();
    }
}
