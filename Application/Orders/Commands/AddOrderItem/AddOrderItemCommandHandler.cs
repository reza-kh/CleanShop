using Application.Interfaces;
using Domain.Common;
using Domain.Orders.Entity;
using Domain.Products.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.AddOrderItem;

public sealed class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand,Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddOrderItemCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken)
            ?? throw new DomainException("Order not found.");

        order.AddItem(request.ProductId, request.Quantity, request.UnitPrice, request.UserId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return order.Id;
    }

}