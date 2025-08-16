using Application.Customers.Queries.GetCustomerById;
using Application.Orders.Queries.GetOrderById;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Validators;

public sealed class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("CustomerId is required.");
    }
}
