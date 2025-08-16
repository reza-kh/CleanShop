using Application.Customers.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Queries.GetCustomerById;

public sealed class GetCustomerByIdQuery : IRequest<CustomerDto?>
{
    public Guid Id { get; set; }
}