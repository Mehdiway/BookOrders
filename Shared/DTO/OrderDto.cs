namespace Shared.DTO;
public class OrderDto
{
    public int Id { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;

    public List<OrderItemDto> OrderItems { get; set; } = [];
}
