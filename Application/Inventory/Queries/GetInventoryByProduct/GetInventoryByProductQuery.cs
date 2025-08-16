using Application.Inventory.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.Queries.GetInventoryByProduct;

public sealed class GetInventoryByProductQuery : IRequest<InventoryDto?>
{
    public Guid ProductId { get; set; }
}