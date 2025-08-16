using Application.Orders.Commands.AddOrderItemsBulk;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Validators;

public sealed class AddOrderItemsBulkCommandValidator : AbstractValidator<AddOrderItemsBulkCommand>
{
    public AddOrderItemsBulkCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("OrderId is required.");

        RuleFor(x => x.Items)
            .NotNull().WithMessage("Items cannot be null.")
            .NotEmpty().WithMessage("At least one item must be provided.");

        RuleForEach(x => x.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            items.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            items.RuleFor(i => i.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("UnitPrice must not be negative.");
        });
    }
}
