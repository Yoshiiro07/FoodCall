using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Products.Commands.CreateProduct;

public record CreateProductCommand(CreateProductDto Product) : IRequest<ProductDto>;
