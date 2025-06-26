using Shared.Entities;
using Shared.Enums;

namespace Order.Domain.Entities;
public class Order : Entity
{
    public string ShippingAddress { get; set; } = string.Empty;
    public OrderStatus OrderStatus { get; set; }

    public List<OrderItem> OrderItems { get; set; } = [];
}
