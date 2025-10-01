using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Response;

namespace ArquitecturaHexagonalDDD.App.Application.Empleos.Port.In;

public interface ICreateEmpleoUseCase
{
    Task<EmpleoResponse> ExecuteAsync(CreateEmpleoCommand command);
}

public interface IUpdateEmpleoUseCase
{
    Task<EmpleoResponse> ExecuteAsync(UpdateEmpleoCommand command);
}

public interface IDeleteEmpleoUseCase
{
    Task ExecuteAsync(DeleteEmpleoCommand command);
}

public interface IGetEmpleoByIdUseCase
{
    Task<EmpleoResponse?> ExecuteAsync(GetEmpleoByIdQuery query);
}

public interface IListEmpleosUseCase
{
    Task<EmpleoListResponse> ExecuteAsync(ListEmpleosQuery query);
}

