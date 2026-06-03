using MediatR;
using Domain.Repositories;

namespace Application.Products.Commands.UpdateProduct;
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // 1. Busca o produto atual no banco
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
        {
            // Em uma arquitetura real, aqui lançaríamos uma Exception customizada (ex: NotFoundException)
            // Por enquanto, vamos apenas retornar para manter simples.
            throw new Exception($"Produto com ID {request.Id} não foi encontrado para atualização.");
        }

        // 2. Atualiza as propriedades da entidade de domínio com os novos dados
        product.Name = request.Name;
        product.Price = request.Price;

        // 3. Persiste as mudanças no banco
        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}