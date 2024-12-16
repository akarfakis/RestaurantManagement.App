using RestaurantManagement.Common.Exceptions;
using RestaurantManagement.Db.Enums;
using RestaurantManagement.Db.Models;
using RestaurantManagement.Services.Validators.Orders;

namespace RestaurantManagement.Services.Tests.Tests.Services.Orders;

public class OrderValidatorTests
{
    private readonly IOrderValidator _orderValidator;
    public OrderValidatorTests()
    {
        _orderValidator = new OrderValidator();
    }
    [Fact]
    public void Validate_ShouldNotThrowException_WhenOrderIsValid()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new Order
        {
            Id = orderId,
            CustomerId = Guid.NewGuid(),
            Status = OrderStatus.Pending,
            OrderType = OrderType.Delivery,
            DeliveryAddress = "123 Street",
            TotalAmount = 100,
            OrderItems = new List<OrderItem>
        {
            new OrderItem { Id = Guid.NewGuid(), Quantity = 2, MenuItemId = Guid.NewGuid(), OrderId = orderId }
        }
        };



        // Act & Assert
        var exception = Record.Exception(() => _orderValidator.Validate(order));
        Assert.Null(exception);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenCustomerIdIsEmpty()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new Order
        {
            Id = orderId,
            CustomerId = Guid.Empty, // Invalid
            Status = OrderStatus.Pending,
            OrderType = OrderType.Delivery,
            DeliveryAddress = "123 Street",
            TotalAmount = 100,
            OrderItems = new List<OrderItem>
        {
            new OrderItem { Id = Guid.NewGuid(), Quantity = 2, MenuItemId = Guid.NewGuid(), OrderId=orderId }
        }
        };

        // Act & Assert
        var exception = Assert.Throws<InvalidInputException>(() => _orderValidator.Validate(order));
        Assert.Contains("CustomerId is required.", exception.Message);
    }
    [Fact]
    public void Validate_ShouldThrowException_WhenDeliveryAddressIsMissingForDeliveryOrder()
    {
        // Arrange
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Status = OrderStatus.Pending,
            OrderType = OrderType.Delivery,
            DeliveryAddress = null,
            TotalAmount = 100,
            OrderItems = new List<OrderItem>
        {
            new OrderItem { Id = Guid.NewGuid(), Quantity = 2, MenuItemId = Guid.NewGuid() }
        }
        };

        // Act & Assert
        var exception = Assert.Throws<InvalidInputException>(() => _orderValidator.Validate(order));
        Assert.Contains("Delivery address is required for delivery orders.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenOrderItemsHaveInvalidQuantity()
    {
        // Arrange
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Status = OrderStatus.Pending,
            OrderType = OrderType.Delivery,
            DeliveryAddress = "123 Street",
            TotalAmount = 100,
            OrderItems = new List<OrderItem>
        {
            new OrderItem { Id = Guid.NewGuid(), Quantity = 0, MenuItemId = Guid.NewGuid() }
        }
        };

        // Act & Assert
        var exception = Assert.Throws<InvalidInputException>(() => _orderValidator.Validate(order));
        Assert.Contains("Order items need to have quantity > 0.", exception.Message);
    }

    [Fact]
    public void OrderCanBeCancelled_ShouldNotThrowException_WhenOrderIsPending()
    {
        // Arrange
        var order = new Order
        {
            Status = OrderStatus.Pending
        };

        // Act & Assert
        var exception = Record.Exception(() => _orderValidator.OrderCanBeCancelled(order));
        Assert.Null(exception);
    }

    [Fact]
    public void OrderCanBeCancelled_ShouldThrowException_WhenOrderIsNotPending()
    {
        // Arrange
        var order = new Order
        {
            Status = OrderStatus.Delivered
        };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _orderValidator.OrderCanBeCancelled(order));
        Assert.Equal("Can only cancel orders in 'Pending' status.", exception.Message);
    }

    [Fact]
    public void OrderCanAdvanceStatus_ShouldNotThrowException_WhenOrderCanAdvance()
    {
        // Arrange
        var order = new Order
        {
            Status = OrderStatus.ReadyForDelivery
        };

        // Act & Assert
        var exception = Record.Exception(() => _orderValidator.OrderCanAdvanceStatus(order));
        Assert.Null(exception);
    }

    [Fact]
    public void OrderCanAdvanceStatus_ShouldThrowException_WhenOrderStatusIsDelivered()
    {
        // Arrange
        var order = new Order
        {
            Status = OrderStatus.Delivered
        };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _orderValidator.OrderCanAdvanceStatus(order));
        Assert.Equal("Order has already been completed or cancelled and cannot be advanced.", exception.Message);
    }

    [Fact]
    public void OrderCanBeAssigned_ShouldNotThrowException_WhenOrderIsReadyForDelivery()
    {
        // Arrange
        var order = new Order
        {
            Status = OrderStatus.ReadyForDelivery,
            OrderType = OrderType.Delivery
        };

        // Act & Assert
        var exception = Record.Exception(() => _orderValidator.OrderCanBeAssigned(order));
        Assert.Null(exception); // No exception should be thrown
    }

    [Fact]
    public void OrderCanBeAssigned_ShouldThrowException_WhenOrderIsNotReadyForDelivery()
    {
        // Arrange
        var order = new Order
        {
            Status = OrderStatus.Pending,
            OrderType = OrderType.Pickup // Invalid type
        };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _orderValidator.OrderCanBeAssigned(order));
        Assert.Equal("Order has already been completed or cancelled and cannot be advanced.", exception.Message);
    }
}
