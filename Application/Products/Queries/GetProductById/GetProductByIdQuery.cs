using Application.Products.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetProductById;

public sealed class GetProductByIdQuery : IRequest<ProductDto?>
{
    public Guid Id { get; set; }
}