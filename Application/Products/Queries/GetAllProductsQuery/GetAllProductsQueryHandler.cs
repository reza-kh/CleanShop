using Application.Interfaces;
using Application.Products.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetAllProductsQuery;

public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _repository;
    public GetAllProductsQueryHandler(IProductRepository repository) => _repository = repository;

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);
        return products.Select(p => new ProductDto(p.Id, p.Name, p.Price)).ToList();
    }
}