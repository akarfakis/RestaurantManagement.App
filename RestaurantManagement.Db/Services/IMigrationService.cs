namespace RestaurantManagement.Db.Services;

public interface IMigrationService
{
    Task UpdateSchemaAsync();
}
