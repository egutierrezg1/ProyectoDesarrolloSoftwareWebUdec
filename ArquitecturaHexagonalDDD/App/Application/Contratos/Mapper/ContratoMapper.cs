using AutoMapper;
using ArquitecturaHexagonalDDD.App.Domain.Contratos;
using ArquitecturaHexagonalDDD.App.Domain.Contratos.ValueObject;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Response;

namespace ArquitecturaHexagonalDDD.App.Application.Contratos.Mapper;

public class ContratoMapper : Profile
{
    public ContratoMapper()
    {
        // Domain to Response
        CreateMap<Contrato, ContratoResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Empresa.Value))
            .ForMember(dest => dest.EmpleadoId, opt => opt.MapFrom(src => src.EmpleadoId.Value))
            .ForMember(dest => dest.Funciones, opt => opt.MapFrom(src => src.Funciones.Value))
            .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Monto.Value))
            .ForMember(dest => dest.FrecuenciaPago, opt => opt.MapFrom(src => src.FrecuenciaPago.Value));

        // Commands to Value Objects
        CreateMap<CreateContratoCommand, Empresa>().ConstructUsing(src => new Empresa(src.Empresa));
        CreateMap<CreateContratoCommand, EmpleadoId>().ConstructUsing(src => new EmpleadoId(src.EmpleadoId));
        CreateMap<CreateContratoCommand, Funciones>().ConstructUsing(src => new Funciones(src.Funciones));
        CreateMap<CreateContratoCommand, Monto>().ConstructUsing(src => new Monto(src.Monto));
        CreateMap<CreateContratoCommand, FrecuenciaPago>().ConstructUsing(src => new FrecuenciaPago(src.FrecuenciaPago));

        CreateMap<UpdateContratoCommand, Empresa>().ConstructUsing(src => new Empresa(src.Empresa));
        CreateMap<UpdateContratoCommand, EmpleadoId>().ConstructUsing(src => new EmpleadoId(src.EmpleadoId));
        CreateMap<UpdateContratoCommand, Funciones>().ConstructUsing(src => new Funciones(src.Funciones));
        CreateMap<UpdateContratoCommand, Monto>().ConstructUsing(src => new Monto(src.Monto));
        CreateMap<UpdateContratoCommand, FrecuenciaPago>().ConstructUsing(src => new FrecuenciaPago(src.FrecuenciaPago));
    }
}

