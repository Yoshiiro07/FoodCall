// src/Application/Products/Commands/CreateProduct/CreateProductCommandHandler.cs
using MediatR;
using Domain.Entities;
using Domain.Repositories;
using MassTransit;
using Application.Common.Events;

namespace Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateProductCommandHandler(IProductRepository productRepository, IPublishEndpoint publishEndpoint)
    {
        _productRepository = productRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price
        };

        await _productRepository.AddAsync(product, cancellationToken);

        await _publishEndpoint.Publish(new ProductCreatedEvent(product.Id, product.Name, product.Price), cancellationToken);

        return product.Id;
    }
}