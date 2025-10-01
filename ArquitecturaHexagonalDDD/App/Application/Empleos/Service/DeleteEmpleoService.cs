using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Application.Empleos.Service;

public class DeleteEmpleoService : IDeleteEmpleoUseCase
{
    private readonly IEmpleoRepository _repository;

    public DeleteEmpleoService(IEmpleoRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(DeleteEmpleoCommand command)
    {
        await _repository.DeleteAsync(new EmpleoId(command.Id));
    }
}

