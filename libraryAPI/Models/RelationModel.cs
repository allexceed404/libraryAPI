namespace libraryAPI.Models;
public class RelationModel
{        
    public string? book_name { get; set; } 
	public DateTime? book_date_of_first_publication  { get; set; }
    public string? author_name { get; set; }
    public DateTime? author_date_of_birth { get; set; }
}