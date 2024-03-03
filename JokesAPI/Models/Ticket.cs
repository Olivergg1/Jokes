using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JokesAPI.Models;

public class Ticket
{
    public int Id { get; set; }

    [Required]
    public int TycketTypeId { get; set; }

    [Required]
    public int JokeId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime IssuedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }

    public TicketType? TicketType { get; set; }
    public Joke? Joke { get; set; }
}
