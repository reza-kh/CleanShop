using Domain.Common;
using Domain.Products.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Inventory.Entity;
public sealed class InventoryItem:BaseEntity, IEntity
{
    public Guid ProductId { get; }
    public int Quantity { get; private set; }
    public Product Product { get; private set; }

    public InventoryItem() : base()
    {
    }

    public InventoryItem(Guid productId, int quantity, string creatorUserId) : base(creatorUserId)
    {
        if (productId == Guid.Empty) throw new DomainException("ProductId is required.");
        if (quantity < 0) throw new DomainException("Quantity must be >= 0.");

        ProductId = productId;
        Quantity = quantity;
    }

    public bool HasEnough(int required) => Quantity >= required;

    public void Decrease(int amount)
    {
        if (amount <= 0) throw new DomainException("Decrease amount must be > 0.");
        if (amount > Quantity) throw new DomainException("Insufficient inventory.");
        Quantity -= amount;
    }

    public void Increase(int amount)
    {
        if (amount <= 0) throw new DomainException("Increase amount must be > 0.");
        Quantity += amount;
    }
}
