namespace JokesApp.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Username { get; set; }

    public string Image {  get; set; }

    public List<Joke> Jokes { get; set; }

    public User() 
    {
        Jokes ??= new List<Joke>();
    }
}