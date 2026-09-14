using FoodCall.Domain.Entities;
using FoodCall.Domain.Interfaces;
using FoodCall.Infrastructure.Context;  
using Microsoft.EntityFrameworkCore;

namespace FoodCall.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _context.Products.AsNoTracking().ToListAsync();

    public async Task<Product?> GetByIdAsync(Guid id) =>
        await _context.Products.FindAsync(id);

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await GetByIdAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public Task DeleteAsync(Product product)
    {
        throw new NotImplementedException();
    }
}