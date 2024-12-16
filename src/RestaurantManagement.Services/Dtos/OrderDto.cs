using RestaurantManagement.Db.Enums;

namespace RestaurantManagement.Services.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid AssignedDeliveryStaffId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public OrderType OrderType { get; set; }
    public string StatusDescription { get; set; }
    public string? DeliveryAddress { get; set; }
    public string? SpecialInstructions { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
}
