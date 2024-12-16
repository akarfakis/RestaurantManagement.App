using RestaurantManagement.Db.Models;

namespace RestaurantManagement.Db.Repositories.Customers;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Customer> AddAsync(Customer customer);
    Task<Customer> UpdateAsync(Customer customer);
    Task DeleteAsync(Guid id);
}
