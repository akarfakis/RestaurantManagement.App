using RestaurantManagement.Db.Models;

namespace RestaurantManagement.Db.Repositories.MenuItems;

public interface IMenuItemRepository
{
    Task<bool> Exists(Guid id);
    Task<List<MenuItem>> GetAllAsync();
    Task<List<MenuItem>> GetByIdsAsync(List<Guid> ids);
    Task<List<MenuItem>> GetByCategoryAsync(string category);
    Task<MenuItem?> GetByIdAsync(Guid id);
    Task<MenuItem> AddAsync(MenuItem menuItem);
    Task<MenuItem> UpdateAsync(MenuItem menuItem);
    Task DeleteAsync(Guid id);
}