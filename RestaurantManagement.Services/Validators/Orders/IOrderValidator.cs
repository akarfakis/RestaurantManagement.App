using RestaurantManagement.Db.Models;

namespace RestaurantManagement.Services.Validators.Orders;

public interface IOrderValidator
{
    void OrderCanBeCancelled(Order order);

    void OrderCanAdvanceStatus(Order order);
    void OrderCanBeAssigned(Order order);
    void Validate(Order order);
    void OrderCanBeNotDelivered(Order order);
}
