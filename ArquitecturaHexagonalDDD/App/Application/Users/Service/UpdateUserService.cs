using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Response;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Users;
using ArquitecturaHexagonalDDD.App.Domain.Users.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;
using ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Service;

public class UpdateUserService : IUpdateUserUseCase
{
    private readonly IUsuarioRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserService(IUsuarioRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResponse> ExecuteAsync(UpdateUserCommand command)
    {
        var userId = new UserId(command.Id);
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException(command.Id);
        }

        // Crear value objects
        var userName = new UserName(command.UserName);
        var email = new Email(command.Email);
        var role = new Role(command.Role);

        // Verificar si el email ya existe en otro usuario
        var existingUserByEmail = await _userRepository.GetByEmailAsync(email);
        if (existingUserByEmail != null && existingUserByEmail.Id != userId)
        {
            throw new EmailAlreadyExistsException(email.Value);
        }

        // Verificar si el nombre de usuario ya existe en otro usuario
        var existingUserByUserName = await _userRepository.GetByUserNameAsync(userName);
        if (existingUserByUserName != null && existingUserByUserName.Id != userId)
        {
            throw new InvalidUserNameException("El nombre de usuario ya está en uso");
        }

        // Actualizar usuario
        user.Rename(userName);
        user.UpdateEmail(email);
        user.AssignRole(role);

        await _userRepository.UpdateAsync(user);

        return _mapper.Map<UserResponse>(user);
    }
}
