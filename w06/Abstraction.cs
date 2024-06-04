using System;
using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }

    public string DisplayComment()
    {
        return $"{CommenterName}: {CommentText}";
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");
        foreach (var comment in Comments)
        {
            Console.WriteLine(comment.DisplayComment());
        }
    }
}

class Program
{
    static void Main()
    {
        // Create video instances
        Video video1 = new Video("Python Tutorial", "John Doe", 300);
        Video video2 = new Video("Learn JavaScript", "Jane Smith", 600);
        Video video3 = new Video("Understanding Databases", "Alice Johnson", 450);

        // Create comment instances and add to videos
        Comment comment1 = new Comment("User1", "Great video!");
        Comment comment2 = new Comment("User2", "Very informative.");
        Comment comment3 = new Comment("User3", "Helped me a lot, thanks!");
        Comment comment4 = new Comment("User4", "Awesome content!");

        video1.AddComment(comment1);
        video1.AddComment(comment2);

        video2.AddComment(comment3);
        video2.AddComment(comment4);

        video3.AddComment(comment1);
        video3.AddComment(comment3);

        // Add videos to a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Iterate through the list of videos and display their information
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
            Console.WriteLine("-----");
        }
    }
}
