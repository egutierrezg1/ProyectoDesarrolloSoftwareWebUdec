using ArquitecturaHexagonalDDD.App.Domain.Shared;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;
using ArquitecturaHexagonalDDD.App.Domain.Users.Event;
using ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

namespace ArquitecturaHexagonalDDD.App.Domain.Users;

public class Usuario : AggregateRoot
{
    public UserId Id { get; private set; }
    public UserName UserName { get; private set; }
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public Role Role { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Usuario() { } // Para EF Core

    public Usuario(UserId id, UserName userName, Email email, PasswordHash passwordHash, Role role)
    {
        Id = id;
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;

        AddDomainEvent(new UserRegisteredEvent(id, userName, email, role));
    }

    public static Usuario Create(UserName userName, Email email, PasswordHash passwordHash, Role role)
    {
        return new Usuario(UserId.New(), userName, email, passwordHash, role);
    }

    public void ChangePassword(PasswordHash newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserPasswordChangedEvent(Id));
    }

    public void Rename(UserName newUserName)
    {
        if (UserName == newUserName)
            return;

        var oldUserName = UserName;
        UserName = newUserName;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserRenamedEvent(Id, oldUserName, newUserName));
    }

    public void AssignRole(Role newRole)
    {
        if (Role == newRole)
            return;

        var oldRole = Role;
        Role = newRole;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserRoleAssignedEvent(Id, oldRole, newRole));
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new UserAlreadyInactiveException();

        IsActive = false;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserDeactivatedEvent(Id));
    }

    public void Reactivate()
    {
        if (IsActive)
            throw new UserAlreadyActiveException();

        IsActive = true;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserReactivatedEvent(Id));
    }

    public void UpdateEmail(Email newEmail)
    {
        Email = newEmail;
        UpdatedAt = DateTime.UtcNow;
    }
}