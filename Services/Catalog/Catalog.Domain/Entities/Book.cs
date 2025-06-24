namespace Catalog.Domain.Entities;
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateOnly PublicationDate { get; set; }
}
