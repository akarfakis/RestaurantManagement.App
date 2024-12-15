﻿namespace RestaurantManagement.Api.Contracts;

public class PostMenuItemContract
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}
