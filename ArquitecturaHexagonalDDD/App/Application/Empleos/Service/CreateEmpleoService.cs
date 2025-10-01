using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Response;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Empleos;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Application.Empleos.Service;

public class CreateEmpleoService : ICreateEmpleoUseCase
{
    private readonly IEmpleoRepository _repository;
    private readonly IMapper _mapper;

    public CreateEmpleoService(IEmpleoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EmpleoResponse> ExecuteAsync(CreateEmpleoCommand command)
    {
        var empleo = Empleo.Create(
            new Nombre(command.Nombre),
            new Categoria(command.Categoria),
            new AreaTrabajo(command.AreaTrabajo),
            new Empresa(command.Empresa),
            new Nivel(command.Nivel),
            new Sueldo(command.Sueldo),
            new Funciones(command.Funciones),
            new CargoJefe(command.CargoJefe));

        await _repository.AddAsync(empleo);
        return _mapper.Map<EmpleoResponse>(empleo);
    }
}

