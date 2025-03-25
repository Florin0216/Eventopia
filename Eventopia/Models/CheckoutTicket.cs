using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eventopia.Models;

[Table(name:"CheckoutTicket")]
public class CheckoutTicket
{
    [ForeignKey("Checkout")]
    [Column("checkout_id")]
    public int checkoutId { get; set; }
    
    public Checkout Checkout { get; set; }
    
    [ForeignKey("Ticket")]
    [Column("ticket_id")]
    public int ticketId { get; set; }
    
    public Ticket Ticket { get; set; }
}