using ArquitecturaHexagonalDDD.App.Application.Users.Port.In;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.Out;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Service;

public class LogoutService : ILogoutUseCase
{
    private readonly ITokenIssuerPort _tokenIssuer;

    public LogoutService(ITokenIssuerPort tokenIssuer)
    {
        _tokenIssuer = tokenIssuer;
    }

    public async Task ExecuteAsync(string token)
    {
        // En una implementación real, aquí podrías invalidar el token
        // agregándolo a una lista negra o actualizando su estado en la base de datos
        // Por ahora, simplemente validamos que el token sea válido
        if (!_tokenIssuer.ValidateToken(token))
        {
            throw new InvalidOperationException("Token inválido");
        }

        await Task.CompletedTask;
    }
}
