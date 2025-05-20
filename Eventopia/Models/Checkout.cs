using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventopia.Models;

[Table(name:"Checkout")]
public class Checkout
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(name: "checkout_id")]
    public int Id { get; set; }
    
    [Column(name: "checkout_date")]
    [DataType(DataType.Date)]
    public DateTime CheckoutDate { get; set; }
    
    [Column(name: "location")]
    [StringLength(100)]
    public string Location { get; set; }
    
    [Column(name: "type")]
    [StringLength(100)]
    public string Type { get; set; }
    
    [Column(name: "amount")]
    public decimal Amount { get; set; }
    
    [Column(name: "quantity")]
    public int Quantity { get; set; }
    
    [ForeignKey("Users")]
    [Column(name: "user_id")]
    [Required]
    public string? UserId { get; set; }
    
    public Users? User { get; set; }
    
    [ForeignKey("Event")]
    [Column(name: "event_id")]
    public int? EventId { get; set; }
    
    public Event? Event { get; set; }
    
    public ICollection<CheckoutTicket>? CheckoutTickets { get; set; }
}