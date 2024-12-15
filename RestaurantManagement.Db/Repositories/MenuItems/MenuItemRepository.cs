using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Db.Models;
using RestaurantManagement.Db.SqlServer;

namespace RestaurantManagement.Db.Repositories.MenuItems;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly AppDbContext db;

    public MenuItemRepository(AppDbContext db)
    {
        this.db = db;
    }
    public Task<List<MenuItem>> GetAllAsync()
    {
        return db.MenuItems.ToListAsync();
    }

    public Task<List<MenuItem>> GetByCategoryAsync(string category)
    {
        return db.MenuItems.Where(mi => mi.Category.ToLower() == category.ToLower())
                           .ToListAsync();
    }

    public Task<MenuItem?> GetByIdAsync(Guid id)
    {
        return db.MenuItems.FirstOrDefaultAsync(mi => mi.Id == id);
    }

    public async Task<MenuItem> AddAsync(MenuItem menuItem)
    {
        await db.MenuItems.AddAsync(menuItem);
        await db.SaveChangesAsync();
        return menuItem;
    }

    public async Task<MenuItem> UpdateAsync(MenuItem menuItem)
    {
        db.MenuItems.Update(menuItem);
        await db.SaveChangesAsync();
        return menuItem;
    }

    public async Task DeleteAsync(Guid id)
    {
        var menuItem = await db.MenuItems.FindAsync(id);
        if (menuItem != null)
        {
            db.MenuItems.Remove(menuItem);
            await db.SaveChangesAsync();
        }
    }

    public Task<bool> Exists(Guid id)
    {
        return db.MenuItems.AnyAsync(mi => mi.Id == id);
    }

    public Task<List<MenuItem>> GetByIdsAsync(List<Guid> ids)
    {
        return db.MenuItems.Where(o=>ids.Contains(o.Id)).ToListAsync();
    }
}
