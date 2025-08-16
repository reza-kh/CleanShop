using Application.Inventory.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.Queries.GetAllInventory;

public sealed class GetAllInventoryQuery : IRequest<List<InventoryDto>> { }