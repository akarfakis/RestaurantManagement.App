using RestaurantManagement.Common.Exceptions;
using RestaurantManagement.Db.Enums;
using RestaurantManagement.Db.Models;

namespace RestaurantManagement.Services.Validators.Orders;

public class OrderValidator : IOrderValidator
{
    public void Validate(Order order)
    {
        var errors = new List<string>();
        if (order is null) throw new InvalidInputException("Order is required.", 400);

        if (order.CustomerId == Guid.Empty) errors.Add("CustomerId is required.");
        if (!Enum.IsDefined(order.Status)) errors.Add("Invalid order status.");

        if (order.OrderType == OrderType.Delivery)
        {
            if (string.IsNullOrWhiteSpace(order.DeliveryAddress)) errors.Add("Delivery address is required for delivery orders.");
        }
        else if (order.OrderType == OrderType.Pickup)
        {
            if (order.AssignedDeliveryStaffId.HasValue || order.AssignedDeliveryStaff is not null) errors.Add("Cannot assign delivery staff on pickup orders.");
        }

        if (order.TotalAmount <= 0) errors.Add("Invalid total amount.");
        if (order.OrderItems.Any())
        {
            if (order.OrderItems.Any(s => s.Quantity <= 0)) errors.Add("Order items need to have quantity > 0.");
            if (order.OrderItems.Any(s => s.MenuItemId == Guid.Empty && s.MenuItem is null)) errors.Add("Menu item is required.");
            if (order.OrderItems.Any(s => s.OrderId != order.Id)) errors.Add("Invalid orderId in order items.");
            if (order.OrderItems.Select(s => s.MenuItemId).Distinct().Count() != order.OrderItems.Count) errors.Add("Duplicate menuitem id on order items.");
        }
        else
        {
            errors.Add("Order items are required.");
        }

        if (errors.Any()) throw new InvalidInputException(String.Join(", ", errors), 400);
    }
    public void OrderCanBeCancelled(Order order)
    {
        if (order.Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException("Can only cancel orders in 'Pending' status.");
        }
    }

    public void OrderCanAdvanceStatus(Order order)
    {
        if (order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Cancelled)
        {
            throw new InvalidOperationException("Order has already been completed or cancelled and cannot be advanced.");
        }
    }

    public void OrderCanBeAssigned(Order order)
    {
        if (order.Status != OrderStatus.ReadyForDelivery || order.OrderType != OrderType.Delivery)
        {
            throw new InvalidOperationException("Order has already been completed or cancelled and cannot be advanced.");
        }
    }

    public void OrderCanBeNotDelivered(Order order)
    {
        if (order.Status != OrderStatus.OutForDelivery)
        {
            throw new InvalidOperationException("Order should be in OutForDelivery to be marked as UnableToDeliver.");
        }
    }
}
