using MediatR;
using Domain.Repositories;

namespace Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;