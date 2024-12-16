using RestaurantManagement.Db.Enums;
using RestaurantManagement.Services.Services.Orders;
using RestaurantManagement.Services.Services.Orders.Interfaces;

namespace RestaurantManagement.Services.Tests.Tests.Services.Orders;

public class OrderStatusServiceTests
{
    private readonly IOrderStatusService _orderStatusService;

    public OrderStatusServiceTests()
    {
        _orderStatusService = new OrderStatusService(); 
    }

    [Theory]
    [InlineData(OrderStatus.Pending, OrderType.Pickup, OrderStatus.Preparing)]
    [InlineData(OrderStatus.Preparing, OrderType.Pickup, OrderStatus.ReadyForPickup)] 
    [InlineData(OrderStatus.ReadyForPickup, OrderType.Pickup, OrderStatus.Delivered)]

    [InlineData(OrderStatus.Pending, OrderType.Delivery, OrderStatus.Preparing)]
    [InlineData(OrderStatus.Preparing, OrderType.Delivery, OrderStatus.ReadyForDelivery)]
    [InlineData(OrderStatus.ReadyForDelivery, OrderType.Delivery, OrderStatus.OutForDelivery)]
    [InlineData(OrderStatus.OutForDelivery, OrderType.Delivery, OrderStatus.Delivered)]
    public void GetNextOrderStatus_ShouldReturnCorrectStatus_WhenValidTransition(OrderStatus currentStatus, OrderType orderType, OrderStatus expectedNextStatus)
    {
        // Act
        var nextStatus = _orderStatusService.GetNextOrderStatus(currentStatus, orderType);

        // Assert
        Assert.Equal(expectedNextStatus, nextStatus);
    }

    [Theory]
    [InlineData(OrderStatus.Pending, (OrderType)999)] 
    [InlineData(OrderStatus.Preparing, (OrderType)999)] 
    [InlineData(OrderStatus.ReadyForPickup, (OrderType)999)] 
    [InlineData(OrderStatus.ReadyForDelivery, (OrderType)999)] 
    public void GetNextOrderStatus_ShouldThrowArgumentOutOfRangeException_WhenInvalidOrderType(OrderStatus currentStatus, OrderType invalidOrderType)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _orderStatusService.GetNextOrderStatus(currentStatus, invalidOrderType));
        Assert.Equal("Invalid OrderType (Parameter 'orderType')", exception.Message);
    }

    [Theory]
    [InlineData(OrderStatus.Delivered, OrderType.Pickup)] 
    [InlineData(OrderStatus.Delivered, OrderType.Delivery)]
    [InlineData(OrderStatus.Cancelled, OrderType.Pickup)]
    [InlineData(OrderStatus.Cancelled, OrderType.Delivery)]
    [InlineData(OrderStatus.OutForDelivery, OrderType.Pickup)]
    [InlineData(OrderStatus.ReadyForDelivery, OrderType.Pickup)]
    [InlineData(OrderStatus.ReadyForPickup, OrderType.Delivery)]
    public void GetNextOrderStatus_ShouldThrowArgumentOutOfRangeException_WhenInvalidStatusTransition(OrderStatus currentStatus, OrderType orderType)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _orderStatusService.GetNextOrderStatus(currentStatus, orderType));
        Assert.Equal("Invalid OrderStatus (Parameter 'currentStatus')", exception.Message);
    }

}

