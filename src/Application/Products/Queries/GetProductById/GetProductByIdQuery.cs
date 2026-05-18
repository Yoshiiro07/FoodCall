using MediatR;
using Domain.Entities;

namespace Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Product?>;
}