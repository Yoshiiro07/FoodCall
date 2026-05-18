using MediatR;
using Domain.Entities;

namespace Application.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<Product>>;
}