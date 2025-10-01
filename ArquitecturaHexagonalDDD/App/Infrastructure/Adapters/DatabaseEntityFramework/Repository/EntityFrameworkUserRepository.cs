using Microsoft.EntityFrameworkCore;
using ArquitecturaHexagonalDDD.App.Domain.Users;
using ArquitecturaHexagonalDDD.App.Domain.Users.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;
using ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework;
using ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework.Model;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework.Repository;

public class EntityFrameworkUserRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public EntityFrameworkUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByIdAsync(UserId id)
    {
        var userModel = await _context.Users.FindAsync(id.Value);
        return userModel == null ? null : MapToDomain(userModel);
    }

    public async Task<Usuario?> GetByEmailAsync(Email email)
    {
        var userModel = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email.Value);
        return userModel == null ? null : MapToDomain(userModel);
    }

    public async Task<Usuario?> GetByUserNameAsync(UserName userName)
    {
        var userModel = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == userName.Value);
        return userModel == null ? null : MapToDomain(userModel);
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        var userModels = await _context.Users.ToListAsync();
        return userModels.Select(MapToDomain);
    }

    public async Task<IEnumerable<Usuario>> GetActiveUsersAsync()
    {
        var userModels = await _context.Users
            .Where(u => u.IsActive)
            .ToListAsync();
        return userModels.Select(MapToDomain);
    }

    public async Task AddAsync(Usuario usuario)
    {
        var userModel = MapToModel(usuario);
        _context.Users.Add(userModel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        var userModel = MapToModel(usuario);
        _context.Users.Update(userModel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserId id)
    {
        var userModel = await _context.Users.FindAsync(id.Value);
        if (userModel != null)
        {
            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByEmailAsync(Email email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email.Value);
    }

    public async Task<bool> ExistsByUserNameAsync(UserName userName)
    {
        return await _context.Users.AnyAsync(u => u.UserName == userName.Value);
    }

    private static Usuario MapToDomain(UserModel model)
    {
        var usuario = new Usuario(
            new UserId(model.Id),
            new UserName(model.UserName),
            new Email(model.Email),
            new PasswordHash(model.PasswordHash),
            new Role(model.Role));
        
        // Usar reflexión para establecer las propiedades privadas
        var isActiveProperty = typeof(Usuario).GetProperty("IsActive");
        var createdAtProperty = typeof(Usuario).GetProperty("CreatedAt");
        var updatedAtProperty = typeof(Usuario).GetProperty("UpdatedAt");
        
        isActiveProperty?.SetValue(usuario, model.IsActive);
        createdAtProperty?.SetValue(usuario, model.CreatedAt);
        updatedAtProperty?.SetValue(usuario, model.UpdatedAt);
        
        return usuario;
    }

    private static UserModel MapToModel(Usuario usuario)
    {
        return new UserModel
        {
            Id = usuario.Id.Value,
            UserName = usuario.UserName.Value,
            Email = usuario.Email.Value,
            PasswordHash = usuario.PasswordHash.Value,
            Role = usuario.Role.Value,
            IsActive = usuario.IsActive,
            CreatedAt = usuario.CreatedAt,
            UpdatedAt = usuario.UpdatedAt
        };
    }
}
