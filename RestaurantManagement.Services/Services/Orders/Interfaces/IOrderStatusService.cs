using RestaurantManagement.Db.Enums;

namespace RestaurantManagement.Services.Services.Orders.Interfaces;

public interface IOrderStatusService
{
    OrderStatus GetNextOrderStatus(OrderStatus currentStatus, OrderType orderType);
}
