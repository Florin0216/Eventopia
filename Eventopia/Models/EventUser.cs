using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eventopia.Models;

[Table("EventUser")]
public class EventUser
{
    [ForeignKey("Event")]
    [Column("event_id")]
    public int EventId { get; set; }
    
    [ForeignKey("Users")]
    [Column("Id")]
    public string userId { get; set; }
    
    public Users User { get; set; }
    public Event Event { get; set; }
}