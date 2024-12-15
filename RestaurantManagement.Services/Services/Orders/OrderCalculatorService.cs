using RestaurantManagement.Db.Models;
using RestaurantManagement.Db.Repositories.MenuItems;
using RestaurantManagement.Services.Services.Orders.Interfaces;

namespace RestaurantManagement.Services.Services.Orders;

public class OrderCalculatorService : IOrderCalculatorService
{
    private readonly IMenuItemRepository _menuItemRepository;

    public OrderCalculatorService(IMenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }
    public async Task<decimal> CalculateTotalAmountAsync(List<OrderItem> orderItems)
    {
        if (!orderItems?.Any() ?? true) return 0;
        var menuItemIds = orderItems.Select(i=>i.MenuItemId).ToList();
        var menuItems = await _menuItemRepository.GetByIdsAsync(menuItemIds);
        return menuItems.Sum(mi=> mi.Price * orderItems.First(oi=>oi.MenuItemId == mi.Id).Quantity);
    }

}
