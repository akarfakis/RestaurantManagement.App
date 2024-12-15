using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Db.Repositories.Customers;
using RestaurantManagement.Services.Services.Customers;

namespace RestaurantManagement.Services.Services.Registrators;

public static class CustomerServicesRegistrator
{
    public static void AddCustomerServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerService, CustomerService>();
    }
}
