using RestaurantManagement.Db.Enums;
using RestaurantManagement.Db.Models;

namespace RestaurantManagement.Db.Repositories.Orders;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(Guid id);
    Task<List<Order>> GetByStatusAsync(OrderStatus status);
    Task<List<Order>> GetByCustomerIdAsync(Guid customerId);
    Task<Order> AddAsync(Order order);
    Task<Order> UpdateAsync(Order order);
    Task DeleteAsync(Guid id);
}
