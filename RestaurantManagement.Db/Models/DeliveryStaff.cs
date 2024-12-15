namespace RestaurantManagement.Db.Models;

public class DeliveryStaff : Person
{
    public ICollection<Order> AssignedOrders { get; set; } 
}
