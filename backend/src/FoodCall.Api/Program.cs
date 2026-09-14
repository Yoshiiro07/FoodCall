using FoodCall.Domain.Interfaces;
using FoodCall.Infrastructure.Context;
using FoodCall.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using FoodCall.Application.Products.Commands;


var builder = WebApplication.CreateBuilder(args);

// SQL lite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
    
// injeção
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// MediatR
// Registro do MediatR lendo os Handlers da camada Application
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Usa o mapeamento padrão do Swashbuckle
}

app.UseAuthorization();
app.MapControllers();


app.Run();