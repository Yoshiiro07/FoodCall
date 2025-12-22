using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
    Task<IEnumerable<User>> GetByRoleAsync(UserRole role);
}