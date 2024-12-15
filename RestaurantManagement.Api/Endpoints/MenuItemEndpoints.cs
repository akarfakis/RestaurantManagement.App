using AutoMapper;
using RestaurantManagement.Api.Contracts;
using RestaurantManagement.Services.Dtos;
using RestaurantManagement.Services.Services.MenuItems;

namespace RestaurantManagement.Api.Endpoints;

public static class MenuItemEndpoints
{
    public static void MapMenuItemEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/menu-items", async (IMenuItemService menuItemService) =>
        {
            var menuItems = await menuItemService.GetAllMenuItemsAsync();
            return Results.Ok(menuItems);
        });

        endpoints.MapGet("/menu-items/category/{category}", async (string category, IMenuItemService menuItemService) =>
        {
            var menuItems = await menuItemService.GetMenuItemsByCategoryAsync(category);
            return Results.Ok(menuItems);
        });

        endpoints.MapGet("/menu-items/{id}", async (Guid id, IMenuItemService menuItemService) =>
        {
            var menuItem = await menuItemService.GetMenuItemByIdAsync(id);
            if (menuItem is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(menuItem);
        });

        endpoints.MapPost("/menu-items", async (PostMenuItemContract menuItemContract, IMenuItemService menuItemService, IMapper mapper) =>
        {
            var menuItemDto = mapper.Map<MenuItemDto>(menuItemContract);
            var createdMenuItem = await menuItemService.AddMenuItemAsync(menuItemDto);
            return Results.Created($"/menu-items/{createdMenuItem.Id}", createdMenuItem);
        });

        endpoints.MapPut("/menu-items/{id}", async (Guid id, MenuItemDto menuItemDto, IMenuItemService menuItemService) =>
        {
            if (id != menuItemDto.Id)
            {
                return Results.BadRequest("Id mismatch.");
            }
            var updatedMenuItem = await menuItemService.UpdateMenuItemAsync(menuItemDto);
            return Results.Ok(updatedMenuItem);
        });

        endpoints.MapDelete("/menu-items/{id}", async (Guid id, IMenuItemService menuItemService) =>
        {
            await menuItemService.DeleteMenuItemAsync(id);
            return Results.NoContent();
        });
    }
}
