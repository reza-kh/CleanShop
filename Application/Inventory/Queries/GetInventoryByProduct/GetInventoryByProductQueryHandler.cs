using Application.Interfaces;
using Application.Inventory.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.Queries.GetInventoryByProduct;

public sealed class GetInventoryByProductQueryHandler : IRequestHandler<GetInventoryByProductQuery, InventoryDto?>
{
    private readonly IInventoryRepository _repository;
    public GetInventoryByProductQueryHandler(IInventoryRepository repository) => _repository = repository;

    public async Task<InventoryDto?> Handle(GetInventoryByProductQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _repository.GetByProductIdAsync(request.ProductId, cancellationToken);
        return inventory == null ? null : new InventoryDto(inventory.Id, inventory.ProductId, inventory.Quantity);
    }
}