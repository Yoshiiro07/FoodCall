namespace Application.Common.Events
{
    public record ProductCreatedEvent(Guid ProductId, string Name, decimal Price);

    public record ProductUpdatedEvent(Guid ProductId, string Name, decimal Price);

    public record ProductDeletedEvent(Guid ProductId);
}