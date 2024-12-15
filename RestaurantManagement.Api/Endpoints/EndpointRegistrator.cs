namespace RestaurantManagement.Api.Endpoints;

public static class EndpointRegistrator
{
    public static void RegisterEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapMenuItemEndpoints();
        endpoints.MapOrderEndpoints();
        endpoints.MapCustomerEndpoints();
        endpoints.MapDeliveryStaffEndpoints();
    }
}

