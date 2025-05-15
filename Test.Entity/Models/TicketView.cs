using System.ComponentModel.DataAnnotations;

namespace Test.Entity.Models;

public class TicketView
{

    public int TicketId { get; set; }
    public int UserId { get; set; }


    public int ConcertId { get; set; }

    [Required]
    public DateTime BookTime { get; set; }

    [Required] [Range(1 , 100 ,ErrorMessage = "seat can only be selected between 1 - 100." )]
    public int Seat { get; set; }

    public int Discount { get; set; }

    public decimal Amount { get; set; }
}
