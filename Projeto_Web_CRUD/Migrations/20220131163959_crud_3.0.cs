using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Web_CRUD.Migrations
{
    public partial class crud_30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdutosCadastrados",
                table: "vendedores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdutosCadastrados",
                table: "vendedores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
