using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FoodCall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FoodCall.Domain.Interfaces;
using FoodCall.Infrastructure.Data.Context;

namespace FoodCall.Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FoodCallDbContext _context;

        public OrderRepository(FoodCallDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }   
}