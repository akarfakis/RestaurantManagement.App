using RestaurantManagement.Db.Enums;
using RestaurantManagement.Db.Repositories.Orders;
using RestaurantManagement.Services.Dtos;
using RestaurantManagement.Services.Services.Orders.Interfaces;

namespace RestaurantManagement.Services.Services.Orders;

public class OrderStatisticsService : IOrderStatisticsService
{
    private readonly IOrderRepository _orderRepository;

    public OrderStatisticsService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderStatisticsDto>> GetOrderStatisticsAsync()
    {
        var orders = await _orderRepository.GetAllAsync();

        var statistics = orders
            .GroupBy(o => o.OrderDate.Date)
            .Select(g => new OrderStatisticsDto
            {
                Date = DateOnly.FromDateTime(g.Key),
                OrdersPlaced = g.Count(),
                AverageOrderCompletionTimeInMinutes = g.Where(o => o.DeliveredDate.HasValue)
                                                    .Average(o => (o.DeliveredDate.Value - o.OrderDate).TotalMinutes),
                OrdersDelivered = g.Count(o => o.Status == OrderStatus.Delivered),
                OrdersCancelled = g.Count(o => o.Status == OrderStatus.Cancelled),
                DeliveryOrders = g.Count(o=> o.OrderType == OrderType.Delivery),
                PickUpOrders = g.Count(o=> o.OrderType == OrderType.Pickup),
            })
            .OrderByDescending(s => s.Date)
            .ToList();

        return statistics;
    }
}
