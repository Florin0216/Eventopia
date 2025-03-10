using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventopia.Models;

[Table(name: "TicketEvent")]
public class TicketEvent
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [ForeignKey(name: "Ticket")]
    [Column("ticket_id")]
    public int TicketId { get; set; }
    
    [ForeignKey("Event")]
    [Column("event_id")]
    public int EventId { get; set; }
    
    public Ticket Ticket { get; set; }
    public Event Event { get; set; }
}