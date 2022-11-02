using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace libraryAPI.EfCore;

[Table("relation")]
public class Relation
{
    [Key, Required]
    public string? book_name { get; set; }         
    // [Key, Required]
	public DateTime? book_date_of_first_publication  { get; set; }
    // [Key, Required]
    public string? author_name { get; set; }
    // [Key, Required]
    public DateTime? author_date_of_birth { get; set; }
}