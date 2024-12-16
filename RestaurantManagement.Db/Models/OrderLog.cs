using RestaurantManagement.Db.Enums;

namespace RestaurantManagement.Db.Models;

public class OrderLog
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime Timestamp { get; set; }

    public virtual Order Order { get; set; }
}
