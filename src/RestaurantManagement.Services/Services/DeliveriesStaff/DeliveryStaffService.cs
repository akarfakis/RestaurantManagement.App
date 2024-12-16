using AutoMapper;
using RestaurantManagement.Db.Models;
using RestaurantManagement.Db.Repositories.DeliveriesStaff;
using RestaurantManagement.Services.Dtos;

namespace RestaurantManagement.Services.Services.DeliveriesStaff;

public class DeliveryStaffService : IDeliveryStaffService
{
    private readonly IDeliveryStaffRepository _deliveryStaffRepository;
    private readonly IMapper _mapper;

    public DeliveryStaffService(IDeliveryStaffRepository deliveryStaffRepositor,IMapper mapper)
    {
        _deliveryStaffRepository = deliveryStaffRepositor;
        _mapper = mapper;
    }

    public async Task<List<DeliveryStaffDto>> GetAllAsync()
    {
        var staff = await _deliveryStaffRepository.GetAllAsync();
        return _mapper.Map<List<DeliveryStaffDto>>(staff);
    }

    public async Task<DeliveryStaffDto?> GetByIdAsync(Guid id)
    {
        var staff = await _deliveryStaffRepository.GetByIdAsync(id);

        return _mapper.Map<DeliveryStaffDto>(staff);
    }

    public async Task<DeliveryStaffDto> AddAsync(DeliveryStaffDto deliveryStaffDto)
    {
        var staff = _mapper.Map<DeliveryStaff>(deliveryStaffDto);
        var addedStaff = await _deliveryStaffRepository.AddAsync(staff);
        return _mapper.Map<DeliveryStaffDto>(addedStaff);

    }

    public async Task<DeliveryStaffDto> UpdateAsync(DeliveryStaffDto deliveryStaffDto)
    {
        var staff = _mapper.Map<DeliveryStaff>(deliveryStaffDto);
        var updatedStaff = await _deliveryStaffRepository.UpdateAsync(staff);
        return _mapper.Map<DeliveryStaffDto>(updatedStaff);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _deliveryStaffRepository.DeleteAsync(id);
    }
}
