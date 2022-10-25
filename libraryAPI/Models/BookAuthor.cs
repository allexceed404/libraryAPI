namespace libraryAPI;
public class BookAuthor 
{        
	public string? book_name { get; set; }         
	public DateTime? book_dateOfFirstPublication { get; set; }  
	public int book_edition { get; set; }  
	public string? book_publisher { get; set; }  
	public string? book_originalLanguage { get; set; }  
    public string? author_name { get; set; }
    public DateTime? author_dateOfBirth { get; set; }       
    public string? author_country { get; set; }  
}