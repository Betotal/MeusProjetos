using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPModular.Shared.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddResetPasswordRequested : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ResetPasswordRequested",
                schema: "shared",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordRequested",
                schema: "shared",
                table: "AspNetUsers");
        }
    }
}
