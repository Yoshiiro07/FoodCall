using FoodCall.Application.DTOs;
using FoodCall.Domain.Entities;
using FoodCall.Domain.Exceptions;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await _unitOfWork.Restaurants.GetByIdAsync(request.Product.RestaurantId);
        if (restaurant == null)
            throw new EntityNotFoundException("Restaurant", request.Product.RestaurantId);

        var product = new Product(
            request.Product.RestaurantId,
            request.Product.Name,
            request.Product.Description,
            request.Product.Price
        );

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return new ProductDto(
            product.Id,
            product.RestaurantId,
            product.Name,
            product.Description,
            product.Price
        );
    }
}
