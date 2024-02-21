using System.ComponentModel.DataAnnotations.Schema;

namespace JokesAPI.Models;

public class Joke
{
    public int Id { get; set; }

    public string Content { get; set; }

    [ForeignKey(nameof(User))]
    public int AuthorId { get; set; }
}