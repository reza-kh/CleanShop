using Application.Products.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetAllProductsQuery;


public sealed class GetAllProductsQuery : IRequest<List<ProductDto>> { }