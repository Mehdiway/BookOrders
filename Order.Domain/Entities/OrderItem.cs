using Shared.Entities;

namespace Order.Domain.Entities;
public class OrderItem : Entity
{
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }
}
