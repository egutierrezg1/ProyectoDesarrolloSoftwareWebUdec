using Microsoft.EntityFrameworkCore.Storage;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.Out;
using ArquitecturaHexagonalDDD.App.Infraestructure.Adapters.Database-EntityFramework;

namespace ArquitecturaHexagonalDDD.App.Infraestructure.Adapters.Database-EntityFramework.UnitOfWork;

public class EntityFrameworkUnitOfWork : IUnitOfWorkPort
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    public EntityFrameworkUnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
