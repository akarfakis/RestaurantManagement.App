using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Db.Repositories.Orders;
using RestaurantManagement.Services.Services.Orders;
using RestaurantManagement.Services.Services.Orders.Interfaces;
using RestaurantManagement.Services.Validators.Orders;

namespace RestaurantManagement.Services.Services.Registrators;

public static class OrderServicesRegistrator
{
    public static void AddOrderServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderValidator, OrderValidator>();
        services.AddScoped<IOrderStatusService, OrderStatusService>();
        services.AddScoped<IOrderCalculatorService, OrderCalculatorService>();
        services.AddScoped<IOrderStatisticsService, OrderStatisticsService>();
    }
}
