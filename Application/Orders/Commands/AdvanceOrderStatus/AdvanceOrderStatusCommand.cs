using Domain.Orders.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.AdvanceOrderStatus
{
    public sealed class AdvanceOrderStatusCommand : IRequest<OrderStatus>
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
