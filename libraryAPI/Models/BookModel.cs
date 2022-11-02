namespace libraryAPI.Models;
public class BookModel
{        
    public string? name { get; set; }
	public DateTime? date_of_first_publication { get; set; }  
	public int edition { get; set; } = 0;
	public string publisher { get; set; } = string.Empty;
	public string original_language { get; set; } = string.Empty;
	public List<AuthorId> authors { get; set; } = new List<AuthorId>();
}