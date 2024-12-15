using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Db.Models;
using RestaurantManagement.Db.SqlServer;

namespace RestaurantManagement.Db.Repositories.DeliveriesStaff;

public class DeliveryStaffRepository : IDeliveryStaffRepository
{
    private readonly AppDbContext db;

    public DeliveryStaffRepository(AppDbContext db)
    {
        this.db = db;
    }

    public async Task<List<DeliveryStaff>> GetAllAsync()
    {
        return await db.DeliveryStaff.Include(ds => ds.AssignedOrders)
                                     .ToListAsync();
    }

    public async Task<DeliveryStaff?> GetByIdAsync(Guid id)
    {
        return await db.DeliveryStaff.Include(ds => ds.AssignedOrders)
                                     .FirstOrDefaultAsync(ds => ds.Id == id);
    }

    public async Task<DeliveryStaff> AddAsync(DeliveryStaff deliveryStaff)
    {
        await db.DeliveryStaff.AddAsync(deliveryStaff);
        await db.SaveChangesAsync();
        return deliveryStaff;
    }

    public async Task<DeliveryStaff> UpdateAsync(DeliveryStaff deliveryStaff)
    {
        db.DeliveryStaff.Update(deliveryStaff);
        await db.SaveChangesAsync();
        return deliveryStaff;
    }

    public async Task DeleteAsync(Guid id)
    {
        var deliveryStaff = await db.DeliveryStaff.FindAsync(id);
        if (deliveryStaff != null)
        {
            db.DeliveryStaff.Remove(deliveryStaff);
            await db.SaveChangesAsync();
        }
    }
}
