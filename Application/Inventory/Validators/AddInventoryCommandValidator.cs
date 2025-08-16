using Application.Inventory.Commands.AddInventoryItem;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.Validators;

public class AddInventoryCommandValidator : AbstractValidator<AddInventoryItemCommand>
{
    public AddInventoryCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
    }
}