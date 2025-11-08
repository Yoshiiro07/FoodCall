using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> AddAsync(Order order);
    }
}