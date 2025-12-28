namespace FoodCall.Domain.Exceptions;

public class DuplicateEntityException : DomainException
{
    public DuplicateEntityException(string entityName, string identifier) 
        : base($"{entityName} com '{identifier}' já existe no sistema.")
    {
    }

    public DuplicateEntityException(string message) 
        : base(message)
    {
    }
}
