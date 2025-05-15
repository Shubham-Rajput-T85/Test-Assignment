using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Entity.Data;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Required]
    public string Email {get; set;} = null!;

    [MaxLength(100)] [Required]
    public string Name { get; set; } = null!;

    [MaxLength(30)] [Required]
    public string Role { get; set; } = null!;

    [MinLength(8)] [Required]
    public string Password { get; set; } = "password";

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
