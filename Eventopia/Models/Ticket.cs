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
    public string type { get; set; }
    
    [Column(name: "ticket_price")]
    [Required]
    public decimal price { get; set; }
    
    [Column(name: "ticket_status")]
    [StringLength(50)]
    public string? status { get; set; }
    
    [Column(name: "purchase_date")]
    public DateTime? purchaseDate { get; set; }
    
    public virtual ICollection<TicketEvent> TicketEvents { get; set; }
    public virtual ICollection<TicketUser> TicketUsers { get; set; }
}