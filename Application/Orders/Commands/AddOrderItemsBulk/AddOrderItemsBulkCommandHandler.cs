using Application.Interfaces;
using Domain.Common;
using Domain.Orders.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.AddOrderItemsBulk
{
    public sealed class AddOrderItemsBulkCommandHandler : IRequestHandler<AddOrderItemsBulkCommand,Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AddOrderItemsBulkCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(AddOrderItemsBulkCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken)
            ?? throw new DomainException("Order not found.");

            foreach (var i in request.Items)
            {
                order.AddItem(i.ProductId, i.Quantity, i.UnitPrice, request.UserId);// Currently adds items one by one. Consider bulk insert method.
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
