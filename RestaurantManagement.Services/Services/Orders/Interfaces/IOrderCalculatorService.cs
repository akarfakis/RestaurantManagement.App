using RestaurantManagement.Db.Models;

namespace RestaurantManagement.Services.Services.Orders.Interfaces;

public interface IOrderCalculatorService
{
    Task<decimal> CalculateTotalAmountAsync(List<OrderItem> orderItems);
}
