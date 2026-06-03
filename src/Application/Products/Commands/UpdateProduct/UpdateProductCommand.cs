using MediatR;

namespace Application.Products.Commands.UpdateProduct;
public record UpdateProductCommand(Guid Id, string Name, decimal Price) : IRequest;