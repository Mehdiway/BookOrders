using Shared.DTO;
using Shared.Enums;

namespace Shared.Messaging.Events;
public class OrderPlacedEvent
{
    public int OrderId { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public OrderStatus OrderStatus { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = [];
}
