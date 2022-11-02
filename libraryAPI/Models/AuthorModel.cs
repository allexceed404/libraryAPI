namespace libraryAPI.Models;
public class AuthorModel
{
    public string? name { get; set; }
    public DateTime? date_of_birth { get; set; }
    public string country { get; set; } = string.Empty;
    public List<BookId> books { get; set; } = new List<BookId>();
}