using Application.Interfaces;
using Application.Services;
using Domain.Common;
using Domain.Orders.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.AdvanceOrderStatus
{
    internal class AdvanceOrderStatusCommandHandler : IRequestHandler<AdvanceOrderStatusCommand, OrderStatus>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdvanceOrderStatusCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderStatus> Handle(AdvanceOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId)
                            ?? throw new DomainException($"Order with Id {request.OrderId} not found.");

            try
            {
                var newStatus=OrderStatusService.Advance(order, request.UserId);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return newStatus;
            }
            catch (DomainException ex)
            {
                throw new DomainException($"An error occurred while changing the order status with Id {request.OrderId}. Message: {ex.Message}");
            }
        }
    }
}
