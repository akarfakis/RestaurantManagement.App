using AutoMapper;
using RestaurantManagement.Api.Contracts;
using RestaurantManagement.Services.Dtos;

namespace RestaurantManagement.Api.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile() {
        CreateMap<PostOrderContract, OrderDto>();
        CreateMap<PostOrderItemContract, OrderItemDto>();
        CreateMap<PostMenuItemContract, MenuItemDto>();
        CreateMap<PostCustomerContract, CustomerDto>();
        CreateMap<PostDeliveryStaffContract, DeliveryStaffDto>();
    }       
}
