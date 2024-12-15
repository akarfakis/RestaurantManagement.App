using RestaurantManagement.Db.Models;

namespace RestaurantManagement.Services.Validators.MenuItems;

public interface IMenuItemValidator
{
    void Validate(MenuItem menuItem);
}
