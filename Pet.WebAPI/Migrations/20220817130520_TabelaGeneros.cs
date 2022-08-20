using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet.WebAPI.Migrations
{
    public partial class TabelaGeneros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                schema: "projetoimpacta",
                columns: table => new
                {
                    GeneroId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.GeneroId);
                });

            migrationBuilder.InsertData(
                schema: "projetoimpacta",
                table: "Generos",
                columns: new[] { "GeneroId", "Descricao" },
                values: new object[] { 0, "Feminino" });

            migrationBuilder.InsertData(
                schema: "projetoimpacta",
                table: "Generos",
                columns: new[] { "GeneroId", "Descricao" },
                values: new object[] { 1, "Masculino" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Generos",
                schema: "projetoimpacta");
        }
    }
}
