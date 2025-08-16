using Application.Customers.DTOs;
using Application.Interfaces;
using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Queries.GetCustomerById;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    private readonly ICustomerRepository _repository;
    public GetCustomerByIdQueryHandler(ICustomerRepository repository) => _repository = repository;

    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return customer == null ?null : new CustomerDto(customer.Id, customer.FullName, customer.Email);
    }
}