namespace JokesApp.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string Image {  get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public List<Joke> Jokes { get; set; } = [];

    public int Upvotes { get; set; } = 0;
    public bool HasUpvoted { get; set; } = false;

    public Dictionary<string, string> Claims { get; set; } = [];
}