using ArquitecturaHexagonalDDD.App.Domain.Shared;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Domain.Empleos;

public class Empleo : AggregateRoot
{
    public EmpleoId Id { get; private set; }
    public Nombre Nombre { get; private set; }
    public Categoria Categoria { get; private set; }
    public AreaTrabajo AreaTrabajo { get; private set; }
    public Empresa Empresa { get; private set; }
    public Nivel Nivel { get; private set; }
    public Sueldo Sueldo { get; private set; }
    public Funciones Funciones { get; private set; }
    public CargoJefe CargoJefe { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Empleo() { }

    public Empleo(
        EmpleoId id,
        Nombre nombre,
        Categoria categoria,
        AreaTrabajo areaTrabajo,
        Empresa empresa,
        Nivel nivel,
        Sueldo sueldo,
        Funciones funciones,
        CargoJefe cargoJefe)
    {
        Id = id;
        Nombre = nombre;
        Categoria = categoria;
        AreaTrabajo = areaTrabajo;
        Empresa = empresa;
        Nivel = nivel;
        Sueldo = sueldo;
        Funciones = funciones;
        CargoJefe = cargoJefe;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public static Empleo Create(
        Nombre nombre,
        Categoria categoria,
        AreaTrabajo areaTrabajo,
        Empresa empresa,
        Nivel nivel,
        Sueldo sueldo,
        Funciones funciones,
        CargoJefe cargoJefe)
        => new(EmpleoId.New(), nombre, categoria, areaTrabajo, empresa, nivel, sueldo, funciones, cargoJefe);

    public void Update(
        Nombre nombre,
        Categoria categoria,
        AreaTrabajo areaTrabajo,
        Empresa empresa,
        Nivel nivel,
        Sueldo sueldo,
        Funciones funciones,
        CargoJefe cargoJefe)
    {
        Nombre = nombre;
        Categoria = categoria;
        AreaTrabajo = areaTrabajo;
        Empresa = empresa;
        Nivel = nivel;
        Sueldo = sueldo;
        Funciones = funciones;
        CargoJefe = cargoJefe;
        UpdatedAt = DateTime.UtcNow;
    }
}

