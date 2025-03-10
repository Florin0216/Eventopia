using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventopia.Models;

[Table(name : "Event")]
public class Event
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(name: "event_id")]
    public int Id { get; set; }
    
    [Column(name: "event_name")]
    [StringLength(50)]
    [Required]
    public string name { get; set; }
    
    [Column(name: "event_date")]
    [DataType(DataType.Date)]
    [Required]
    public DateTime date { get; set; }
    
    [Column(name: "event_time")]
    [DataType(DataType.Time)]
    [Required]
    public DateTime time { get; set; }
    
    [Column(name: "event_location")]
    [StringLength(50)]
    [Required]
    public string location { get; set; }
    
    [Column(name: "event_description")]
    [StringLength(500)]
    public string? description { get; set; }
    
    [Column(name: "photo_path")]
    public string? photoPath { get; set; }
    
    public virtual ICollection<TicketEvent> TicketEvents { get; set; }
}