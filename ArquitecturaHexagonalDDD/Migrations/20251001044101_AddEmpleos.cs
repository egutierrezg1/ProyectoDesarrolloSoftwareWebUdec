using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArquitecturaHexagonalDDD.Migrations
{
    public partial class AddEmpleos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Categoria = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AreaTrabajo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Empresa = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Nivel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Sueldo = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Funciones = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    CargoJefe = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleos_Empresa_Nombre",
                table: "Empleos",
                columns: new[] { "Empresa", "Nombre" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleos");
        }
    }
}
