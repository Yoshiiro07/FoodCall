using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Interfaces;

public interface IAddressRepository : IRepository<Address>
{
    Task<IEnumerable<Address>> GetByUserIdAsync(int userId);
    Task<Address?> GetDefaultByUserIdAsync(int userId);
    Task SetAsDefaultAsync(int addressId, int userId);
}