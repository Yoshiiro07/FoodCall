using FoodCall.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace FoodCall.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly FoodCallDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(FoodCallDbContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Restaurants = new RestaurantRepository(_context);
        Products = new ProductRepository(_context);
        Categories = new CategoryRepository(_context);
        Orders = new OrderRepository(_context);
        Couriers = new CourierRepository(_context);
        Payments = new PaymentRepository(_context);
        Reviews = new ReviewRepository(_context);
    }

    public IUserRepository Users { get; }
    public IRestaurantRepository Restaurants { get; }
    public IProductRepository Products { get; }
    public ICategoryRepository Categories { get; }
    public IOrderRepository Orders { get; }
    public ICourierRepository Couriers { get; }
    public IPaymentRepository Payments { get; }
    public IReviewRepository Reviews { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}