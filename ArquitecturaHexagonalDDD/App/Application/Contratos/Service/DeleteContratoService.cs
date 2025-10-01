using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Contratos.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Contratos.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Application.Contratos.Service;

public class DeleteContratoService : IDeleteContratoUseCase
{
    private readonly IContratoRepository _repository;

    public DeleteContratoService(IContratoRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(DeleteContratoCommand command)
    {
        await _repository.DeleteAsync(new ContratoId(command.Id));
    }
}

