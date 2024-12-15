using Moq;
using RestaurantManagement.Db.Models;
using RestaurantManagement.Db.Repositories.MenuItems;
using RestaurantManagement.Services.Services.Orders;

namespace RestaurantManagement.Services.Tests.Tests.Services.Orders;

public class OrderCalculatorServiceTests
{
    private readonly OrderCalculatorService _orderCalculatorService;
    private readonly Mock<IMenuItemRepository> _menuItemRepositoryMock;

    public OrderCalculatorServiceTests()
    {
        _menuItemRepositoryMock = new Mock<IMenuItemRepository>();
        _orderCalculatorService = new OrderCalculatorService(_menuItemRepositoryMock.Object);
    }

    [Fact]
    public async Task CalculateTotalAmountAsync_ShouldReturnCorrectTotalAmount_WhenOrderItemsAreValid()
    {
        // Arrange
        var menuItemId1 = Guid.NewGuid();
        var menuItemId2 = Guid.NewGuid();
        var orderItems = new List<OrderItem>
        {
            new OrderItem { MenuItemId = menuItemId1, Quantity = 2 },
            new OrderItem { MenuItemId = menuItemId2, Quantity = 3 }
        };

        var menuItems = new List<MenuItem>
        {
            new MenuItem { Id = menuItemId1, Price = 10 },
            new MenuItem { Id = menuItemId2, Price = 20 }
        };

        _menuItemRepositoryMock
            .Setup(m => m.GetByIdsAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(menuItems);

        // Act
        var totalAmount = await _orderCalculatorService.CalculateTotalAmountAsync(orderItems);

        // Assert
        Assert.Equal(80, totalAmount);
    }

    [Theory]
    [InlineData(10, 5, 50)]
    [InlineData(20, 2, 40)]
    [InlineData(15, 3, 45)]
    public async Task CalculateTotalAmountAsync_ShouldReturnCorrectTotalAmount_ForDifferentQuantities(decimal price, int quantity, decimal expectedTotal)
    {
        // Arrange
        var menuItemId = Guid.NewGuid();
        var orderItems = new List<OrderItem>
        {
            new OrderItem { MenuItemId = menuItemId, Quantity = quantity }
        };

        var menuItems = new List<MenuItem>
        {
            new MenuItem { Id = menuItemId, Price = price }
        };

        _menuItemRepositoryMock
            .Setup(m => m.GetByIdsAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(menuItems);

        // Act
        var totalAmount = await _orderCalculatorService.CalculateTotalAmountAsync(orderItems);

        // Assert
        Assert.Equal(expectedTotal, totalAmount);
    }

    [Fact]
    public async Task CalculateTotalAmountAsync_ShouldReturnZero_WhenNoOrderItems()
    {
        // Arrange
        var orderItems = new List<OrderItem>();

        // Act
        var totalAmount = await _orderCalculatorService.CalculateTotalAmountAsync(orderItems);

        // Assert
        Assert.Equal(0, totalAmount); 
    }
}
