using AutoMapper;
using RestaurantManagement.Db.Models;
using RestaurantManagement.Db.Repositories.Customers;
using RestaurantManagement.Services.Dtos;

namespace RestaurantManagement.Services.Services.Customers;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<List<CustomerDto>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return _mapper.Map<List<CustomerDto>>(customers);
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return _mapper.Map<CustomerDto?>(customer);
    }

    public async Task<CustomerDto> AddCustomerAsync(CustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        var addedCustomer = await _customerRepository.AddAsync(customer);
        return _mapper.Map<CustomerDto>(addedCustomer);
    }

    public async Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        var updatedCustomer = await _customerRepository.UpdateAsync(customer);
        return _mapper.Map<CustomerDto>(updatedCustomer);
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
        await _customerRepository.DeleteAsync(id);
    }
}