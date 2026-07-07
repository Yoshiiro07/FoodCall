using Infrastructure.Context;
using Infrastructure.Repositories;
using Domain.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Configuração do DbContext (SQLite)
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // 2. Registro dos Repositórios
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // 3. Configuração do MassTransit (RabbitMQ ou InMemory)
        var rabbitEnabled = bool.TryParse(configuration["RabbitMQ:Enabled"], out var enabled) && enabled;
        var rabbitHost = configuration["RabbitMQ:Host"] ?? "localhost";
        var rabbitVirtualHost = configuration["RabbitMQ:VirtualHost"] ?? "/";
        var rabbitUsername = configuration["RabbitMQ:Username"] ?? "guest";
        var rabbitPassword = configuration["RabbitMQ:Password"] ?? "guest";

        services.AddMassTransit(x =>
        {
            if (rabbitEnabled)
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitHost, rabbitVirtualHost, h =>
                    {
                        h.Username(rabbitUsername);
                        h.Password(rabbitPassword);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            }
            else
            {
                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            }
        });

        return services;
    }
}