using Domain.Common;
using Domain.Orders.Entity;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.OrderTests.Domain;

public class OrderItemTests
{
    private readonly string _userId = "test-user";

    [Fact]
    public void Create_OrderItem_Should_Have_CorrectValues()
    {
        // Arrange
        var productId = Guid.NewGuid();
        int quantity = 3;
        decimal unitPrice = 100;

        // Act
        var item = new OrderItem(productId, quantity, unitPrice, _userId);

        // Assert
        item.ProductId.Should().Be(productId);
        item.Quantity.Should().Be(quantity);
        item.UnitPrice.Should().Be(unitPrice);
        item.LineTotal.Should().Be(quantity * unitPrice);
    }

    [Fact]
    public void Increase_Should_AddQuantity()
    {
        var item = new OrderItem(Guid.NewGuid(), 2, 50, _userId);

        item.Increase(3);

        item.Quantity.Should().Be(5);
        item.LineTotal.Should().Be(5 * 50);
    }

    [Fact]
    public void RemoveItem_Should_DecreaseQuantity()
    {
        var item = new OrderItem(Guid.NewGuid(), 5, 20, _userId);

        // Remove 2 units
        item.RemoveItem(2);

        item.Quantity.Should().Be(3);
        item.LineTotal.Should().Be(3 * 20);
    }

    [Fact]
    public void Creating_WithInvalidQuantity_ShouldThrow()
    {
        Action act = () => new OrderItem(Guid.NewGuid(), 0, 10, _userId);

        act.Should().Throw<DomainException>().WithMessage("*quantity*");
    }

    [Fact]
    public void Creating_WithNegativeUnitPrice_ShouldThrow()
    {
        Action act = () => new OrderItem(Guid.NewGuid(), 1, -10, _userId);

        act.Should().Throw<DomainException>().WithMessage("*UnitPrice*");
    }
}

public class OrderBulkTests
{
    private readonly string _userId = "test-user";

    [Fact]
    public void AddingMultipleItems_Should_MergeDuplicates()
    {
        var productId = Guid.NewGuid();

        var order = Order.Create(Guid.NewGuid(), new[]
        {
                new OrderItem(productId, 2, 50, _userId)
            }, _userId);

        // Add bulk items including duplicate
        var bulkItems = new[]
        {
                new OrderItem(productId, 3, 50, _userId), // duplicate
                new OrderItem(Guid.NewGuid(), 1, 100, _userId)
            };

        foreach (var it in bulkItems)
        {
            order.AddItem(it.ProductId, it.Quantity, it.UnitPrice, _userId);
        }

        order.Items.Should().HaveCount(2); // duplicates merged
        order.Items.First(i => i.ProductId == productId).Quantity.Should().Be(5);
        order.TotalAmount.Should().Be(5 * 50 + 1 * 100);
    }
}