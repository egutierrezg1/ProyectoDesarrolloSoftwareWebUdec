using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Response;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Application.Empleos.Service;

public class UpdateEmpleoService : IUpdateEmpleoUseCase
{
    private readonly IEmpleoRepository _repository;
    private readonly IMapper _mapper;

    public UpdateEmpleoService(IEmpleoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EmpleoResponse> ExecuteAsync(UpdateEmpleoCommand command)
    {
        var empleo = await _repository.GetByIdAsync(new EmpleoId(command.Id));
        if (empleo == null)
            throw new KeyNotFoundException("Empleo no encontrado");

        empleo.Update(
            new Nombre(command.Nombre),
            new Categoria(command.Categoria),
            new AreaTrabajo(command.AreaTrabajo),
            new Empresa(command.Empresa),
            new Nivel(command.Nivel),
            new Sueldo(command.Sueldo),
            new Funciones(command.Funciones),
            new CargoJefe(command.CargoJefe));

        await _repository.UpdateAsync(empleo);
        return _mapper.Map<EmpleoResponse>(empleo);
    }
}

