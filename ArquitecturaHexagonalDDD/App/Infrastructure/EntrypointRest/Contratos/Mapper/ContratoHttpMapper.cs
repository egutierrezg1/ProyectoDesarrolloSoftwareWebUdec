using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Response;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Contratos.Request;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Contratos.Response;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Contratos.Mapper;

public class ContratoHttpMapper : Profile
{
    public ContratoHttpMapper()
    {
        // Request to Command
        CreateMap<CreateContratoHttpRequest, CreateContratoCommand>();
        CreateMap<UpdateContratoHttpRequest, UpdateContratoCommand>();

        // Response to HttpResponse
        CreateMap<ContratoResponse, ContratoHttpResponse>();
        CreateMap<ContratoListResponse, ContratoListHttpResponse>();
    }
}

