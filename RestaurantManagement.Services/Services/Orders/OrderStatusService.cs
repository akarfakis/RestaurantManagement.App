using RestaurantManagement.Db.Enums;
using RestaurantManagement.Services.Services.Orders.Interfaces;

namespace RestaurantManagement.Services.Services.Orders;

public class OrderStatusService : IOrderStatusService
{
    public OrderStatus GetNextOrderStatus(OrderStatus currentStatus, OrderType orderType)
    {
        switch (orderType)
        {
            case OrderType.Pickup:
                return NextPickUpOrderStatus(currentStatus);
            case OrderType.Delivery:
                return NextDeliveryOrderStatus(currentStatus);
            default:
                throw new ArgumentOutOfRangeException(nameof(orderType), "Invalid OrderType");

        }     
    }

    private OrderStatus NextDeliveryOrderStatus(OrderStatus currentStatus)
    {
        switch (currentStatus)
        {
            case OrderStatus.Pending:
                return OrderStatus.Preparing;
            case OrderStatus.Preparing:
                return OrderStatus.ReadyForDelivery;
            case OrderStatus.ReadyForDelivery:
                return OrderStatus.OutForDelivery;
            case OrderStatus.OutForDelivery:
                return OrderStatus.Delivered;
            default:
                throw new ArgumentOutOfRangeException(nameof(currentStatus), "Invalid OrderStatus");
        }
    }

    private OrderStatus NextPickUpOrderStatus(OrderStatus currentStatus)
    {
        switch (currentStatus)
        {
            case OrderStatus.Pending:
                return OrderStatus.Preparing;
            case OrderStatus.Preparing:
                return OrderStatus.ReadyForPickup;
            case OrderStatus.ReadyForPickup:
                return OrderStatus.Delivered;
            default:
                throw new ArgumentOutOfRangeException(nameof(currentStatus), "Invalid OrderStatus");
        }
    }
}
