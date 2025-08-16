using Application.Interfaces;
using Domain.Customers.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Commands.CreateCustomer
{
    public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.Name, request.Email,request.UserId);
            await _repository.AddAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return customer.Id;
        }
    }
}
