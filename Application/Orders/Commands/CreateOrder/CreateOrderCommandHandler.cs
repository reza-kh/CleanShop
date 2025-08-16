using Application.Interfaces;
using Domain.Orders.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderItems = request.Items
                .Select(i => new OrderItem(i.ProductId, i.Quantity, i.UnitPrice, request.CreatorUserId))
                .ToList();

            var order =Order.Create(request.CustomerId, orderItems,request.CreatorUserId);
            

            await _orderRepository.AddAsync(order, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
