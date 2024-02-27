using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JokesAPI.Models;

[Index(nameof(Username), IsUnique = true)]
public class User
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Image { get; set; }

    public ICollection<Joke>? Jokes { get; } = new List<Joke>();

    public User()
    {
        Image = "https://ih1.redbubble.net/image.5137167725.5314/raf,360x360,075,t,fafafa:ca443f4786.jpg";
    }
}