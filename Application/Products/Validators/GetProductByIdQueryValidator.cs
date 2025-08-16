using Application.Customers.Queries.GetCustomerById;
using Application.Products.Queries.GetProductById;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Validators;

public sealed class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("CustomerId is required.");
    }
}
