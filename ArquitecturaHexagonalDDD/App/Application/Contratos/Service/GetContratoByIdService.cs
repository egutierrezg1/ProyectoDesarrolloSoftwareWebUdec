using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Response;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Contratos.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Contratos.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Application.Contratos.Service;

public class GetContratoByIdService : IGetContratoByIdUseCase
{
    private readonly IContratoRepository _repository;
    private readonly IMapper _mapper;

    public GetContratoByIdService(IContratoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ContratoResponse?> ExecuteAsync(GetContratoByIdQuery query)
    {
        var contrato = await _repository.GetByIdAsync(new ContratoId(query.Id));
        return contrato == null ? null : _mapper.Map<ContratoResponse>(contrato);
    }
}

