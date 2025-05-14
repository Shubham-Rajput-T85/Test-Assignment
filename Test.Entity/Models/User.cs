using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Test.Entity.Models;

public class User
{   
    public int UserId { get; set; }

    public string Name { get; set; }

    public string Role { get; set; }
    
}
