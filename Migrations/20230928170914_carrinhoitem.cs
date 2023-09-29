using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InduMovel.Migrations
{
    /// <inheritdoc />
    public partial class carrinhoitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarrinhoItens",
                columns: table => new
                {
                    CarrinhoItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MovelId = table.Column<int>(type: "INTEGER", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    CarrinhoId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoItens", x => x.CarrinhoItemId);
                    table.ForeignKey(
                        name: "FK_CarrinhoItens_Moveis_MovelId",
                        column: x => x.MovelId,
                        principalTable: "Moveis",
                        principalColumn: "MovelId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItens_MovelId",
                table: "CarrinhoItens",
                column: "MovelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoItens");
        }
    }
}
