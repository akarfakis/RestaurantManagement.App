using RestaurantManagement.Services.Dtos;

namespace RestaurantManagement.Services.Services.MenuItems;

public interface IMenuItemService
{
    Task<List<MenuItemDto>> GetAllMenuItemsAsync();
    Task<List<MenuItemDto>> GetMenuItemsByCategoryAsync(string category);
    Task<MenuItemDto> GetMenuItemByIdAsync(Guid id);
    Task<MenuItemDto> AddMenuItemAsync(MenuItemDto menuItemDto);
    Task<MenuItemDto> UpdateMenuItemAsync(MenuItemDto menuItemDto);
    Task DeleteMenuItemAsync(Guid id);
}
