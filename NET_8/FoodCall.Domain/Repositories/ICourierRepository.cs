using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Repositories;

public interface ICourierRepository
{
    Task<Courier?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Courier>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Courier>> GetAvailableCouriersAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Courier courier, CancellationToken cancellationToken = default);
    Task UpdateAsync(Courier courier, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
