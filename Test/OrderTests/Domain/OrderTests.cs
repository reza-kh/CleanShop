using Domain.Common;
using Domain.Orders.Entity;
using Domain.Orders.Enum;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.OrderTests.Domain;

public class OrderTests
{
    private readonly string _creatorUserId = "test-user";

    [Fact]
    public void CreateOrder_WithValidItems_ShouldSucceed()
    {
        var items = new[]
        {
                new OrderItem(Guid.NewGuid(), 2, 10m, _creatorUserId),
                new OrderItem(Guid.NewGuid(), 1, 20m, _creatorUserId)
        };
        var customerId = Guid.NewGuid();

        var order = Order.Create(customerId, items, _creatorUserId);

        order.CustomerId.Should().Be(customerId);
        order.Items.Should().HaveCount(2);
        order.TotalAmount.Should().Be(40m); // 2*10 + 1*20
        order.Status.Should().Be(OrderStatus.Pending);
    }



    [Fact]
    public void CreateOrder_WithoutItems_ShouldThrowDomainException()
    {
        var customerId = Guid.NewGuid();

        Action act = () => Order.Create(customerId, Array.Empty<OrderItem>(), _creatorUserId);

        act.Should().Throw<DomainException>()
           .WithMessage("Order must have at least one item.");
    }

    [Fact]
    public void AddItem_NewProduct_ShouldIncreaseItemsCount()
    {
        var customerId = Guid.NewGuid();
        var order = Order.Create(customerId, new[] { new OrderItem(Guid.NewGuid(), 1, 10m, _creatorUserId) }, _creatorUserId);

        var newProductId = Guid.NewGuid();
        order.AddItem(newProductId, 3, 5m, _creatorUserId);

        order.Items.Should().HaveCount(2);
        order.Items.First(i => i.ProductId == newProductId).Quantity.Should().Be(3);
    }

    [Fact]
    public void AddItem_ExistingProduct_ShouldIncreaseQuantity()
    {
        var productId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var order = Order.Create(customerId, new[] { new OrderItem(productId, 2, 10m, _creatorUserId) }, _creatorUserId);

        order.AddItem(productId, 3, 10m, _creatorUserId);

        order.Items.Should().HaveCount(1);
        order.Items.First(i => i.ProductId == productId).Quantity.Should().Be(5);
    }

    [Fact]
    public void ConfirmOrder_ShouldChangeStatusToConfirmed()
    {
        var customerId = Guid.NewGuid();
        var order = Order.Create(customerId, new[] { new OrderItem(Guid.NewGuid(), 1, 10m, _creatorUserId) }, _creatorUserId);

        order.Confirm();

        order.Status.Should().Be(OrderStatus.Confirmed);
    }

    [Fact]
    public void InvalidStatusTransition_ShouldThrowException()
    {
        var customerId = Guid.NewGuid();
        var order = Order.Create(customerId, new[] { new OrderItem(Guid.NewGuid(), 1, 10m, _creatorUserId) }, _creatorUserId);

        Action act = () => order.Ship(); // cannot skip Confirm

        act.Should().Throw<DomainException>()
           .WithMessage("Invalid status transition.");
    }

    [Fact]
    public void CannotAddItem_WhenOrderIsNotPending_ShouldThrowException()
    {
        var customerId = Guid.NewGuid();
        var order = Order.Create(customerId, new[] { new OrderItem(Guid.NewGuid(), 1, 10m, _creatorUserId) }, _creatorUserId);

        order.Confirm();

        var newProductId = Guid.NewGuid();
        Action act = () => order.AddItem(newProductId, 1, 5m, _creatorUserId);

        act.Should().Throw<DomainException>()
           .WithMessage("Order items can only be changed while status is Pending.");
    }
}