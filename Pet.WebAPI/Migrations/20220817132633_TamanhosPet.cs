using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet.WebAPI.Migrations
{
    public partial class TamanhosPet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TamanhosPet",
                schema: "projetoimpacta",
                columns: table => new
                {
                    TamanhoPetId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TamanhosPet", x => x.TamanhoPetId);
                });

            migrationBuilder.InsertData(
                schema: "projetoimpacta",
                table: "TamanhosPet",
                columns: new[] { "TamanhoPetId", "Descricao" },
                values: new object[,]
                {
                    { 0, "Mini" },
                    { 1, "Pequeno" },
                    { 2, "Medio" },
                    { 3, "Grande" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TamanhosPet",
                schema: "projetoimpacta");
        }
    }
}
