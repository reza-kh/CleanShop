using Domain.Customers.Entity;
using Domain.Orders.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ICustomerRepository 
{
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
    Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken = default);
}
