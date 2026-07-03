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
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IRestaurantRepository restaurantRepository,
        IPublishEndpoint publishEndpoint)
    {
        _productRepository = productRepository;
        _restaurantRepository = restaurantRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (request.RestaurantId == Guid.Empty)
        {
            throw new ArgumentException("RestaurantId deve ser informado.");
        }

        var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId, cancellationToken);
        if (restaurant is null)
        {
            throw new KeyNotFoundException($"Restaurante com ID {request.RestaurantId} não encontrado.");
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price,
            RestaurantId = request.RestaurantId
        };

        await _productRepository.AddAsync(product, cancellationToken);

        await _publishEndpoint.Publish(new ProductCreatedEvent(product.Id, product.Name, product.Price), cancellationToken);

        return product.Id;
    }
}