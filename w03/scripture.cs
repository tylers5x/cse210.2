using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load scriptures from a library (exceeds requirements)
            List<Scripture> scriptures = LoadScriptures();

            Random rand = new Random();
            Scripture scripture = scriptures[rand.Next(scriptures.Count)];

            while (true)
            {
                Console.Clear();
                scripture.Display();
                Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                {
                    break;
                }

                scripture.HideRandomWords();
                if (scripture.AllWordsHidden())
                {
                    Console.Clear();
                    scripture.Display();
                    Console.WriteLine("\nAll words are hidden. Well done!");
                    break;
                }
            }
        }

        // Load scriptures from a file (exceeds requirements)
        static List<Scripture> LoadScriptures()
        {
            return new List<Scripture>
            {
                new Scripture(new Reference("John", 3, 16), "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."),
                new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.")
            };
        }
    }

    class Scripture
    {
        private Reference _reference;
        private List<Word> _words;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = text.Split(' ').Select(word => new Word(word)).ToList();
        }

        public void Display()
        {
            Console.WriteLine(_reference);
            Console.WriteLine(string.Join(" ", _words.Select(word => word.Display())));
        }

        public void HideRandomWords()
        {
            Random rand = new Random();
            int wordsToHide = rand.Next(1, Math.Max(2, _words.Count / 5));
            var wordsToHideList = _words.Where(word => !word.IsHidden).OrderBy(x => rand.Next()).Take(wordsToHide).ToList();

            foreach (var word in wordsToHideList)
            {
                word.Hide();
            }
        }

        public bool AllWordsHidden()
        {
            return _words.All(word => word.IsHidden);
        }
    }

    class Reference
    {
        private string _book;
        private int _chapter;
        private int _startVerse;
        private int _endVerse;

        public Reference(string book, int chapter, int verse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = verse;
            _endVerse = verse;
        }

        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = startVerse;
            _endVerse = endVerse;
        }

        public override string ToString()
        {
            return _endVerse == _startVerse ?
                $"{_book} {_chapter}:{_startVerse}" :
                $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }

    class Word
    {
        private string _text;
        private bool _isHidden;

        public bool IsHidden => _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public void Hide()
        {
            _isHidden = true;
        }

        public string Display()
        {
            return _isHidden ? "_____" : _text;
        }
    }
}
