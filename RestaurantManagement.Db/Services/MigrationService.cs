using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Db.SqlServer;

namespace RestaurantManagement.Db.Services;

public class MigrationService : IMigrationService
{
    private readonly AppDbContext db;
    public MigrationService(AppDbContext db)
    {
         this.db = db;
    }
    public Task UpdateSchemaAsync() => db.Database.MigrateAsync();
}
