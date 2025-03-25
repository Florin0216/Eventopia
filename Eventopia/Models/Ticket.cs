using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventopia.Models;

[Table(name: "Ticket")]
public class Ticket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(name: "ticket_id")]
    public int Id { get; set; }
    
    [Column(name: "ticket_type")]
    [StringLength(50)]
    [Required]
    public string Type { get; set; }
    
    [Column(name: "ticket_price")]
    [Required]
    public decimal Price { get; set; }
    
    [Column(name: "ticket_status")]
    [StringLength(50)]
    public string? Status { get; set; }
    
    [Column(name: "quantity")]
    [Required]
    public int Quantity { get; set; }
    
    [Column(name: "purchase_date")]
    public DateTime? PurchaseDate { get; set; }
    
    [ForeignKey("Event")]
    [Column(name: "event_id")]
    public int? EventId { get; set; }
    
    public Event? Event { get; set; }
    
    public ICollection<TicketUser>? TicketUsers { get; set; }
    public ICollection<CheckoutTicket>? CheckoutTickets { get; set; }
    
}