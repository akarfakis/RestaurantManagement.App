using AutoMapper;
using RestaurantManagement.Api.Contracts;
using RestaurantManagement.Services.Dtos;
using RestaurantManagement.Services.Services.Customers;

namespace RestaurantManagement.Api.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/customers", async (ICustomerService customerService) =>
        {
            var customers = await customerService.GetAllCustomersAsync();
            return Results.Ok(customers);
        });

        endpoints.MapGet("/customers/{id}", async (Guid id, ICustomerService customerService) =>
        {
            var customer = await customerService.GetCustomerByIdAsync(id);
            if (customer is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(customer);
        });

        endpoints.MapPost("/customers", async (PostCustomerContract customerContract, ICustomerService customerService, IMapper mapper) =>
        {
            var customerDto = mapper.Map<CustomerDto>(customerContract);
            var createdCustomer = await customerService.AddCustomerAsync(customerDto);
            return Results.Created($"/customers/{createdCustomer.Id}", createdCustomer);
        });

        endpoints.MapPut("/customers/{id}", async (Guid id, CustomerDto customerDto, ICustomerService customerService) =>
        {
            if (id != customerDto.Id)
            {
                return Results.BadRequest("Id mismatch.");
            }
            var updatedCustomer = await customerService.UpdateCustomerAsync(customerDto);
            return Results.Ok(updatedCustomer);
        });

        endpoints.MapDelete("/customers/{id}", async (Guid id, ICustomerService customerService) =>
        {
            await customerService.DeleteCustomerAsync(id);
            return Results.NoContent();
        });
    }
}
