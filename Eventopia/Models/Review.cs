using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventopia.Models;

[Table(name:"Review")]
public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(name: "review_id")]
    public int Id { get; set; }
    
    [Column(name: "reviewer_rating")]
    [Range(1, 5)]
    [Required]
    public int ReviewerRating { get; set; }
    
    [Column(name: "review_comment")]
    [StringLength(500)]
    public string? ReviewComment { get; set; }
    
    [Column(name: "review_date")]
    [DataType(DataType.Date)]
    public DateTime ReviewDate { get; set; }
    
    [ForeignKey("Event")]
    [Column(name: "event_id")]
    public int? EventId { get; set; }
    
    public Event? Event { get; set; }
    
    [ForeignKey("Users")]
    [Column(name: "user_id")]
    [Required]
    public string? UserId { get; set; }
    
    public Users? User { get; set; }
}