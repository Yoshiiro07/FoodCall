using FoodCall.Application.DTOs;
using FoodCall.Domain.Entities;
using MediatR;

namespace FoodCall.Application.UseCases.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(CreateCategoryDto Category) : IRequest<CategoryDto>;
}