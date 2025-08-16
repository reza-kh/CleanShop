using Domain.Common;
using Domain.Orders.Entity;
using Domain.Orders.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public static class OrderStatusService
{
    public static OrderStatus Advance(Order order, string userId)
    {
        switch (order.Status)
        {
            case OrderStatus.Pending:
                order.Confirm();
                return OrderStatus.Confirmed;
            case OrderStatus.Confirmed:
                order.Ship();
                return OrderStatus.Shipped;
            case OrderStatus.Shipped:
                order.Deliver();
                return OrderStatus.Delivered;
            case OrderStatus.Delivered:
                throw new DomainException("Order is already delivered. Cannot advance further.");
            default:
                throw new DomainException("Invalid order status.");
        }
    }
}