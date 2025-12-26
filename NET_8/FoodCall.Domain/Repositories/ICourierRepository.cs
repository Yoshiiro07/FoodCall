using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Repositories;

public interface ICourierRepository : IRepository<Courier>
{
    Task<IReadOnlyList<Courier>> GetAvailableAsync(CancellationToken ct = default);
    Task<Courier?> GetByVehiclePlateAsync(string plate, CancellationToken ct = default);
}