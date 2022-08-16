using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet.WebAPI.Migrations
{
    public partial class AjusteFKEnderecoPrestador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnderecosPrestadores_Prestadores_PrestadorId",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores");

            migrationBuilder.AlterColumn<int>(
                name: "PrestadorId",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecosPrestadores_Prestadores_PrestadorId",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                column: "PrestadorId",
                principalSchema: "projetoimpacta",
                principalTable: "Prestadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnderecosPrestadores_Prestadores_PrestadorId",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores");

            migrationBuilder.AlterColumn<int>(
                name: "PrestadorId",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecosPrestadores_Prestadores_PrestadorId",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                column: "PrestadorId",
                principalSchema: "projetoimpacta",
                principalTable: "Prestadores",
                principalColumn: "Id");
        }
    }
}
