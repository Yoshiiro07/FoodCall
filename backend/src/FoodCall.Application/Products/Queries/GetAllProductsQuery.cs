using FoodCall.Domain.Interfaces;
using MediatR;

namespace FoodCall.Application.Products.Queries;

public record ProductResponse(Guid Id, string Name, string Description, decimal Price, bool Active);
public record GetAllProductsQuery() : IRequest<IEnumerable<ProductResponse>>;
public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse?>;

public class ProductQueryHandlers : 
    IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>,
    IRequestHandler<GetProductByIdQuery, ProductResponse?>
{
    private readonly IProductRepository _repository;

    public ProductQueryHandlers(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync();
        return products.Select(p => new ProductResponse(p.Id, p.Name, p.Description, p.Price, p.Active));
    }

    public async Task<ProductResponse?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);
        if (product == null) return null;

        return new ProductResponse(product.Id, product.Name, product.Description, product.Price, product.Active);
    }
}