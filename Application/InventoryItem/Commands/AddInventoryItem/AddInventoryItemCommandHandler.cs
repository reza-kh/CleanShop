using Application.Interfaces;
using Domain.Inventory.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.Commands.AddInventoryItem
{
    public sealed class AddInventoryItemCommandHandler : IRequestHandler<AddInventoryItemCommand, Guid>
    {
        private readonly IInventoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AddInventoryItemCommandHandler(IInventoryRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddInventoryItemCommand request, CancellationToken cancellationToken)
        {
            var inventory = new InventoryItem(request.ProductId, request.Quantity, request.UserId);
            await _repository.AddAsync(inventory, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return inventory.Id;
        }
    }
}
