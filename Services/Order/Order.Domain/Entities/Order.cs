using Shared.Entities;

namespace Order.Domain.Entities;
public class Order : Entity
{
    public string ShippingAddress { get; set; } = string.Empty;

    public List<OrderItem> OrderItems { get; set; } = [];
}
