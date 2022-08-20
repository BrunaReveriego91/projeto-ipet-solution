using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet.WebAPI.Migrations
{
    public partial class FkServicosPrestador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServicosPrestador",
                schema: "projetoimpacta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrestadorId = table.Column<int>(type: "int", nullable: false),
                    ServicoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicosPrestador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicosPrestador_Prestadores_PrestadorId",
                        column: x => x.PrestadorId,
                        principalSchema: "projetoimpacta",
                        principalTable: "Prestadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicosPrestador_PrestadorId",
                schema: "projetoimpacta",
                table: "ServicosPrestador",
                column: "PrestadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicosPrestador",
                schema: "projetoimpacta");
        }
    }
}
