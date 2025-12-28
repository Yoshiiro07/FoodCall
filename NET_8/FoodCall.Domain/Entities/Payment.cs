using FoodCall.Domain.Enums;
using FoodCall.Domain.Exceptions;

namespace FoodCall.Domain.Entities;

public class Payment
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentMethod Method { get; private set; }
    public bool IsPaid { get; private set; }
    public DateTime? PaidAt { get; private set; }

    public Payment(Guid orderId, decimal amount, PaymentMethod method)
    {
        ValidateOrderId(orderId);
        ValidateAmount(amount);
        ValidatePaymentMethod(method);

        Id = Guid.NewGuid();
        OrderId = orderId;
        Amount = amount;
        Method = method;
        IsPaid = false;
        PaidAt = null;
    }

    public void MarkAsPaid()
    {
        if (IsPaid)
            throw new BusinessRuleException("MarkAsPaid", "Pagamento já foi efetuado");

        IsPaid = true;
        PaidAt = DateTime.UtcNow;
    }

    public void CancelPayment()
    {
        if (!IsPaid)
            throw new BusinessRuleException("CancelPayment", "Não é possível cancelar um pagamento que não foi efetuado");

        IsPaid = false;
        PaidAt = null;
    }

    private void ValidateOrderId(Guid orderId)
    {
        if (orderId == Guid.Empty)
            throw new InvalidEntityException("Payment", "OrderId não pode ser vazio");
    }

    private void ValidateAmount(decimal amount)
    {
        if (amount <= 0)
            throw new InvalidEntityException("Payment", "Valor do pagamento deve ser maior que zero");

        if (amount > 999999.99m)
            throw new InvalidEntityException("Payment", "Valor do pagamento deve ser menor que R$ 999.999,99");
    }

    private void ValidatePaymentMethod(PaymentMethod method)
    {
        if (!Enum.IsDefined(typeof(PaymentMethod), method))
            throw new InvalidEntityException("Payment", "Método de pagamento inválido");
    }
}
