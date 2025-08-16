using Domain.Customers.Entity;
using Domain.Inventory.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IInventoryRepository
{
    Task<InventoryItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(InventoryItem inventory, CancellationToken cancellationToken = default);
    Task<List<InventoryItem>> GetAllAsync(CancellationToken cancellationToken = default);
}
