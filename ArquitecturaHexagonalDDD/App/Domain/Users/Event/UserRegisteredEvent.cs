using ArquitecturaHexagonalDDD.App.Domain.Shared;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Domain.Users.Event;

public class UserRegisteredEvent : IDomainEvent
{
    public UserId UserId { get; }
    public UserName UserName { get; }
    public Email Email { get; }
    public Role Role { get; }
    public DateTime OccurredOn { get; }

    public UserRegisteredEvent(UserId userId, UserName userName, Email email, Role role)
    {
        UserId = userId;
        UserName = userName;
        Email = email;
        Role = role;
        OccurredOn = DateTime.UtcNow;
    }
}
