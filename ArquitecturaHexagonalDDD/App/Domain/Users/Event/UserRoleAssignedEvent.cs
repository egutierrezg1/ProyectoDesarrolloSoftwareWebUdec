using ArquitecturaHexagonalDDD.App.Domain.Shared;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Domain.Users.Event;

public class UserRoleAssignedEvent : IDomainEvent
{
    public UserId UserId { get; }
    public Role OldRole { get; }
    public Role NewRole { get; }
    public DateTime OccurredOn { get; }

    public UserRoleAssignedEvent(UserId userId, Role oldRole, Role newRole)
    {
        UserId = userId;
        OldRole = oldRole;
        NewRole = newRole;
        OccurredOn = DateTime.UtcNow;
    }
}
