using FoodCall.Domain.Entities;
using FoodCall.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodCall.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementação concreta do ICategoryRepository usando Entity Framework Core
    /// Esta classe conversa diretamente com o banco de dados
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        // DbContext: representa a sessão com o banco de dados
        // Injetado via Dependency Injection no construtor
        private readonly FoodCallDbContext _context;

        public CategoryRepository(FoodCallDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca todas as categorias
        /// AsNoTracking: melhora performance em queries read-only
        /// ToListAsync: executa a query de forma assíncrona
        /// </summary>
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .AsNoTracking() // Não rastreia mudanças - melhor performance para leitura
                .ToListAsync();
        }

        /// <summary>
        /// Busca categoria por ID
        /// FirstOrDefaultAsync: retorna o primeiro resultado ou null
        /// </summary>
        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Busca categoria por nome
        /// Útil para validar duplicatas antes de criar/atualizar
        /// </summary>
        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        /// <summary>
        /// Adiciona nova categoria ao contexto
        /// AddAsync: marca a entidade para ser inserida
        /// SaveChanges será chamado pelo UnitOfWork
        /// </summary>
        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        /// <summary>
        /// Atualiza categoria existente
        /// Update: marca a entidade como modificada
        /// EF Core detecta automaticamente mudanças em entidades rastreadas
        /// </summary>
        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        /// <summary>
        /// Remove categoria do banco
        /// Remove: marca a entidade para exclusão
        /// </summary>
        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}