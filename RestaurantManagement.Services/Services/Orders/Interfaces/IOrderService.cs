using RestaurantManagement.Db.Enums;
using RestaurantManagement.Services.Dtos;

namespace RestaurantManagement.Services.Services.Orders.Interfaces;

public interface IOrderService
{
    Task<List<OrderDto>> GetOrdersAsync();
    Task<OrderDto> GetOrderByIdAsync(Guid id);
    Task<List<OrderDto>> GetOrdersByStatusAsync(OrderStatus status);
    Task<List<OrderDto>> GetOrdersByCustomerAsync(Guid customerId);
    Task<OrderDto> AddOrderAsync(OrderDto orderDto);
    Task<OrderDto> UpdateOrderAsync(OrderDto orderDto);
    Task DeleteOrderAsync(Guid id);
    Task AdvanceOrderStatusAsync(Guid id);
    Task AssignOrderToDeliveryStaffAsync(Guid id, Guid deliveryStaffId);
    Task CancelOrderAsync(Guid id);
    Task UpdateOrderNotDeliveredAsync(Guid id);

}
