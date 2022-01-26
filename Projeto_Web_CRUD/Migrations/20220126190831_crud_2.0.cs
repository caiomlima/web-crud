using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Web_CRUD.Migrations
{
    public partial class crud_20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "produto",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "produto",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "VendedorId",
                table: "produto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "vendedores",
                columns: table => new
                {
                    VendedorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProdutosCadastrados = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendedores", x => x.VendedorId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_produto_VendedorId",
                table: "produto",
                column: "VendedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_produto_vendedores_VendedorId",
                table: "produto",
                column: "VendedorId",
                principalTable: "vendedores",
                principalColumn: "VendedorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produto_vendedores_VendedorId",
                table: "produto");

            migrationBuilder.DropTable(
                name: "vendedores");

            migrationBuilder.DropIndex(
                name: "IX_produto_VendedorId",
                table: "produto");

            migrationBuilder.DropColumn(
                name: "VendedorId",
                table: "produto");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "produto",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "produto",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
