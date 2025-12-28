using FoodCall.Domain.Entities;
using FoodCall.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodCall.Infrastructure.Persistence.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly FoodCallDbContext _context;

    public RestaurantRepository(FoodCallDbContext context)
    {
        _context = context;
    }

    public async Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Restaurants
            .Include(r => r.Menu)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Restaurant?> GetByDocumentAsync(string document, CancellationToken cancellationToken = default)
    {
        return await _context.Restaurants
            .FirstOrDefaultAsync(r => r.Document == document, cancellationToken);
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Restaurants
            .Include(r => r.Menu)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Restaurant>> GetActiveRestaurantsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Restaurants
            .Where(r => r.IsActive)
            .Include(r => r.Menu)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
    {
        await _context.Restaurants.AddAsync(restaurant, cancellationToken);
    }

    public Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
    {
        _context.Restaurants.Update(restaurant);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var restaurant = await GetByIdAsync(id, cancellationToken);
        if (restaurant != null)
        {
            _context.Restaurants.Remove(restaurant);
        }
    }

    public async Task<bool> ExistsByDocumentAsync(string document, CancellationToken cancellationToken = default)
    {
        return await _context.Restaurants
            .AnyAsync(r => r.Document == document, cancellationToken);
    }
}