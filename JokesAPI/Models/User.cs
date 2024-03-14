using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JokesAPI.Models;

[Index(nameof(UserName), IsUnique = true)]
public class User : IdentityUser<int>
{
    [Required]
    public string Name { get; set; } = "Unknown Jokster";

    [Required]
    public string Image { get; set; }

    public ICollection<Joke>? Jokes { get; } = new List<Joke>();

    public List<UserUpvote> Upvoters { get; set; } = [];

    public List<UserUpvote> UpvotedUsers { get; set; } = [];

    public bool HasUpdatedName => Name != "Unknown Jokster";

    public User()
    {
        Image = "https://ih1.redbubble.net/image.5137167725.5314/raf,360x360,075,t,fafafa:ca443f4786.jpg";
    }
}