using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;

public class Role : Shared.ValueObject
{
    public string Value { get; }

    public Role(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El rol no puede estar vacío", nameof(value));

        var validRoles = new[] { "Admin", "User", "Guest" };
        if (!validRoles.Contains(value))
            throw new ArgumentException($"El rol '{value}' no es válido. Roles válidos: {string.Join(", ", validRoles)}", nameof(value));

        Value = value;
    }

    public static Role Admin => new("Admin");
    public static Role User => new("User");
    public static Role Guest => new("Guest");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Role role) => role.Value;
    public static implicit operator Role(string value) => new(value);
}
