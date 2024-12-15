using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Db.Enums;
using RestaurantManagement.Db.Models;
using RestaurantManagement.Db.SqlServer;

namespace RestaurantManagement.Db.Repositories.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext db;

    public OrderRepository(AppDbContext db)
    {
        this.db = db;
    }

    public Task<List<Order>> GetAllAsync()
    {
        return db.Orders.Include(o => o.Customer)
                        .Include(o => o.OrderItems)
                        .ToListAsync();
    }

    public Task<Order?> GetByIdAsync(Guid id)
    {
        return db.Orders.Include(o => o.Customer)
                        .Include(o => o.OrderItems)
                        .FirstOrDefaultAsync(o => o.Id == id);
    }

    public Task<List<Order>> GetByStatusAsync(OrderStatus status)
    {
        return db.Orders.Where(o => o.Status == status)
                        .Include(o => o.Customer)
                        .Include(o => o.OrderItems)
                        .ToListAsync();
    }

    public Task<List<Order>> GetByCustomerIdAsync(Guid CustomerId)
    {
        return db.Orders.Where(o => o.CustomerId == CustomerId)
                        .Include(o => o.Customer)
                        .Include(o => o.OrderItems)
                        .ToListAsync();
    }
    public async Task<Order> AddAsync(Order order)
    {
        await AddOrderLog(order);
        await db.Orders.AddAsync(order);
        await db.SaveChangesAsync();
        return order;
    }

    private async Task AddOrderLog(Order order)
    {
        var orderLog = new OrderLog
        {
            OrderId = order.Id,
            Status = order.Status,
            Timestamp = DateTime.UtcNow
        };

        await db.OrderLogs.AddAsync(orderLog);
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        var entries = db.ChangeTracker.Entries<Order>()
            .Where(e => e.State == EntityState.Modified && e.Property(p => p.Status).IsModified)
            .ToList();
        if (db.Entry(order).Property(p => p.Status).IsModified)
        {
            await AddOrderLog(order);
        }
        
        db.Orders.Update(order);
        await db.SaveChangesAsync();
        return order;
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await db.Orders.FindAsync(id);
        if (order != null)
        {
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
        }
    }
}


