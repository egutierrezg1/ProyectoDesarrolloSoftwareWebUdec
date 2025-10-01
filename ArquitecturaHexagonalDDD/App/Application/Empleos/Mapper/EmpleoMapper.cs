using AutoMapper;
using ArquitecturaHexagonalDDD.App.Domain.Empleos;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Response;

namespace ArquitecturaHexagonalDDD.App.Application.Empleos.Mapper;

public class EmpleoMapper : Profile
{
    public EmpleoMapper()
    {
        // Domain to Response
        CreateMap<Empleo, EmpleoResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre.Value))
            .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Value))
            .ForMember(dest => dest.AreaTrabajo, opt => opt.MapFrom(src => src.AreaTrabajo.Value))
            .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Empresa.Value))
            .ForMember(dest => dest.Nivel, opt => opt.MapFrom(src => src.Nivel.Value))
            .ForMember(dest => dest.Sueldo, opt => opt.MapFrom(src => src.Sueldo.Value))
            .ForMember(dest => dest.Funciones, opt => opt.MapFrom(src => src.Funciones.Value))
            .ForMember(dest => dest.CargoJefe, opt => opt.MapFrom(src => src.CargoJefe.Value));

        // Command to Value Objects
        CreateMap<CreateEmpleoCommand, Nombre>().ConstructUsing(src => new Nombre(src.Nombre));
        CreateMap<CreateEmpleoCommand, Categoria>().ConstructUsing(src => new Categoria(src.Categoria));
        CreateMap<CreateEmpleoCommand, AreaTrabajo>().ConstructUsing(src => new AreaTrabajo(src.AreaTrabajo));
        CreateMap<CreateEmpleoCommand, Empresa>().ConstructUsing(src => new Empresa(src.Empresa));
        CreateMap<CreateEmpleoCommand, Nivel>().ConstructUsing(src => new Nivel(src.Nivel));
        CreateMap<CreateEmpleoCommand, Sueldo>().ConstructUsing(src => new Sueldo(src.Sueldo));
        CreateMap<CreateEmpleoCommand, Funciones>().ConstructUsing(src => new Funciones(src.Funciones));
        CreateMap<CreateEmpleoCommand, CargoJefe>().ConstructUsing(src => new CargoJefe(src.CargoJefe));

        CreateMap<UpdateEmpleoCommand, Nombre>().ConstructUsing(src => new Nombre(src.Nombre));
        CreateMap<UpdateEmpleoCommand, Categoria>().ConstructUsing(src => new Categoria(src.Categoria));
        CreateMap<UpdateEmpleoCommand, AreaTrabajo>().ConstructUsing(src => new AreaTrabajo(src.AreaTrabajo));
        CreateMap<UpdateEmpleoCommand, Empresa>().ConstructUsing(src => new Empresa(src.Empresa));
        CreateMap<UpdateEmpleoCommand, Nivel>().ConstructUsing(src => new Nivel(src.Nivel));
        CreateMap<UpdateEmpleoCommand, Sueldo>().ConstructUsing(src => new Sueldo(src.Sueldo));
        CreateMap<UpdateEmpleoCommand, Funciones>().ConstructUsing(src => new Funciones(src.Funciones));
        CreateMap<UpdateEmpleoCommand, CargoJefe>().ConstructUsing(src => new CargoJefe(src.CargoJefe));
    }
}

