using System.ComponentModel.DataAnnotations.Schema;
namespace libraryAPI.EfCore;

[Table("relation")]
public class Relation
{
    public string? bookname { get; set; }
    public DateTime? bookdate_of_first_publication { get; set; }
    public Book book { get; set; }
    public string? authorname { get; set; }
    public DateTime? authordate_of_birth { get; set; }
    public Author author { get; set; }
}