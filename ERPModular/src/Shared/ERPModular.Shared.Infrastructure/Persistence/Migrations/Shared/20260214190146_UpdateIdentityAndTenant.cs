using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPModular.Shared.Infrastructure.Persistence.Migrations.Shared
{
    /// <inheritdoc />
    public partial class UpdateIdentityAndTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                schema: "shared",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InvitationAccepted",
                schema: "shared",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "shared",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    NomeFantasia = table.Column<string>(type: "text", nullable: false),
                    DomainId = table.Column<string>(type: "text", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataAdesao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DominioId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenants_Dominios_DominioId",
                        column: x => x.DominioId,
                        principalSchema: "shared",
                        principalTable: "Dominios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_DominioId",
                schema: "shared",
                table: "Tenants",
                column: "DominioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "shared");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                schema: "shared",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InvitationAccepted",
                schema: "shared",
                table: "AspNetUsers");
        }
    }
}
