using Application.Customers.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Queries.GetAllCustomers;

public sealed class GetAllCustomersQuery : IRequest<List<CustomerDto>> { }