using Domain.Orders.Entity;
using Domain.Orders.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken = default);
        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<(IReadOnlyList<Order> Orders, int TotalCount)> GetOrdersAsync(
            Guid? customerId,
            OrderStatus? status,
            DateTime? createdFrom,
            DateTime? createdTo,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken);
    }
}
