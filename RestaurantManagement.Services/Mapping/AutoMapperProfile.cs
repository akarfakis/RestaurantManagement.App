using AutoMapper;
using RestaurantManagement.Db.Models;
using RestaurantManagement.Services.Dtos;
using RestaurantManagement.Common.Extensions;

namespace RestaurantManagement.Services.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.StatusDescription, opt => opt.MapFrom(src => src.Status.GetDescription()))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ReverseMap();

        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<MenuItem, MenuItemDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
    }
}
