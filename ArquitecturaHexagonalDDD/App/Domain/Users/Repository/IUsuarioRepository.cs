using ArquitecturaHexagonalDDD.App.Domain.Users;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Domain.Users.Repository;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(UserId id);
    Task<Usuario?> GetByEmailAsync(Email email);
    Task<Usuario?> GetByUserNameAsync(UserName userName);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<IEnumerable<Usuario>> GetActiveUsersAsync();
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task DeleteAsync(UserId id);
    Task<bool> ExistsByEmailAsync(Email email);
    Task<bool> ExistsByUserNameAsync(UserName userName);
}