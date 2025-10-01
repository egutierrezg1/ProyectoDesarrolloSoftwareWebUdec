using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Users.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;
using ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Service;

public class DeleteUserService : IDeleteUserUseCase
{
    private readonly IUsuarioRepository _userRepository;

    public DeleteUserService(IUsuarioRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync(DeleteUserCommand command)
    {
        var userId = new UserId(command.Id);
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException(command.Id);
        }

        await _userRepository.DeleteAsync(userId);
    }
}
