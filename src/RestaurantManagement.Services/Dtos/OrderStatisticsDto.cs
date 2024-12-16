namespace RestaurantManagement.Services.Dtos;

public class OrderStatisticsDto
{
    public DateOnly Date { get; set; }
    public int OrdersPlaced { get; set; }
    public double AverageOrderCompletionTimeInMinutes { get; set; }
    public int OrdersDelivered { get; set; }
    public int OrdersCancelled { get; set; }
    public int PickUpOrders { get; set; }
    public int DeliveryOrders { get; set; }
}
