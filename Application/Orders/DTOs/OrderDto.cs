using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.DTOs;

public sealed class OrderDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
    public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
}
