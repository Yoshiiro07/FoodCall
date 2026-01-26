using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(Guid id);

        Task<Category?> GetByNameAsync(string name);

        Task AddAsync(Category category);

        void Update(Category category);

        void Delete(Category category);
    }
}