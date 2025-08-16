using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderStatusService
    {
        Task ConfirmAsync(Guid orderId, string userId, CancellationToken ct);
        Task ShipAsync(Guid orderId, string userId, CancellationToken ct);
        Task DeliverAsync(Guid orderId, string userId, CancellationToken ct);
    }
}
