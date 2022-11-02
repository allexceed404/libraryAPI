using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace libraryAPI.EfCore;

[Table("author")]
public class Author
{
    [Key, Required]
    public string? name { get; set; }
    // [Key, Required]
    public DateTime? date_of_birth { get; set; }
    public string country { get; set; } = string.Empty;
}