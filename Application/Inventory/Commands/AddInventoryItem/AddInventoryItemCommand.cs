using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.Commands.AddInventoryItem;

public sealed class AddInventoryItemCommand : IRequest<Guid>
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string UserId { get; set; } = string.Empty;
}
