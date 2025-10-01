using AutoMapper;
using ArquitecturaHexagonalDDD.App.Domain.Users;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Response;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        // Domain to Response
        CreateMap<Usuario, UserResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName.Value))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Value));

        // Command to Domain Value Objects
        CreateMap<CreateUserCommand, UserName>()
            .ConstructUsing(src => new UserName(src.UserName));
        
        CreateMap<CreateUserCommand, Email>()
            .ConstructUsing(src => new Email(src.Email));
        
        CreateMap<CreateUserCommand, Role>()
            .ConstructUsing(src => new Role(src.Role));

        CreateMap<UpdateUserCommand, UserName>()
            .ConstructUsing(src => new UserName(src.UserName));
        
        CreateMap<UpdateUserCommand, Email>()
            .ConstructUsing(src => new Email(src.Email));
        
        CreateMap<UpdateUserCommand, Role>()
            .ConstructUsing(src => new Role(src.Role));
    }
}
