namespace FoodCall.Domain.Exceptions;

public class BusinessRuleException : DomainException
{
    public BusinessRuleException(string message) 
        : base(message)
    {
    }

    public BusinessRuleException(string rule, string reason) 
        : base($"Regra de negócio violada ({rule}): {reason}")
    {
    }
}
