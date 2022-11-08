using System.ComponentModel.DataAnnotations.Schema;
namespace libraryAPI.EfCore;

[Table("author")]
public class Author
{
    public string? name { get; set; }
    public DateTime? date_of_birth { get; set; }
    public string country { get; set; } = string.Empty;
}