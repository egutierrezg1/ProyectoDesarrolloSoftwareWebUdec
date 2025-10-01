using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;

using ArquitecturaHexagonalDDD.App.Infraestructure.Adapters.Database-EntityFramework;
using ArquitecturaHexagonalDDD.App.Infraestructure.Adapters.Database-EntityFramework.Repository;
using ArquitecturaHexagonalDDD.App.Infraestructure.Adapters.Database-EntityFramework.UnitOfWork;
using ArquitecturaHexagonalDDD.App.Infraestructure.Adapters.Security;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.Out;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.In;
using ArquitecturaHexagonalDDD.App.Application.Users.Service;
using ArquitecturaHexagonalDDD.App.Application.Users.Mapper;
using ArquitecturaHexagonalDDD.App.Infraestructure.Entrypoint-Rest.Users.Mapper;
using ArquitecturaHexagonalDDD.App.Domain.Users.Repository;

namespace ArquitecturaHexagonalDDD.App.Infraestructure.Providers;

public static class ServiceProvider
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IUsuarioRepository, EntityFrameworkUserRepository>();
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

        // AutoMapper
        services.AddAutoMapper(typeof(UserMapper), typeof(UserHttpMapper));

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
