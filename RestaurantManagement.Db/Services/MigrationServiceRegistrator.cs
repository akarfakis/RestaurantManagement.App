using Microsoft.Extensions.DependencyInjection;

namespace RestaurantManagement.Db.Services;

public static class MigrationServiceRegistrator
{

    public static void AddMigrationService(this IServiceCollection services)
    {
        services.AddScoped<IMigrationService, MigrationService>();
    }
}
