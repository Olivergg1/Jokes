namespace JokesAPI.Models;

public class UserDto
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Name { get; set; }

    public string Image {  get; set; }
}

public class UserDetailDto : UserDto
{
    public int Upvotes { get; set; }
    public bool HasUpvoted { get; set; } = false;
    public List<Joke> Jokes { get; set; } = [];
}

public class UserCredentialsDto
{
    public string Username { get; set; }

    public string Password { get; set; }

    public UserCredentialsDto(string username, string password)
    {
        Username = username;
        Password = password;
    }
}