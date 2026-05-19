// src/Application/Products/Commands/CreateProduct/CreateProductCommandHandler.cs
using MediatR;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Instancia uma nova entidade de domínio gerando um novo Guid
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price
        };

        await _productRepository.AddAsync(product, cancellationToken);

        return product.Id; // Retorna o ID do produto criado
    }
}