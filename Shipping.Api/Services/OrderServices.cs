using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;


        public OrderServices(IUnitOfWork unitOfWork,IMapper mapper,UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<OrderWithProductsDto>> GetAllOrdersAsyncAndDeleteOrder()
        {
            var orders = await _unitOfWork.GetRepository<Order,int>().GetAllAsync();
            var ordersDto = _mapper.Map<List<OrderWithProductsDto>>(orders);
            foreach(var order in ordersDto)
            {
                var MerchantName =await _userManager.FindByIdAsync(order.MerchantName);
                 order.MerchantName = MerchantName?.FullName;
            }

            return ordersDto;
        }
        public async Task<OrderWithProductsDto?> GetOrderByIdAsync(int id)
        {
            var findOrder = await _unitOfWork.GetRepository<Order, int>()
                .GetByIdAsync(id); // Use GetByIdAsync instead of GetFirstOrDefaultAsync

            if(findOrder == null || findOrder.IsDeleted)
            {
                return null;
            }

            var orderDto = _mapper.Map<OrderWithProductsDto>(findOrder);
            return orderDto;
        }

        public async Task<bool> CreateOrderAsync(addOrderDto addorderDto)
        {
            var order = _mapper.Map<Order>(addorderDto);
            await _unitOfWork.GetRepository<Order, int>().AddAsync(order);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateOrderAsync(updateOrderDto UpdateOrderDto)
        {
            var existingOrder = await _unitOfWork.GetRepository<Order, int>().GetByIdAsync(UpdateOrderDto.Id);
            if (existingOrder is null) return false;

            _mapper.Map(UpdateOrderDto, existingOrder);
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

