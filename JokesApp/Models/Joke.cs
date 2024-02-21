namespace JokesApp.Models;

public class Joke
{
    public int Id { get; set; }

    public string Content { get; set; }

    public int AuthorId { get; set; }
}
