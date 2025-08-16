using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.AddOrderItemsBulk;

public sealed class AddOrderItemsBulkCommand : IRequest<Guid>
{
    public Guid OrderId { get; set; }
    public List<(Guid ProductId, int Quantity, decimal UnitPrice)> Items { get; set; } = new();
    public string UserId { get; set; } = string.Empty;
}
