using Application.Interfaces;
using Application.Orders.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PagedResult<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<PagedResult<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var (orders, totalCount) = await _orderRepository.GetOrdersAsync(
            request.Filter?.CustomerId,
            request.Filter?.Status,
            request.Filter?.CreatedFrom,
            request.Filter?.CreatedTo,
            request.PageIndex,
            request.PageSize,
            cancellationToken);

        var orderDtos = orders.Select(o => new OrderDto
        {
            Id = o.Id,
            CustomerId = o.CustomerId,
            Status = o.Status.ToString(),
            CreatedAtUtc = o.CreationDate,
            Items = o.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        }).ToList();

        return new PagedResult<OrderDto>
        {
            Items = orderDtos,
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }
}
