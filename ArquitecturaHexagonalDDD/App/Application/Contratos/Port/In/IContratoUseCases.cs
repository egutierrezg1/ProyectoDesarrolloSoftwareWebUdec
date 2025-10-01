using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Response;

namespace ArquitecturaHexagonalDDD.App.Application.Contratos.Port.In;

public interface ICreateContratoUseCase
{
    Task<ContratoResponse> ExecuteAsync(CreateContratoCommand command);
}

public interface IUpdateContratoUseCase
{
    Task<ContratoResponse> ExecuteAsync(UpdateContratoCommand command);
}

public interface IDeleteContratoUseCase
{
    Task ExecuteAsync(DeleteContratoCommand command);
}

public interface IGetContratoByIdUseCase
{
    Task<ContratoResponse?> ExecuteAsync(GetContratoByIdQuery query);
}

public interface IListContratosUseCase
{
    Task<ContratoListResponse> ExecuteAsync(ListContratosQuery query);
}

