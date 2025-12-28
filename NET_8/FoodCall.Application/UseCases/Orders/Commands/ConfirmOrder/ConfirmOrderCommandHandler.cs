using FoodCall.Domain.Exceptions;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Orders.Commands.ConfirmOrder;

public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
        if (order == null)
            throw new EntityNotFoundException("Order", request.OrderId);

        order.Confirm();
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
