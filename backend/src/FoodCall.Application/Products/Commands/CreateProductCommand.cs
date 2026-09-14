using FoodCall.Application.Products.Queries;
using FoodCall.Domain.Entities;
using FoodCall.Domain.Interfaces;
using MediatR;

namespace FoodCall.Application.Products.Commands;

// O próprio Command recebe os dados da requisição HTTP e define o retorno
public record CreateProductCommand(string Name, string Description, decimal Price, bool Active) : IRequest<ProductResponse>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Description, request.Price, request.Active);
        await _repository.AddAsync(product);

        return new ProductResponse(product.Id, product.Name, product.Description, product.Price, product.Active);
    }
}