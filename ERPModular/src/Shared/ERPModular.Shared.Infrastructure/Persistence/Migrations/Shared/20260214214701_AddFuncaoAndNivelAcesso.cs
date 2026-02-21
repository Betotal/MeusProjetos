using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPModular.Shared.Infrastructure.Persistence.Migrations.Shared
{
    /// <inheritdoc />
    public partial class AddFuncaoAndNivelAcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Funcao",
                schema: "shared",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NivelAcesso",
                schema: "shared",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Funcao",
                schema: "shared",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NivelAcesso",
                schema: "shared",
                table: "AspNetUsers");
        }
    }
}
