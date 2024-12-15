namespace RestaurantManagement.Db.Models;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid MenuItemId { get; set; }
    public int Quantity { get; set; }
    public string? SpecialInstructions { get; set; }

    public virtual Order Order { get; set; }
    public virtual MenuItem MenuItem { get; set; }
}
