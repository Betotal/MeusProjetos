using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPModular.Shared.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLicenseRefinements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxEmpresas",
                schema: "shared",
                table: "Licencas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxUsuarios",
                schema: "shared",
                table: "Licencas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "shared",
                table: "Licencas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                schema: "shared",
                table: "Licencas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxEmpresas",
                schema: "shared",
                table: "Licencas");

            migrationBuilder.DropColumn(
                name: "MaxUsuarios",
                schema: "shared",
                table: "Licencas");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "shared",
                table: "Licencas");

            migrationBuilder.DropColumn(
                name: "Tipo",
                schema: "shared",
                table: "Licencas");
        }
    }
}
