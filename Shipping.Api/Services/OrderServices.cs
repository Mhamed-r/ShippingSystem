using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Data;
using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Services
{
    public class OrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OrderWithProductsDto>> GetAllOrdersAsyncAndDeleteOrder()
        {
            var orders = await _unitOfWork.GetRepository<Order, int>().GetAllAsync();

            return _mapper.Map<List<OrderWithProductsDto>>(orders);
        }

        public async Task<IEnumerable<OrderWithProductsDto>> GetAllOrdersAsyncExecuteDeleteOrder()
        {
            var orders = await _unitOfWork.GetRepository<Order, int>().GetAllAsync();
            var filteredOrders = orders.Where(o => !o.IsDeleted).ToList();
            return _mapper.Map<List<OrderWithProductsDto>>(filteredOrders);
        }

        public async Task<OrderWithProductsDto?> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.GetRepository<Order, int>()
                .GetByIdAsync(id); // Use GetByIdAsync instead of GetFirstOrDefaultAsync

            if (order == null || order.IsDeleted)
            {
                return null;
            }

            return new OrderWithProductsDto { Id = order.Id, IsDeleted = order.IsDeleted };
        }

        public async Task<bool> CreateOrderAsync(OrderWithProductsDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _unitOfWork.GetRepository<Order, int>().AddAsync(order);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateOrderAsync(int id, OrderWithProductsDto orderDto)
        {
            var existingOrder = await _unitOfWork.GetRepository<Order, int>().GetByIdAsync(id);
            if (existingOrder is null) return false;

            _mapper.Map(orderDto, existingOrder);
            await _unitOfWork.GetRepository<Order, int>().UpdateAsync(existingOrder);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<Order, int>(); 

            var order = await repository.GetByIdAsync(id);
            if (order is null) return false;

            order.IsDeleted = true; 

            await repository.UpdateAsync(order); 
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> RestoreOrderAsync(int id)
        {
            var order = await _unitOfWork.GetRepository<Order, int>().GetByIdAsync(id);
            if (order is null || !order.IsDeleted) return false; 

            order.IsDeleted = false; 
            await _unitOfWork.GetRepository<Order, int>().UpdateAsync(order);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}

