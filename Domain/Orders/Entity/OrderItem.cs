using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Orders.Entity
{
    public sealed class OrderItem:BaseEntity
    {
        public Guid ProductId { get; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; }
        public decimal LineTotal => UnitPrice * Quantity;

        public OrderItem():base()
        {
            
        }


        public OrderItem(Guid productId, int quantity, decimal unitPrice, string creatorUserId) : base(creatorUserId)
        {
            if (productId == Guid.Empty) throw new DomainException("ProductId is required.");
            Guard.AgainstOutOfRange(quantity, 1, nameof(quantity));
            Guard.AgainstNegative(unitPrice, nameof(unitPrice));

            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void Increase(int quantity)
        {
            Guard.AgainstOutOfRange(quantity, 1, nameof(quantity));
            Quantity += quantity;
        }


        public void RemoveItem(int quantity)
        {
            Guard.AgainstOutOfRange(quantity, 1, nameof(quantity));
            Quantity -= quantity;

            SetLastModified(CreatorUserId);
        }
    }
}
