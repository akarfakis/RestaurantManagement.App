using AutoMapper;
using RestaurantManagement.Api.Contracts;
using RestaurantManagement.Services.Dtos;
using RestaurantManagement.Services.Services.DeliveriesStaff;

namespace RestaurantManagement.Api.Endpoints;

public static class DeliveryStaffEndpoints
{
    public static void MapDeliveryStaffEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/delivery-staff", async (IDeliveryStaffService deliveryStaffService) =>
        {
            var staff = await deliveryStaffService.GetAllAsync();
            return Results.Ok(staff);
        });

        endpoints.MapGet("/delivery-staff/{id}", async (Guid id, IDeliveryStaffService deliveryStaffService) =>
        {
            var staff = await deliveryStaffService.GetByIdAsync(id);
            if (staff is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(staff);
        });

        endpoints.MapPost("/delivery-staff", async (PostDeliveryStaffContract staffContract, IDeliveryStaffService deliveryStaffService, IMapper mapper) =>
        {
            var staffDto = mapper.Map<DeliveryStaffDto>(staffContract);
            var createdStaff = await deliveryStaffService.AddAsync(staffDto);
            return Results.Created($"/delivery-staff/{createdStaff.Id}", createdStaff);
        });

        endpoints.MapPut("/delivery-staff/{id}", async (Guid id, DeliveryStaffDto staffDto, IDeliveryStaffService deliveryStaffService) =>
        {
            if (id != staffDto.Id)
            {
                return Results.BadRequest("Id mismatch.");
            }
            var updatedStaff = await deliveryStaffService.UpdateAsync(staffDto);
            return Results.Ok(updatedStaff);
        });

        endpoints.MapDelete("/delivery-staff/{id}", async (Guid id, IDeliveryStaffService deliveryStaffService) =>
        {
            await deliveryStaffService.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}