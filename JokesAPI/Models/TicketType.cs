using System.ComponentModel.DataAnnotations;

namespace JokesAPI.Models;

public class TicketType
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public List<Ticket>? Tickets { get; set; }
}
