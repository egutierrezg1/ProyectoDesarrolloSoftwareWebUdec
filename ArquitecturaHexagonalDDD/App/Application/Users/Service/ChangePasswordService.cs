using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.In;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.Out;
using ArquitecturaHexagonalDDD.App.Domain.Users.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;
using ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Service;

public class ChangePasswordService : IChangePasswordUseCase
{
    private readonly IUsuarioRepository _userRepository;
    private readonly IPasswordHasherPort _passwordHasher;
    private readonly IPasswordStrengthPolicyPort _passwordStrengthPolicy;

    public ChangePasswordService(
        IUsuarioRepository userRepository,
        IPasswordHasherPort passwordHasher,
        IPasswordStrengthPolicyPort passwordStrengthPolicy)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _passwordStrengthPolicy = passwordStrengthPolicy;
    }

    public async Task ExecuteAsync(ChangePasswordCommand command)
    {
        var userId = new UserId(command.Id);
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException(command.Id);
        }

        // Verificar contraseña actual
        if (!_passwordHasher.VerifyPassword(command.CurrentPassword, user.PasswordHash.Value))
        {
            throw new InvalidPasswordException("La contraseña actual es incorrecta");
        }

        // Validar fortaleza de la nueva contraseña
        if (!_passwordStrengthPolicy.IsStrongPassword(command.NewPassword))
        {
            throw new InvalidPasswordException(_passwordStrengthPolicy.GetPasswordStrengthMessage(command.NewPassword));
        }

        // Hash de la nueva contraseña
        var newPasswordHash = new PasswordHash(_passwordHasher.HashPassword(command.NewPassword));

        // Cambiar contraseña
        user.ChangePassword(newPasswordHash);

        await _userRepository.UpdateAsync(user);
    }
}
