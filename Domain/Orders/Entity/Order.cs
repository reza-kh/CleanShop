using Domain.Common;
using Domain.Orders.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Orders.Entity;

public sealed class Order : BaseEntity
{
    public Guid CustomerId { get; }
    public OrderStatus Status { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    private readonly List<OrderItem> _items = new();
    public decimal TotalAmount => _items.Sum(i => i.LineTotal);

    public Order() : base()
    { 
    }

    private Order( Guid customerId, IEnumerable<OrderItem> items, string creatorUserId) : base(creatorUserId)
    {
        if (customerId == Guid.Empty) throw new DomainException("CustomerId is required.");
        CustomerId = customerId;
        Status = OrderStatus.Pending;

        AddItems(items);
        EnsureHasAtLeastOneItem();
    }

    public static Order Create(Guid customerId, IEnumerable<OrderItem> items, string creatorUserId)
            => new(customerId, items, creatorUserId);

    public void AddItem(Guid productId, int quantity, decimal unitPrice, string creatorUserId)
    {
        EnsureEditableInPending();

        // Validation happens inside OrderItem ctor and Increase
        var existing = _items.FirstOrDefault(i => i.ProductId == productId);
        if (existing is null)
        {
            _items.Add(new OrderItem(productId, quantity, unitPrice, creatorUserId));
        }
        else
        {
            existing.Increase(quantity);
        }
    }

    public void Confirm()
    {
        SetStatus(OrderStatus.Confirmed);
    }

    public void Ship()
    {
        SetStatus(OrderStatus.Shipped);
    }

    public void Deliver()
    {
        SetStatus(OrderStatus.Delivered);
    }

    private void SetStatus(OrderStatus target)
    {
        // only forward one step: Pending→Confirmed→Shipped→Delivered
        var delta = (int)target - (int)Status;
        if (delta != 1)
            throw new DomainException("Invalid status transition.");
        Status = target;
    }

    private void EnsureEditableInPending()
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException("Order items can only be changed while status is Pending.");
    }

    private void AddItems(IEnumerable<OrderItem> items)
    {
        Guard.AgainstNull(items, nameof(items));
        foreach (var it in items)
        {
            // Merge duplicates by ProductId
            var existing = _items.FirstOrDefault(i => i.ProductId == it.ProductId);
            if (existing is null) _items.Add(new OrderItem(it.ProductId, it.Quantity, it.UnitPrice,it.CreatorUserId));
            else existing.Increase(it.Quantity);
        }
    }

    private void EnsureHasAtLeastOneItem()
    {
        if (_items.Count == 0)
            throw new DomainException("Order must have at least one item.");
    }

}