using AutoMapper;
using RestaurantManagement.Common.Exceptions;
using RestaurantManagement.Db.Models;
using RestaurantManagement.Db.Repositories.MenuItems;
using RestaurantManagement.Services.Dtos;
using RestaurantManagement.Services.Validators.MenuItems;

namespace RestaurantManagement.Services.Services.MenuItems;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IMenuItemValidator _menuItemValidator;
    private readonly IMapper _mapper;

    public MenuItemService(IMenuItemRepository menuItemRepository, IMenuItemValidator menuItemValidator, IMapper mapper)
    {
        _menuItemRepository = menuItemRepository;
        _menuItemValidator = menuItemValidator;
        _mapper = mapper;
    }

    public async Task<List<MenuItemDto>> GetAllMenuItemsAsync()
    {
        var menuItems = await _menuItemRepository.GetAllAsync();
        return _mapper.Map<List<MenuItemDto>>(menuItems);
    }

    public async Task<List<MenuItemDto>> GetMenuItemsByCategoryAsync(string category)
    {
        var menuItems = await _menuItemRepository.GetByCategoryAsync(category);
        return _mapper.Map<List<MenuItemDto>>(menuItems);
    }

    public async Task<MenuItemDto> GetMenuItemByIdAsync(Guid id)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(id);
        return _mapper.Map<MenuItemDto>(menuItem);
    }

    public async Task<MenuItemDto> AddMenuItemAsync(MenuItemDto menuItemDto)
    {
        var menuItem = _mapper.Map<MenuItem>(menuItemDto);
        menuItem.Id = Guid.NewGuid();

        _menuItemValidator.Validate(menuItem);
        var addedMenuItem = await _menuItemRepository.AddAsync(menuItem);
        return _mapper.Map<MenuItemDto>(addedMenuItem);
    }

    public async Task<MenuItemDto> UpdateMenuItemAsync(MenuItemDto menuItemDto)
    {
        var menuItem = _mapper.Map<MenuItem>(menuItemDto);

        var menuItemExists = await _menuItemRepository.Exists(menuItem.Id);
        if (!menuItemExists)
        {
            throw new InvalidInputException("MenuItem not found", 404);
        }

        _menuItemValidator.Validate(menuItem);
        var result = await _menuItemRepository.UpdateAsync(menuItem);
        return _mapper.Map<MenuItemDto>(result);
    }

    public Task DeleteMenuItemAsync(Guid id)
    {
        return _menuItemRepository.DeleteAsync(id);
    }
}
