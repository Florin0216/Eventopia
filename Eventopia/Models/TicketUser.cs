using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventopia.Models;

[Table(name: "TicketUser")]
public class TicketUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [ForeignKey("Ticket")]
    [Column("ticket_id")]
    public int ticketId { get; set; }
    
    [ForeignKey("Users")]
    [Column("user_id")]
    public int userId { get; set; }
    
    public virtual Ticket ticket { get; set; }
    public virtual Users user { get; set; }
}