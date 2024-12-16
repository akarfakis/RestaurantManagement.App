using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Db.Models;
using RestaurantManagement.Db.SqlServer;

namespace RestaurantManagement.Db.Repositories.Customers;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext db;

    public CustomerRepository(AppDbContext db)
    {
        this.db = db;
    }

    public Task<List<Customer>> GetAllAsync()
    {
        return db.Customers.ToListAsync();
    }

    public Task<Customer?> GetByIdAsync(Guid id)
    {
        return db.Customers.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Customer> AddAsync(Customer customer)
    {
        await db.Customers.AddAsync(customer);
        await db.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        db.Customers.Update(customer);
        await db.SaveChangesAsync();
        return customer;
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await GetByIdAsync(id);
        if (customer != null)
        {
            db.Customers.Remove(customer);
            await db.SaveChangesAsync();
        }
    }
}