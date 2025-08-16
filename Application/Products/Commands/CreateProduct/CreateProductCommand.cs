using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string UserId { get; set; } = string.Empty;
}