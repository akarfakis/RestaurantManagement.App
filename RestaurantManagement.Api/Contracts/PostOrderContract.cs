using RestaurantManagement.Db.Enums;

namespace RestaurantManagement.Api.Contracts;

public class PostOrderContract
{
    public Guid CustomerId { get; set; } 
    public OrderType OrderType { get; set; }
    public string? DeliveryAddress { get; set; } 
    public string? SpecialInstructions { get; set; }
    public List<PostOrderItemContract> OrderItems { get; set; } = new();
}
