using FoodCall.Domain.Entities;
using FoodCall.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FoodCall.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(FoodCallDbContext context)
    {
        // Verificar se já existe algum usuário
        if (await context.Users.AnyAsync())
        {
            return; // Já tem dados
        }

        // Criar usuários de teste
        var users = new[]
        {
            CreateTestUser(
                "Admin User",
                "admin@foodcall.com",
                "11999999999",
                "admin123"
            ),
            CreateTestUser(
                "João Silva",
                "joao@teste.com",
                "11988888888",
                "senha123"
            ),
            CreateTestUser(
                "Maria Santos",
                "maria@teste.com",
                "11977777777",
                "senha123"
            )
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();

        Console.WriteLine("✅ Banco de dados populado com usuários de teste!");
        Console.WriteLine("📧 Logins disponíveis:");
        Console.WriteLine("   - admin@foodcall.com / admin123");
        Console.WriteLine("   - joao@teste.com / senha123");
        Console.WriteLine("   - maria@teste.com / senha123");
    }

    private static User CreateTestUser(string name, string email, string phone, string password)
    {
        // Hash da senha usando BCrypt
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        
        var user = new User(name, email, phone, passwordHash);
        
        // Não adicionar endereço por enquanto - pode ser adicionado depois via endpoint
        
        return user;
    }
}
