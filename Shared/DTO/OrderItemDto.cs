namespace Shared.DTO;
public class OrderItemDto
{
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    public int OrderId { get; set; }
}
