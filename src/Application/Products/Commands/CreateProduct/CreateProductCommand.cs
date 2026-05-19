using MediatR;

namespace Application.Products.Commands.CreateProduct;

public record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>;