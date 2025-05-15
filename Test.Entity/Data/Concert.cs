using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Entity.Data;

public class Concert
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

    public int OccupiedSeat { get; set; } = 0;

    [Required]
    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();


}
