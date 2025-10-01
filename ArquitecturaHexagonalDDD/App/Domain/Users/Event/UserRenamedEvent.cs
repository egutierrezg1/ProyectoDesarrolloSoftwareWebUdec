using ArquitecturaHexagonalDDD.App.Domain.Shared;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Domain.Users.Event;

public class UserRenamedEvent : IDomainEvent
{
    public UserId UserId { get; }
    public UserName OldUserName { get; }
    public UserName NewUserName { get; }
    public DateTime OccurredOn { get; }

    public UserRenamedEvent(UserId userId, UserName oldUserName, UserName newUserName)
    {
        UserId = userId;
        OldUserName = oldUserName;
        NewUserName = newUserName;
        OccurredOn = DateTime.UtcNow;
    }
}
