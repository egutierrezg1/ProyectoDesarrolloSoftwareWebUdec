namespace ArquitecturaHexagonalDDD.App.Domain.Shared;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
