using AutoMapper;
using RestaurantManagement.Api.Contracts;
using RestaurantManagement.Db.Enums;
using RestaurantManagement.Services.Dtos;
using RestaurantManagement.Services.Services.Orders.Interfaces;

namespace RestaurantManagement.Api.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/orders", async (IOrderService orderService) =>
        {
            var orders = await orderService.GetOrdersAsync();
            return Results.Ok(orders);
        });

        endpoints.MapGet("/orders/{id:guid}", async (Guid id, IOrderService orderService) =>
        {
            var order = await orderService.GetOrderByIdAsync(id);
            if (order is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(order);
        });

        endpoints.MapGet("/orders/status/{status}", async (OrderStatus status, IOrderService orderService) =>
        {
            var orders = await orderService.GetOrdersByStatusAsync(status);
            return Results.Ok(orders);
        });

        endpoints.MapGet("/orders/customer/{customerId:guid}", async (Guid customerId, IOrderService orderService) =>
        {
            var orders = await orderService.GetOrdersByCustomerAsync(customerId);
            return Results.Ok(orders);
        });

        endpoints.MapPost("/orders", async (PostOrderContract orderContract,IMapper mapper, IOrderService orderService) =>
        {
            var orderDto = mapper.Map<OrderDto>(orderContract);
            var addedOrderDto = await orderService.AddOrderAsync(orderDto);
            return Results.Created($"/orders/{addedOrderDto.Id}", addedOrderDto);
        });

        endpoints.MapPut("/orders/{id:guid}", async (Guid id, OrderDto orderDto, IOrderService orderService) =>
        {
            if (id != orderDto.Id)
            {
                return Results.BadRequest("Order ID mismatch");
            }

            await orderService.UpdateOrderAsync(orderDto);
            return Results.NoContent();
        });

        endpoints.MapPut("/orders/{id:guid}/cancel", async (Guid id, IOrderService orderService) =>
        {
            await orderService.CancelOrderAsync(id);
            return Results.NoContent();
        });

        endpoints.MapPut("/orders/{id:guid}/advance", async (Guid id, IOrderService orderService) =>
        {
            await orderService.AdvanceOrderStatusAsync(id);
            return Results.NoContent();
        });

        endpoints.MapPut("/orders/{id:guid}/not-delivered", async (Guid id, IOrderService orderService) =>
        {
            await orderService.UpdateOrderNotDeliveredAsync(id);
            return Results.NoContent();
        });

        endpoints.MapPut("/orders/{id:guid}/assign/{deliveryStaffId:guid}", async (Guid id, Guid deliveryStaffId, IOrderService orderService) =>
        {
            await orderService.AssignOrderToDeliveryStaffAsync(id, deliveryStaffId);
            return Results.NoContent();
        });

        endpoints.MapDelete("/orders/{id:guid}", async (Guid id, IOrderService orderService) =>
        {
            await orderService.DeleteOrderAsync(id);
            return Results.NoContent();
        });


        endpoints.MapGet("/orders/stats", async (IOrderStatisticsService orderService) =>
        {
            var stats = await orderService.GetOrderStatisticsAsync();
            return Results.Ok(stats);
        });
    }
}
