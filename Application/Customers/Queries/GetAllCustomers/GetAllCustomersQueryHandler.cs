using Application.Customers.DTOs;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Queries.GetAllCustomers;

public sealed class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
{
    private readonly ICustomerRepository _repository;
    public GetAllCustomersQueryHandler(ICustomerRepository repository) => _repository = repository;

    public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _repository.GetAllAsync(cancellationToken);
        return customers.Select(c => new CustomerDto(c.Id, c.FullName, c.Email)).ToList();
    }
}
