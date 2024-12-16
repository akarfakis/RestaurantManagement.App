using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Db.Repositories.DeliveriesStaff;
using RestaurantManagement.Services.Services.DeliveriesStaff;

namespace RestaurantManagement.Services.Services.Registrators;

public static class DeliveryStaffServicesRegistrator
{
    public static void AddDeliveryStaffServices(this IServiceCollection services)
    {
        services.AddScoped<IDeliveryStaffRepository, DeliveryStaffRepository>();
        services.AddScoped<IDeliveryStaffService, DeliveryStaffService>();
    }
}
