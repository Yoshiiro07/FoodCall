using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;


namespace Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly AppDbContext _context;

    public RestaurantRepository(AppDbContext context)
    {
        _context = context;
    }

   public async Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<Restaurant>()
            .Include(r => r.Products)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<Restaurant>()
            .Include(r => r.Products)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken)
    {
        await _context.Set<Restaurant>().AddAsync(restaurant, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken)
    {
        _context.Set<Restaurant>().Update(restaurant);
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(Restaurant restaurant, CancellationToken cancellationToken)
    {
        _context.Set<Restaurant>().Remove(restaurant);
        return _context.SaveChangesAsync(cancellationToken);
    }
}