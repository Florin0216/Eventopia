using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eventopia.Models;

[Table(name: "TicketUser")]
public class TicketUser
{
    [ForeignKey("Ticket")]
    [Column("ticket_id")]
    public int ticketId { get; set; }
    
    [ForeignKey("Users")]
    [Column("Id")]
    public string userId { get; set; }
    
    public Ticket Ticket { get; set; }
    public Users User { get; set; }
}