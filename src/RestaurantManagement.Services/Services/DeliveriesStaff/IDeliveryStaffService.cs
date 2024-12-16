using RestaurantManagement.Services.Dtos;

namespace RestaurantManagement.Services.Services.DeliveriesStaff;

public interface IDeliveryStaffService
{
    Task<List<DeliveryStaffDto>> GetAllAsync();
    Task<DeliveryStaffDto?> GetByIdAsync(Guid id);
    Task<DeliveryStaffDto> AddAsync(DeliveryStaffDto DeliveryStaffDto);
    Task<DeliveryStaffDto> UpdateAsync(DeliveryStaffDto deliveryStaff);
    Task DeleteAsync(Guid id);
}
