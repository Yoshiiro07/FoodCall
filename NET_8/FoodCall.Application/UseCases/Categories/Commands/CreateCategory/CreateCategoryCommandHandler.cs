using FoodCall.Application.DTOs;
using FoodCall.Domain.Entities;
using FoodCall.Domain.Exceptions;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Categories.Commands.CreateCategory
{
    /// <summary>
    /// Handler: processa o comando CreateCategoryCommand
    /// IRequestHandler: interface do MediatR que define Handle()
    /// Responsável por: validar, criar entidade, persistir e retornar DTO
    /// </summary>
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        // UnitOfWork: gerencia todas as transações e repositórios
        // Garante que múltiplas operações sejam atômicas (tudo ou nada)
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // 1. Validação de negócio: nome duplicado?
            var existingCategory = await _unitOfWork.Categories.GetByNameAsync(request.Category.Name);
            if (existingCategory != null)
                throw new DuplicateEntityException("Category", request.Category.Name);

            // 2. Criar entidade de domínio (validações ocorrem no construtor)
            var category = new Category(
                request.Category.Name,
                request.Category.Description
            );

            // 3. Persistir no banco de dados
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync(); // Commit da transação

            // 4. Retornar DTO para o cliente
            return new CategoryDto(
                category.Id,
                category.Name,
                category.Description,
                category.IsActive
            );
        }
    }
}