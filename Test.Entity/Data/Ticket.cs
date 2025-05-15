using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test.Entity.Data;

public class Ticket
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TicketId { get; set; }

    [ForeignKey("UserId")] 
    public int UserId { get; set; }

    [ForeignKey("Concert")]
    public int ConcertId { get; set; }

    public DateTime BookTime { get; set; }

    public int Seat { get; set; }

    [Range(0, 100)]
    public int Discount { get; set; }

    public decimal Amount { get; set; }

    public virtual Concert Concert { get; set; } = null!;

    public virtual User User { get; set; } = null!;

}
