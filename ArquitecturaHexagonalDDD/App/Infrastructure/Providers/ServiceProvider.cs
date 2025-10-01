using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;

using ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework;
using ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework.Repository;
using ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework.UnitOfWork;
using ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.Security;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.Out;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.In;
using ArquitecturaHexagonalDDD.App.Application.Users.Service;
using ArquitecturaHexagonalDDD.App.Application.Users.Mapper;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Users.Mapper;
using ArquitecturaHexagonalDDD.App.Domain.Users.Repository;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Port.In;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Service;
using ArquitecturaHexagonalDDD.App.Application.Empleos.Mapper;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Empleos.Mapper;
using ArquitecturaHexagonalDDD.App.Domain.Empleos.Repository;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Port.In;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Service;
using ArquitecturaHexagonalDDD.App.Application.Contratos.Mapper;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Contratos.Mapper;
using ArquitecturaHexagonalDDD.App.Domain.Contratos.Repository;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.Providers;

public static class ServiceProvider
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database (PostgreSQL)
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IUsuarioRepository, EntityFrameworkUserRepository>();
        services.AddScoped<IEmpleoRepository, EntityFrameworkEmpleoRepository>();
        services.AddScoped<IContratoRepository, EntityFrameworkContratoRepository>();
        services.AddScoped<IUnitOfWorkPort, EntityFrameworkUnitOfWork>();

        // Security Adapters
        services.AddScoped<IPasswordHasherPort, BCryptPasswordHasherAdapter>();
        services.AddScoped<IPasswordStrengthPolicyPort, PasswordStrengthPolicyAdapter>();

        var jwtSettings = configuration.GetSection("JwtSettings");
        services.AddScoped<ITokenIssuerPort>(provider =>
            new JwtTokenIssuerAdapter(
                jwtSettings["SecretKey"]!,
                jwtSettings["Issuer"]!,
                jwtSettings["Audience"]!,
                int.Parse(jwtSettings["ExpirationHours"]!)));

        // Application Services (Use Cases)
        services.AddScoped<ICreateUserUseCase, CreateUserService>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserService>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserService>();
        services.AddScoped<IGetUserByIdUseCase, GetUserByIdService>();
        services.AddScoped<IListUsersUseCase, ListUsersService>();
        services.AddScoped<ILoginUseCase, LoginService>();
        services.AddScoped<ILogoutUseCase, LogoutService>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordService>();
        services.AddScoped<IRequestPasswordResetUseCase, RequestPasswordResetService>();
        services.AddScoped<IResetPasswordUseCase, ResetPasswordService>();

        services.AddScoped<ICreateEmpleoUseCase, CreateEmpleoService>();
        services.AddScoped<IUpdateEmpleoUseCase, UpdateEmpleoService>();
        services.AddScoped<IDeleteEmpleoUseCase, DeleteEmpleoService>();
        services.AddScoped<IGetEmpleoByIdUseCase, GetEmpleoByIdService>();
        services.AddScoped<IListEmpleosUseCase, ListEmpleosService>();

        services.AddScoped<ICreateContratoUseCase, CreateContratoService>();
        services.AddScoped<IUpdateContratoUseCase, UpdateContratoService>();
        services.AddScoped<IDeleteContratoUseCase, DeleteContratoService>();
        services.AddScoped<IGetContratoByIdUseCase, GetContratoByIdService>();
        services.AddScoped<IListContratosUseCase, ListContratosService>();

        // AutoMapper
        services.AddAutoMapper(typeof(UserMapper), typeof(UserHttpMapper), typeof(EmpleoMapper), typeof(EmpleoHttpMapper), typeof(ContratoMapper), typeof(ContratoHttpMapper));

        // FluentValidation
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        // JWT Authentication
        var secretKey = jwtSettings["SecretKey"]!;
        var key = Encoding.ASCII.GetBytes(secretKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwtSettings["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();
    }
}
