using AutoMapper;
using RestaurantManagement.Db.Enums;
using RestaurantManagement.Db.Models;
using RestaurantManagement.Db.Repositories.Orders;
using RestaurantManagement.Services.Dtos;
using RestaurantManagement.Services.Services.Orders.Interfaces;
using RestaurantManagement.Services.Validators.Orders;

namespace RestaurantManagement.Services.Services.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderStatusService _orderStatusService;
    private readonly IOrderValidator _orderValidator;
    private readonly IOrderCalculatorService _orderCalculatorService;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository,
        IOrderStatusService orderStatusService,
        IOrderValidator orderValidator,
        IOrderCalculatorService orderCalculatorService,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _orderStatusService = orderStatusService;
        _orderValidator = orderValidator;
        _orderCalculatorService = orderCalculatorService;
        _mapper = mapper;
    }

    public async Task<List<OrderDto>> GetOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<OrderDto> GetOrderByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<List<OrderDto>> GetOrdersByStatusAsync(OrderStatus status)
    {
        var orders = await _orderRepository.GetByStatusAsync(status);
        return _mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<List<OrderDto>> GetOrdersByCustomerAsync(Guid customerId)
    {
        var orders = await _orderRepository.GetByCustomerIdAsync(customerId);
        return _mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<OrderDto> AddOrderAsync(OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        await InitializeOrder(order);
        _orderValidator.Validate(order);

        var addedOrder = await _orderRepository.AddAsync(order);
        return _mapper.Map<OrderDto>(addedOrder);
    }

    private async Task InitializeOrder(Order order)
    {
        order.Id = Guid.NewGuid();
        order.OrderDate = DateTime.UtcNow;
        order.Status = OrderStatus.Pending;
        order.AssignedDeliveryStaffId = null;
        order.AssignedDeliveryStaff = null;
        foreach (var orderItem in order.OrderItems)
        {
            orderItem.Id = Guid.NewGuid();
            orderItem.OrderId = order.Id;
        }
        order.TotalAmount = await _orderCalculatorService.CalculateTotalAmountAsync(order.OrderItems.ToList());
    }

    public async Task<OrderDto> UpdateOrderAsync(OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        order.TotalAmount = await _orderCalculatorService.CalculateTotalAmountAsync(order.OrderItems.ToList());
        _orderValidator.Validate(order);
        var updatedOrder = await _orderRepository.UpdateAsync(order);
         
        return _mapper.Map<OrderDto>(updatedOrder);
    }

    public Task DeleteOrderAsync(Guid id)
    {
        return _orderRepository.DeleteAsync(id);
    }

    public async Task AdvanceOrderStatusAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        _orderValidator.Validate(order);
        _orderValidator.OrderCanAdvanceStatus(order);
        order.Status = _orderStatusService.GetNextOrderStatus(order.Status, order.OrderType);

        if (order.Status == OrderStatus.Delivered)
        {
            order.DeliveredDate = DateTime.UtcNow;
        }
        await _orderRepository.UpdateAsync(order);
    }

    public async Task CancelOrderAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        _orderValidator.Validate(order);
        _orderValidator.OrderCanBeCancelled(order);
        await _orderRepository.UpdateAsync(order);
    }

    public async Task UpdateOrderNotDeliveredAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        _orderValidator.Validate(order);
        _orderValidator.OrderCanBeNotDelivered(order);
        order.Status = OrderStatus.UnableToDeliver;
        await _orderRepository.UpdateAsync(order);
    }
    public async Task AssignOrderToDeliveryStaffAsync(Guid id, Guid deliveryStaffId)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        _orderValidator.Validate(order);
        _orderValidator.OrderCanBeAssigned(order);
        order.AssignedDeliveryStaffId = deliveryStaffId;
        await _orderRepository.UpdateAsync(order);
    }
}
