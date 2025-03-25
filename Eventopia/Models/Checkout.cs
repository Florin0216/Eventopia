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
    
    [Column(name: "currency")]
    [StringLength(10)]
    public string Currency { get; set; }
    
    [Column(name: "amount")]
    public float Amount { get; set; }
    
    [Column(name: "status")]
    [StringLength(10)]
    public string Status { get; set; }
    
    [ForeignKey("Users")]
    [Column(name: "user_id")]
    [Required]
    public string UserId { get; set; }
    
    public Users? Users { get; set; }
    
    public ICollection<CheckoutTicket>? CheckoutTickets { get; set; }
}