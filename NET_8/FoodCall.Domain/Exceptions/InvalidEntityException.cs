namespace FoodCall.Domain.Exceptions;

public class InvalidEntityException : DomainException
{
    public InvalidEntityException(string message) 
        : base(message)
    {
    }

    public InvalidEntityException(string entityName, string reason) 
        : base($"{entityName} inválido(a): {reason}")
    {
    }
}
