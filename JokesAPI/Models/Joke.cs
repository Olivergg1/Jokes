using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JokesAPI.Models;

public class Joke
{
    public int Id { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public int AuthorId { get; set; }

    public User? Author { get; init; }
}