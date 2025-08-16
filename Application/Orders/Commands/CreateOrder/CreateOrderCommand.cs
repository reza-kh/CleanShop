using Application.Orders.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }
    public string CreatorUserId { get; set; }=string.Empty;
    public List<OrderItemDto> Items { get; set; } = new();
}