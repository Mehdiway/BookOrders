using Shared.Entities;

namespace Catalog.Domain.Entities;
public class Book : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateOnly PublicationDate { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
