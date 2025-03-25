using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventopia.Models;

[Table(name:"Category")]
public class Category
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(name: "category_id")]
    public int Id { get; set; }
    
    [Column(name: "category_name")]
    [StringLength(50)]
    [Required]
    public string Name { get; set; }
    
    [Column(name: "category_description")]
    [StringLength(100)]
    public string? Description { get; set; }
    
    public ICollection<Event>? Events { get; set; }
}