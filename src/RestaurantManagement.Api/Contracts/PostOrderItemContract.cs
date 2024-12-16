namespace RestaurantManagement.Api.Contracts;

public class PostOrderItemContract
{
    public Guid MenuItemId { get; set; }
    public int Quantity { get; set; }
    public string? SpecialInstructions { get; set; }
}
