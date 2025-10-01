using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Response;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Empleos.Request;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Empleos.Response;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Empleos.Mapper;

public class EmpleoHttpMapper : Profile
{
    public EmpleoHttpMapper()
    {
        // Request to Command
        CreateMap<CreateEmpleoHttpRequest, CreateEmpleoCommand>();
        CreateMap<UpdateEmpleoHttpRequest, UpdateEmpleoCommand>();

        // Response to HttpResponse
        CreateMap<EmpleoResponse, EmpleoHttpResponse>();
        CreateMap<EmpleoListResponse, EmpleoListHttpResponse>();
    }
}

