using Microsoft.EntityFrameworkCore;
using ArquitecturaHexagonalDDD.App.Domain.Empleos;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;
using ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework.Model;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework.Repository;

public class EntityFrameworkEmpleoRepository : IEmpleoRepository
{
    private readonly ApplicationDbContext _context;

    public EntityFrameworkEmpleoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Empleo?> GetByIdAsync(EmpleoId id)
    {
        var model = await _context.Empleos.FindAsync(id.Value);
        return model == null ? null : MapToDomain(model);
    }

    public async Task<IEnumerable<Empleo>> GetAllAsync()
    {
        var models = await _context.Empleos.ToListAsync();
        return models.Select(MapToDomain);
    }

    public async Task AddAsync(Empleo empleo)
    {
        var model = MapToModel(empleo);
        _context.Empleos.Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Empleo empleo)
    {
        var model = MapToModel(empleo);
        _context.Empleos.Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(EmpleoId id)
    {
        var model = await _context.Empleos.FindAsync(id.Value);
        if (model != null)
        {
            _context.Empleos.Remove(model);
            await _context.SaveChangesAsync();
        }
    }

    private static Empleo MapToDomain(EmpleoModel model)
    {
        var empleo = new Empleo(
            new EmpleoId(model.Id),
            new Nombre(model.Nombre),
            new Categoria(model.Categoria),
            new AreaTrabajo(model.AreaTrabajo),
            new Empresa(model.Empresa),
            new Nivel(model.Nivel),
            new Sueldo(model.Sueldo),
            new Funciones(model.Funciones),
            new CargoJefe(model.CargoJefe)
        );

        var createdAtProperty = typeof(Empleo).GetProperty("CreatedAt");
        var updatedAtProperty = typeof(Empleo).GetProperty("UpdatedAt");
        createdAtProperty?.SetValue(empleo, model.CreatedAt);
        updatedAtProperty?.SetValue(empleo, model.UpdatedAt);

        return empleo;
    }

    private static EmpleoModel MapToModel(Empleo empleo)
    {
        return new EmpleoModel
        {
            Id = empleo.Id.Value,
            Nombre = empleo.Nombre.Value,
            Categoria = empleo.Categoria.Value,
            AreaTrabajo = empleo.AreaTrabajo.Value,
            Empresa = empleo.Empresa.Value,
            Nivel = empleo.Nivel.Value,
            Sueldo = empleo.Sueldo.Value,
            Funciones = empleo.Funciones.Value,
            CargoJefe = empleo.CargoJefe.Value,
            CreatedAt = empleo.CreatedAt,
            UpdatedAt = empleo.UpdatedAt
        };
    }
}

