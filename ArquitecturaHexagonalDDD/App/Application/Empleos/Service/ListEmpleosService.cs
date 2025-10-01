using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Response;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.Repository;

namespace ArquitecturaHexagonalDDD.App.Application.Empleos.Service;

public class ListEmpleosService : IListEmpleosUseCase
{
    private readonly IEmpleoRepository _repository;
    private readonly IMapper _mapper;

    public ListEmpleosService(IEmpleoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EmpleoListResponse> ExecuteAsync(ListEmpleosQuery query)
    {
        var empleos = await _repository.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(query.Categoria))
            empleos = empleos.Where(e => e.Categoria.Value.Equals(query.Categoria, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(query.Empresa))
            empleos = empleos.Where(e => e.Empresa.Value.Equals(query.Empresa, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(query.Nivel))
            empleos = empleos.Where(e => e.Nivel.Value.Equals(query.Nivel, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(query.AreaTrabajo))
            empleos = empleos.Where(e => e.AreaTrabajo.Value.Equals(query.AreaTrabajo, StringComparison.OrdinalIgnoreCase));

        if (query.MinSueldo.HasValue)
            empleos = empleos.Where(e => e.Sueldo.Value >= query.MinSueldo.Value);

        if (query.MaxSueldo.HasValue)
            empleos = empleos.Where(e => e.Sueldo.Value <= query.MaxSueldo.Value);

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var term = query.SearchTerm;
            empleos = empleos.Where(e =>
                e.Nombre.Value.Contains(term!, StringComparison.OrdinalIgnoreCase) ||
                e.Funciones.Value.Contains(term!, StringComparison.OrdinalIgnoreCase) ||
                e.Empresa.Value.Contains(term!, StringComparison.OrdinalIgnoreCase));
        }

        var total = empleos.Count();

        var paged = empleos
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize);

        return new EmpleoListResponse
        {
            Empleos = _mapper.Map<IEnumerable<EmpleoResponse>>(paged),
            TotalCount = total,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}

