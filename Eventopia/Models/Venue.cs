using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventopia.Models;

[Table(name:"Venue")]
public class Venue
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(name: "venue_id")]
    public int Id { get; set; }
    
    [Column(name: "venue_name")]
    [StringLength(50)]
    [Required]
    public string Name { get; set; }
    
    [Column(name: "address")]
    [StringLength(50)]
    [Required]
    public string Address { get; set; }
    
    [Column(name: "city")]
    [StringLength(50)]
    [Required]
    public string City { get; set; }
    
    [Column(name: "country")]
    [StringLength(50)]
    [Required]
    public string Country { get; set; }
    
    [Column(name: "capacity")]
    public int? Capacity { get; set; }
    
    public ICollection<Event>? Events { get; set; }
}