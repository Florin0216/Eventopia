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
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [Column(name: "event_date")]
    [DataType(DataType.Date)]
    [Required]
    public DateOnly Date { get; set; }
    
    [Column(name: "event_time")]
    [DataType(DataType.Time)]
    [Required]
    public TimeOnly Time { get; set; }
    
    [Column(name: "event_description")]
    [StringLength(500)]
    public string? Description { get; set; }
    
    [Column(name: "photo_path")]
    public string? PhotoPath { get; set; }
    
    [ForeignKey("Venue")]
    [Column(name: "venue_id")]
    public int? VenueId { get; set; }
    
    public Venue? Venue { get; set; }
    
    [ForeignKey("Category")]
    [Column(name: "category_id")]
    public int? CategoryId { get; set; }
    
    public Category? Category { get; set; }
    
    public ICollection<TicketUser>? TicketUsers { get; set; }
    
}