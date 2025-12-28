using FoodCall.Domain.Entities;
using FoodCall.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodCall.Infrastructure.Persistence.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly FoodCallDbContext _context;

    public ReviewRepository(FoodCallDbContext context)
    {
        _context = context;
    }

    public async Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Review?> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .FirstOrDefaultAsync(r => r.OrderId == orderId, cancellationToken);
    }

    public async Task<IEnumerable<Review>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .Where(r => r.CustomerId == customerId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Review review, CancellationToken cancellationToken = default)
    {
        await _context.Reviews.AddAsync(review, cancellationToken);
    }

    public Task UpdateAsync(Review review, CancellationToken cancellationToken = default)
    {
        _context.Reviews.Update(review);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var review = await GetByIdAsync(id, cancellationToken);
        if (review != null)
        {
            _context.Reviews.Remove(review);
        }
    }
}