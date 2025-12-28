using FoodCall.Domain.Entities;
using FoodCall.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodCall.Infrastructure.Persistence.Repositories;

public class CourierRepository : ICourierRepository
{
    private readonly FoodCallDbContext _context;

    public CourierRepository(FoodCallDbContext context)
    {
        _context = context;
    }

    public async Task<Courier?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Couriers
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Courier>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Couriers
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Courier>> GetAvailableCouriersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Couriers
            .Where(c => c.IsAvailable)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Courier courier, CancellationToken cancellationToken = default)
    {
        await _context.Couriers.AddAsync(courier, cancellationToken);
    }

    public Task UpdateAsync(Courier courier, CancellationToken cancellationToken = default)
    {
        _context.Couriers.Update(courier);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var courier = await GetByIdAsync(id, cancellationToken);
        if (courier != null)
        {
            _context.Couriers.Remove(courier);
        }
    }
}