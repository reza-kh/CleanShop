using Domain.Common;
using Domain.Inventory.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products.Entity;

public sealed class Product : BaseEntity, IEntity
{
    public string Name { get; }
    public decimal Price { get; private set; }
    public InventoryItem InventoryItem { get; private set; }
    public Product() : base()
    {
    }
    public Product(string name, decimal price, string creatorUserId) : base(creatorUserId)
    {
        Guard.AgainstNullOrEmpty(name, nameof(name));
        Guard.AgainstNegative(price, nameof(price));

        Name = name;
        Price = price;
    }

    public void ChangePrice(decimal newPrice)
    {
        Guard.AgainstNegative(newPrice, nameof(newPrice));
        Price = newPrice;
    }
}
