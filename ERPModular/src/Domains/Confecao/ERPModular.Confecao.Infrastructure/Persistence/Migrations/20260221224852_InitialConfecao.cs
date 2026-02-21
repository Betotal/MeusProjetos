using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPModular.Confecao.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialConfecao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preco",
                schema: "domain_confecao",
                table: "Produtos");

            migrationBuilder.RenameTable(
                name: "Produtos",
                schema: "domain_confecao",
                newName: "Produtos");

            migrationBuilder.RenameColumn(
                name: "Referencia",
                table: "Produtos",
                newName: "DomainId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Produtos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Produtos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Produtos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoReferencia",
                table: "Produtos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Produtos",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Produtos",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProdutoVariacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Tamanho = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PrecoVenda = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SaldoAtual = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    EstoqueMinimo = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    DomainId = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: false),
                    EmpresaId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoVariacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoVariacoes_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoPrecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VariacaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrecoAntigo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PrecoNovo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Motivo = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    DomainId = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: false),
                    EmpresaId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoPrecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoPrecos_ProdutoVariacoes_VariacaoId",
                        column: x => x.VariacaoId,
                        principalTable: "ProdutoVariacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimentacoesEstoque",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VariacaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    DataMovimentacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Motivo = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ReferenciaDocumento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DomainId = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: false),
                    EmpresaId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacoesEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_ProdutoVariacoes_VariacaoId",
                        column: x => x.VariacaoId,
                        principalTable: "ProdutoVariacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoPrecos_VariacaoId",
                table: "HistoricoPrecos",
                column: "VariacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_VariacaoId",
                table: "MovimentacoesEstoque",
                column: "VariacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoVariacoes_ProdutoId",
                table: "ProdutoVariacoes",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoPrecos");

            migrationBuilder.DropTable(
                name: "MovimentacoesEstoque");

            migrationBuilder.DropTable(
                name: "ProdutoVariacoes");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CodigoReferencia",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Produtos");

            migrationBuilder.EnsureSchema(
                name: "domain_confecao");

            migrationBuilder.RenameTable(
                name: "Produtos",
                newName: "Produtos",
                newSchema: "domain_confecao");

            migrationBuilder.RenameColumn(
                name: "DomainId",
                schema: "domain_confecao",
                table: "Produtos",
                newName: "Referencia");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                schema: "domain_confecao",
                table: "Produtos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                schema: "domain_confecao",
                table: "Produtos",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
