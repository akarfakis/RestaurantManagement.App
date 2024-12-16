using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Db.Repositories.MenuItems;
using RestaurantManagement.Services.Services.MenuItems;
using RestaurantManagement.Services.Validators.MenuItems;

namespace RestaurantManagement.Services.Services.Registrators;

public static class MenuItemServiceRegistrator
{
    public static void AddMenuItemServices(this IServiceCollection services)
    {
        services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<IMenuItemValidator, MenuItemValidator>();
    }
}
