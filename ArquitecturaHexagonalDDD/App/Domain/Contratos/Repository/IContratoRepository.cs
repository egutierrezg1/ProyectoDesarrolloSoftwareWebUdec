using ArquitecturaHexagonalDDD.App.Domain.Contratos;
using ArquitecturaHexagonalDDD.App.Domain.Contratos.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Domain.Contratos.Repository;

public interface IContratoRepository
{
    Task<Contrato?> GetByIdAsync(ContratoId id);
    Task<IEnumerable<Contrato>> GetAllAsync();
    Task AddAsync(Contrato contrato);
    Task UpdateAsync(Contrato contrato);
    Task DeleteAsync(ContratoId id);
}

