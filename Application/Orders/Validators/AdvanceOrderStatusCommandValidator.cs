using Application.Orders.Commands.AdvanceOrderStatus;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Validators;

public sealed class AdvanceOrderStatusCommandValidator : AbstractValidator<AdvanceOrderStatusCommand>
{
    public AdvanceOrderStatusCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("OrderId is required.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");
    }
}