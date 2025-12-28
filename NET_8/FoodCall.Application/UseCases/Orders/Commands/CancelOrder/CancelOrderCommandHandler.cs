using FoodCall.Domain.Exceptions;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Orders.Commands.CancelOrder;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public CancelOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
        if (order == null)
            throw new EntityNotFoundException("Order", request.OrderId);

        order.Cancel();
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
