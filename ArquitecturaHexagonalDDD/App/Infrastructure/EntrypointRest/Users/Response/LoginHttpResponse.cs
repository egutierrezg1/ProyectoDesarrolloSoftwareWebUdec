namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Users.Response;

public class LoginHttpResponse
{
    public string Token { get; set; } = string.Empty;
    public UserHttpResponse User { get; set; } = new();
    public DateTime ExpiresAt { get; set; }
}
