using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Core.Abstraction;

public interface IOrderService
{
    Task<List<OrderWithProductsDto>> GetAllOrdersAsyncAndDeleteOrder();
    Task<OrderWithProductsDto?> GetOrderByIdAsync(int id);
    Task<bool> CreateOrderAsync(addOrderDto addorderDto);
    Task<bool> UpdateOrderAsync(updateOrderDto UpdateOrderDto);
    Task<bool> DeleteOrderAsync(int id);
    Task<bool> RestoreOrderAsync(int id);
}
