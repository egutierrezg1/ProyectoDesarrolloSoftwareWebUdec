using Microsoft.EntityFrameworkCore;
using ArquitecturaHexagonalDDD.App.Domain.Contratos;
using ArquitecturaHexagonalDDD.App.Domain.Contratos.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Contratos.ValueObject;
using ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework.Model;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework.Repository;

public class EntityFrameworkContratoRepository : IContratoRepository
{
    private readonly ApplicationDbContext _context;

    public EntityFrameworkContratoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Contrato?> GetByIdAsync(ContratoId id)
    {
        var model = await _context.Contratos.FindAsync(id.Value);
        return model == null ? null : MapToDomain(model);
    }

    public async Task<IEnumerable<Contrato>> GetAllAsync()
    {
        var models = await _context.Contratos.ToListAsync();
        return models.Select(MapToDomain);
    }

    public async Task AddAsync(Contrato contrato)
    {
        var model = MapToModel(contrato);
        _context.Contratos.Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contrato contrato)
    {
        var model = MapToModel(contrato);
        _context.Contratos.Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ContratoId id)
    {
        var model = await _context.Contratos.FindAsync(id.Value);
        if (model != null)
        {
            _context.Contratos.Remove(model);
            await _context.SaveChangesAsync();
        }
    }

    private static Contrato MapToDomain(ContratoModel model)
    {
        var entity = new Contrato(
            new ContratoId(model.Id),
            model.FechaFirma,
            model.FechaInicio,
            model.FechaFin,
            new Empresa(model.Empresa),
            new EmpleadoId(model.EmpleadoId),
            new Funciones(model.Funciones),
            new Monto(model.Monto),
            new FrecuenciaPago(model.FrecuenciaPago)
        );

        var createdAtProperty = typeof(Contrato).GetProperty("CreatedAt");
        var updatedAtProperty = typeof(Contrato).GetProperty("UpdatedAt");
        createdAtProperty?.SetValue(entity, model.CreatedAt);
        updatedAtProperty?.SetValue(entity, model.UpdatedAt);

        return entity;
    }

    private static ContratoModel MapToModel(Contrato contrato)
    {
        return new ContratoModel
        {
            Id = contrato.Id.Value,
            FechaFirma = contrato.FechaFirma,
            FechaInicio = contrato.FechaInicio,
            FechaFin = contrato.FechaFin,
            Empresa = contrato.Empresa.Value,
            EmpleadoId = contrato.EmpleadoId.Value,
            Funciones = contrato.Funciones.Value,
            Monto = contrato.Monto.Value,
            FrecuenciaPago = contrato.FrecuenciaPago.Value,
            CreatedAt = contrato.CreatedAt,
            UpdatedAt = contrato.UpdatedAt
        };
    }
}

