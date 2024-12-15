using RestaurantManagement.Db.Enums;

namespace RestaurantManagement.Db.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? AssignedDeliveryStaffId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderType OrderType { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime? DeliveredDate { get; set; }
    public DateTime? CancelledDate { get; set; }
    public string? DeliveryAddress { get; set; }
    public string? SpecialInstructions { get; set; }
    public decimal TotalAmount { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
    public virtual ICollection<OrderLog> OrderLogs { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual DeliveryStaff AssignedDeliveryStaff { get; set; }
}
