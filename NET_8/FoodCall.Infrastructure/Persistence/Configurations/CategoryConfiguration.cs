using FoodCall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCall.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuração da entidade Category usando Fluent API do EF Core
    /// Define como a entidade será mapeada para a tabela no banco de dados
    /// IEntityTypeConfiguration: interface do EF Core para configurações de entidade
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Define a chave primária
            builder.HasKey(c => c.Id);

            // Configuração da coluna Name
            builder.Property(c => c.Name)
                .IsRequired()           // NOT NULL no banco
                .HasMaxLength(50);      // VARCHAR(50) - mesmo limite da validação

            // Configuração da coluna Description
            builder.Property(c => c.Description)
                .HasMaxLength(200);     // VARCHAR(200) - opcional (nullable)

            // Configuração da coluna IsActive
            builder.Property(c => c.IsActive)
                .IsRequired()           // NOT NULL
                .HasDefaultValue(true); // Valor padrão no banco

            // Índice único no Name para evitar duplicatas no banco
            // Garante que não existam duas categorias com o mesmo nome
            builder.HasIndex(c => c.Name)
                .IsUnique();

            // Nome da tabela no banco de dados
            builder.ToTable("Categories");
        }
    }
}