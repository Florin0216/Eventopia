using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Eventopia.Models;

[Table(name : "Users")]
public class Users : IdentityUser
{
    [Column(name: "FirstName")]
    [StringLength(50)]
    public string? FirstName { get; set; }
    
    [Column(name: "LastName")]
    [StringLength(50)]
    public string? LastName { get; set; }

    [Column(name: "Gender")]
    [StringLength(1)]
    public string? Gender { get; set; }
    
    public virtual ICollection<TicketUser> TicketUsers { get; set; }
}