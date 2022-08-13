using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet.WebAPI.Migrations
{
    public partial class Prestadores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrestadorId",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Prestadores",
                schema: "projetoimpacta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CPF_CNPJ = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestadores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnderecosPrestadores_PrestadorId",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                column: "PrestadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecosPrestadores_Prestadores_PrestadorId",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                column: "PrestadorId",
                principalSchema: "projetoimpacta",
                principalTable: "Prestadores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnderecosPrestadores_Prestadores_PrestadorId",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores");

            migrationBuilder.DropTable(
                name: "Prestadores",
                schema: "projetoimpacta");

            migrationBuilder.DropIndex(
                name: "IX_EnderecosPrestadores_PrestadorId",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores");

            migrationBuilder.DropColumn(
                name: "PrestadorId",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores");
        }
    }
}
