using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InduMovel.Migrations
{
    /// <inheritdoc />
    public partial class pedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Endereco1 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Endereco2 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Bairro = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Cep = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Cidade = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PedidoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalItensPedido = table.Column<int>(type: "INTEGER", nullable: false),
                    PedidoEnviado = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PedidoEnviadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.PedidoId);
                });

            migrationBuilder.CreateTable(
                name: "PedidoMoveis",
                columns: table => new
                {
                    PedidoMovelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PedidoId = table.Column<int>(type: "INTEGER", nullable: false),
                    MovelId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoMoveis", x => x.PedidoMovelId);
                    table.ForeignKey(
                        name: "FK_PedidoMoveis_Moveis_MovelId",
                        column: x => x.MovelId,
                        principalTable: "Moveis",
                        principalColumn: "MovelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoMoveis_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoMoveis_MovelId",
                table: "PedidoMoveis",
                column: "MovelId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoMoveis_PedidoId",
                table: "PedidoMoveis",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoMoveis");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
