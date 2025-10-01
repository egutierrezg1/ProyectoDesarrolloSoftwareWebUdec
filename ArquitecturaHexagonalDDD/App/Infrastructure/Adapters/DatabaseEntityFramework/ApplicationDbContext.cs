using Microsoft.EntityFrameworkCore;
using ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework.Model;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<EmpleoModel> Empleos { get; set; }
    public DbSet<ContratoModel> Contratos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la entidad User
        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.PasswordHash)
                .IsRequired();

            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt);

            // Índices únicos
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.UserName).IsUnique();
        });

        // Configuración de la entidad Empleo
        modelBuilder.Entity<EmpleoModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Categoria)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.AreaTrabajo)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Empresa)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Nivel)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Sueldo)
                .HasPrecision(18, 2);

            entity.Property(e => e.Funciones)
                .IsRequired()
                .HasMaxLength(2000);

            entity.Property(e => e.CargoJefe)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt);

            entity.HasIndex(e => new { e.Empresa, e.Nombre });
        });

        // Configuración de la entidad Contrato
        modelBuilder.Entity<ContratoModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Empresa)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Funciones)
                .IsRequired()
                .HasMaxLength(2000);

            entity.Property(e => e.FrecuenciaPago)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Monto)
                .HasPrecision(18, 2);

            entity.Property(e => e.FechaFirma)
                .IsRequired();

            entity.Property(e => e.FechaInicio)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt);

            entity.HasIndex(e => new { e.Empresa, e.EmpleadoId });
        });
    }
}
