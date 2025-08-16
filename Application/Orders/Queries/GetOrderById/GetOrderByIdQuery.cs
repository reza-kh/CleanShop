using Application.Orders.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderDto?>;