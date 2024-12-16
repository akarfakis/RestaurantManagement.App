using RestaurantManagement.Db.Models;
using RestaurantManagement.Services.Dtos;

namespace RestaurantManagement.Services.Services.Customers;

public interface ICustomerService
{
    Task<List<CustomerDto>> GetAllCustomersAsync();
    Task<CustomerDto?> GetCustomerByIdAsync(Guid id);
    Task<CustomerDto> AddCustomerAsync(CustomerDto customerDto);
    Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto);
    Task DeleteCustomerAsync(Guid id);
}