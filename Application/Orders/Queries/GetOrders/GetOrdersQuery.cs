using Application.Orders.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Queries.GetOrders;

public class GetOrdersQuery : IRequest<PagedResult<OrderDto>>
{
    public OrderFilterDto? Filter { get; set; }
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
