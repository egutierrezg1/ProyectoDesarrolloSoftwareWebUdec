using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Response;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Contratos.Repository;

namespace ArquitecturaHexagonalDDD.App.Application.Contratos.Service;

public class ListContratosService : IListContratosUseCase
{
    private readonly IContratoRepository _repository;
    private readonly IMapper _mapper;

    public ListContratosService(IContratoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ContratoListResponse> ExecuteAsync(ListContratosQuery query)
    {
        var contratos = await _repository.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(query.Empresa))
            contratos = contratos.Where(c => c.Empresa.Value.Equals(query.Empresa, StringComparison.OrdinalIgnoreCase));

        if (query.EmpleadoId.HasValue)
            contratos = contratos.Where(c => c.EmpleadoId.Value == query.EmpleadoId.Value);

        if (!string.IsNullOrWhiteSpace(query.FrecuenciaPago))
            contratos = contratos.Where(c => c.FrecuenciaPago.Value.Equals(query.FrecuenciaPago, StringComparison.OrdinalIgnoreCase));

        if (query.MinMonto.HasValue)
            contratos = contratos.Where(c => c.Monto.Value >= query.MinMonto.Value);

        if (query.MaxMonto.HasValue)
            contratos = contratos.Where(c => c.Monto.Value <= query.MaxMonto.Value);

        if (query.Desde.HasValue)
            contratos = contratos.Where(c => c.FechaInicio >= query.Desde.Value);

        if (query.Hasta.HasValue)
            contratos = contratos.Where(c => c.FechaInicio <= query.Hasta.Value);

        var total = contratos.Count();
        var paged = contratos
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize);

        return new ContratoListResponse
        {
            Contratos = _mapper.Map<IEnumerable<ContratoResponse>>(paged),
            TotalCount = total,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}

