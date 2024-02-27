namespace JokesAPI.Models;

public class JokeDto
{
    public int Id { get; set; }

    public string Content { get; set; }

    public int AuhtorId { get; set; }
}

public class JokeDetailDto
{
    public int Id { get; set; }

    public string Content { get; set; }

    public int AuthorId { get; set; }

    public string AuthorName { get; set; }
}
