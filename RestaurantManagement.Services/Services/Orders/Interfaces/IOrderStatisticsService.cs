using RestaurantManagement.Services.Dtos;

namespace RestaurantManagement.Services.Services.Orders.Interfaces;

public interface IOrderStatisticsService
{
    Task<List<OrderStatisticsDto>> GetOrderStatisticsAsync();
}
