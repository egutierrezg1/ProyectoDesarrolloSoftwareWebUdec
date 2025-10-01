using ArquitecturaHexagonalDDD.App.Domain.Empleos;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Domain.Empleos.Repository;

public interface IEmpleoRepository
{
    Task<Empleo?> GetByIdAsync(EmpleoId id);
    Task<IEnumerable<Empleo>> GetAllAsync();
    Task AddAsync(Empleo empleo);
    Task UpdateAsync(Empleo empleo);
    Task DeleteAsync(EmpleoId id);
}

