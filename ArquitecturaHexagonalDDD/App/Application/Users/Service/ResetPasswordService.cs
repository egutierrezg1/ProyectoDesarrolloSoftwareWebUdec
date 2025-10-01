using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.In;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.Out;
using ArquitecturaHexagonalDDD.App.Domain.Users.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;
using ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Service;

public class ResetPasswordService : IResetPasswordUseCase
{
    private readonly IUsuarioRepository _userRepository;
    private readonly IPasswordHasherPort _passwordHasher;
    private readonly IPasswordStrengthPolicyPort _passwordStrengthPolicy;

    public ResetPasswordService(
        IUsuarioRepository userRepository,
        IPasswordHasherPort passwordHasher,
        IPasswordStrengthPolicyPort passwordStrengthPolicy)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _passwordStrengthPolicy = passwordStrengthPolicy;
    }

    public async Task ExecuteAsync(ResetPasswordCommand command)
    {
        // En una implementación real, aquí validarías el token de reset
        // y obtendrías el usuario asociado al token
        // Por ahora, simulamos que el token es válido y obtenemos un usuario de prueba

        // Validar fortaleza de la nueva contraseña
        if (!_passwordStrengthPolicy.IsStrongPassword(command.NewPassword))
        {
            throw new InvalidPasswordException(_passwordStrengthPolicy.GetPasswordStrengthMessage(command.NewPassword));
        }

        // En una implementación real, aquí obtendrías el usuario del token
        // var userId = GetUserIdFromResetToken(command.Token);
        // var user = await _userRepository.GetByIdAsync(userId);

        // Por ahora, lanzamos una excepción indicando que esta funcionalidad
        // requiere implementación adicional del sistema de tokens de reset
        throw new NotImplementedException("La funcionalidad de reset de contraseña requiere implementación adicional del sistema de tokens");
    }
}
