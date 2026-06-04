using Microsoft.AspNetCore.Mvc;
using Application; 
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<Domain.Repositories.IProductRepository, Infrastructure.Repositories.ProductRepository>();

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();