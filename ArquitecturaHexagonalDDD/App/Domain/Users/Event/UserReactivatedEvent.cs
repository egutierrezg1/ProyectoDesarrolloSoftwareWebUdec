using ArquitecturaHexagonalDDD.App.Domain.Shared;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Domain.Users.Event;

public class UserReactivatedEvent : IDomainEvent
{
    public UserId UserId { get; }
    public DateTime OccurredOn { get; }

    public UserReactivatedEvent(UserId userId)
    {
        UserId = userId;
        OccurredOn = DateTime.UtcNow;
    }
}
