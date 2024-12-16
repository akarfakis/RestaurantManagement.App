using RestaurantManagement.Db.Models;
namespace RestaurantManagement.Db.Repositories.DeliveriesStaff;

public interface IDeliveryStaffRepository
{
    Task<List<DeliveryStaff>> GetAllAsync();
    Task<DeliveryStaff?> GetByIdAsync(Guid id);
    Task<DeliveryStaff> AddAsync(DeliveryStaff deliveryStaff);
    Task<DeliveryStaff> UpdateAsync(DeliveryStaff deliveryStaff);
    Task DeleteAsync(Guid id);
}
