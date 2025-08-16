using Application.Interfaces;
using Domain.Orders.Entity;
using Domain.Orders.Enum;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Order entity, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(entity, cancellationToken);
        }

        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<(IReadOnlyList<Order> Orders, int TotalCount)> GetOrdersAsync(
            Guid? customerId,
            OrderStatus? status,
            DateTime? createdFrom,
            DateTime? createdTo,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = _context.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .AsQueryable();

            if (customerId.HasValue)
                query = query.Where(o => o.CustomerId == customerId.Value);

            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            if (createdFrom.HasValue)
                query = query.Where(o => o.CreationDate >= createdFrom.Value);

            if (createdTo.HasValue)
                query = query.Where(o => o.CreationDate <= createdTo.Value);

            var totalCount = await query.CountAsync(cancellationToken);

            var orders = await query
                .OrderByDescending(o => o.CreationDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (orders, totalCount);
        }
    }
}
