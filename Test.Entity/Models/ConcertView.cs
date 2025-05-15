using System.ComponentModel.DataAnnotations;

namespace Test.Entity.Models;

public class ConcertView
{
    public int ConcertId { get; set; }
    
    [MaxLength(100)] [Required]
    public string Title { get; set; } = null!;


    [MaxLength(100)] [Required]
    public string ArtistName { get; set; } = null!;

    [MaxLength(100)] [Required]
    public string Venue { get; set; } = null!;

    [Required]
    public DateTime ConcertDate { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int TotalSeat { get; set; }

}
