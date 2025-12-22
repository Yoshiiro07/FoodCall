using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> GetActiveCategoriesAsync();
    Task<Category?> GetWithMenuItemsAsync(int id);
}