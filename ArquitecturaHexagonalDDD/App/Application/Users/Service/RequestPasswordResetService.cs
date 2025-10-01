using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Users.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;
using ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Service;

public class RequestPasswordResetService : IRequestPasswordResetUseCase
{
    private readonly IUsuarioRepository _userRepository;

    public RequestPasswordResetService(IUsuarioRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync(RequestPasswordResetCommand command)
    {
        var email = new Email(command.Email);
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
        {
            // Por seguridad, no revelamos si el email existe o no
            await Task.CompletedTask;
            return;
        }

        // En una implementación real, aquí generarías un token de reset
        // y enviarías un email al usuario con un enlace para resetear su contraseña
        // Por ahora, simplemente simulamos el proceso

        await Task.CompletedTask;
    }
}
