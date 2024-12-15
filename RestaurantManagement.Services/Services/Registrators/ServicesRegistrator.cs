using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Services.Mapping;

namespace RestaurantManagement.Services.Services.Registrators;

public static class ServicesRegistrator
{
    public static void AddServices(this IServiceCollection services)
    {

        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        services.AddOrderServices();
        services.AddMenuItemServices();
        services.AddCustomerServices();
    }
}
