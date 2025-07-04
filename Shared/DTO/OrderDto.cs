﻿using Shared.Enums;

namespace Shared.DTO;
public class OrderDto
{
    public int Id { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public OrderStatus OrderStatus { get; set; }

    public List<OrderItemDto> OrderItems { get; set; } = [];
}
