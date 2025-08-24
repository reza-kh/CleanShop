using Application.Interfaces;
using Domain.Inventory.Entity;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    internal class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(InventoryItem entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<InventoryItem>().AddAsync(entity, cancellationToken);
        }

        public async Task<List<InventoryItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<InventoryItem>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<InventoryItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<InventoryItem>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }

        public async Task<InventoryItem?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            return await _context.Set<InventoryItem>().FirstOrDefaultAsync(i => i.ProductId == productId, cancellationToken);
        }
    }
}
