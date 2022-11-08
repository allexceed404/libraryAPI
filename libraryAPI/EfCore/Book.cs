using System.ComponentModel.DataAnnotations.Schema;
namespace libraryAPI.EfCore;

[Table("book")]
public class Book
{
    public string? name { get; set; }
	public DateTime? date_of_first_publication { get; set; }  
	public int edition { get; set; }  
	public string publisher { get; set; } = string.Empty;
	public string original_language { get; set; } = string.Empty;
}