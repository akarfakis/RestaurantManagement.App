using RestaurantManagement.Common.Exceptions;
using RestaurantManagement.Db.Models;

namespace RestaurantManagement.Services.Validators.MenuItems;

public class MenuItemValidator : IMenuItemValidator
{
    public void Validate(MenuItem menuItem)
    {
        var errors = new List<string>();

        if (menuItem is null)
        {
            errors.Add("Item cannot be null.");
        }

        if (menuItem?.Price <= 0)
        {
            errors.Add("Price must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(menuItem.Category))
        {
            errors.Add("Category is required.");
        }

        if (string.IsNullOrWhiteSpace(menuItem.Name))
        {
            errors.Add("Name is required.");
        }

        if (string.IsNullOrWhiteSpace(menuItem.Description))
        {
            errors.Add("Description is required.");
        }

        if (errors.Any())
        {
            throw new InvalidInputException(string.Join(", ", errors), 400);
        }
    }
}
