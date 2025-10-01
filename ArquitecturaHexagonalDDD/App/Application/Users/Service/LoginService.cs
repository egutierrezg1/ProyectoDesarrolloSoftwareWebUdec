using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Response;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.In;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.Out;
using ArquitecturaHexagonalDDD.App.Domain.Users.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;
using ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Service;

public class LoginService : ILoginUseCase
{
    private readonly IUsuarioRepository _userRepository;
    private readonly IPasswordHasherPort _passwordHasher;
    private readonly ITokenIssuerPort _tokenIssuer;
    private readonly IMapper _mapper;

    public LoginService(
        IUsuarioRepository userRepository,
        IPasswordHasherPort passwordHasher,
        ITokenIssuerPort tokenIssuer,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenIssuer = tokenIssuer;
        _mapper = mapper;
    }

    public async Task<LoginResponse> ExecuteAsync(LoginCommand command)
    {
        var email = new Email(command.Email);
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null || !user.IsActive)
        {
            throw new UserNotFoundException(Guid.Empty);
        }

        if (!_passwordHasher.VerifyPassword(command.Password, user.PasswordHash.Value))
        {
            throw new InvalidPasswordException("Credenciales inválidas");
        }

        var token = _tokenIssuer.GenerateToken(user.Id.Value, user.UserName.Value, user.Role.Value);

        return new LoginResponse
        {
            Token = token,
            User = _mapper.Map<UserResponse>(user),
            ExpiresAt = DateTime.UtcNow.AddHours(24) // Token válido por 24 horas
        };
    }
}
