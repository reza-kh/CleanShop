using Application.Interfaces;
using Application.Inventory.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.Queries.GetAllInventory;

public sealed class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoryQuery, List<InventoryDto>>
{
    private readonly IInventoryRepository _repository;
    public GetAllInventoryQueryHandler(IInventoryRepository repository) => _repository = repository;

    public async Task<List<InventoryDto>> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken)
    {
        var inventories = await _repository.GetAllAsync(cancellationToken);
        return inventories.Select(i => new InventoryDto(i.Id, i.ProductId, i.Quantity)).ToList();
    }
}