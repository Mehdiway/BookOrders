using Shared.DTO;

namespace Shared.Messaging.Events;
public record OrderPlacedEvent
{
    public int OrderId { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public List<OrderItemDto> OrderItems { get; set; } = [];
}
