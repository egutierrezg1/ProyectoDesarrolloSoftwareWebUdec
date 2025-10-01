using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Response;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.In;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.Out;
using ArquitecturaHexagonalDDD.App.Domain.Users;
using ArquitecturaHexagonalDDD.App.Domain.Users.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;
using ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Service;

public class CreateUserService : ICreateUserUseCase
{
    private readonly IUsuarioRepository _userRepository;
    private readonly IPasswordHasherPort _passwordHasher;
    private readonly IPasswordStrengthPolicyPort _passwordStrengthPolicy;
    private readonly IMapper _mapper;

    public CreateUserService(
        IUsuarioRepository userRepository,
        IPasswordHasherPort passwordHasher,
        IPasswordStrengthPolicyPort passwordStrengthPolicy,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _passwordStrengthPolicy = passwordStrengthPolicy;
        _mapper = mapper;
    }

    public async Task<UserResponse> ExecuteAsync(CreateUserCommand command)
    {
        // Validar fortaleza de la contraseña
        if (!_passwordStrengthPolicy.IsStrongPassword(command.Password))
        {
            throw new InvalidPasswordException(_passwordStrengthPolicy.GetPasswordStrengthMessage(command.Password));
        }

        // Crear value objects
        var userName = new UserName(command.UserName);
        var email = new Email(command.Email);
        var role = new Role(command.Role);

        // Verificar si el email ya existe
        if (await _userRepository.ExistsByEmailAsync(email))
        {
            throw new EmailAlreadyExistsException(email.Value);
        }

        // Verificar si el nombre de usuario ya existe
        if (await _userRepository.ExistsByUserNameAsync(userName))
        {
            throw new InvalidUserNameException("El nombre de usuario ya está en uso");
        }

        // Hash de la contraseña
        var passwordHash = new PasswordHash(_passwordHasher.HashPassword(command.Password));

        // Crear usuario
        var user = Usuario.Create(userName, email, passwordHash, role);

        // Guardar usuario
        await _userRepository.AddAsync(user);

        // Mapear respuesta
        return _mapper.Map<UserResponse>(user);
    }
}
