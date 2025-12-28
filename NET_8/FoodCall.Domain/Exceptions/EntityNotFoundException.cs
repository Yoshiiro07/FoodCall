namespace FoodCall.Domain.Exceptions;

public class EntityNotFoundException : DomainException
{
    public EntityNotFoundException(string entityName, Guid id) 
        : base($"{entityName} com ID '{id}' não foi encontrado(a).")
    {
    }

    public EntityNotFoundException(string entityName, string identifier) 
        : base($"{entityName} com identificador '{identifier}' não foi encontrado(a).")
    {
    }
}
