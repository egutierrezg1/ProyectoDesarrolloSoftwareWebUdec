using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Response;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Application.Empleos.Service;

public class GetEmpleoByIdService : IGetEmpleoByIdUseCase
{
    private readonly IEmpleoRepository _repository;
    private readonly IMapper _mapper;

    public GetEmpleoByIdService(IEmpleoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EmpleoResponse?> ExecuteAsync(GetEmpleoByIdQuery query)
    {
        var empleo = await _repository.GetByIdAsync(new EmpleoId(query.Id));
        return empleo == null ? null : _mapper.Map<EmpleoResponse>(empleo);
    }
}

