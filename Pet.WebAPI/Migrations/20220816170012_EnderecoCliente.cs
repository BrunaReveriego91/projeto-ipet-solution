using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet.WebAPI.Migrations
{
    public partial class EnderecoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnderecosClientes",
                schema: "projetoimpacta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Referencia = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    SemNumero = table.Column<bool>(type: "bit", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UF = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecosClientes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnderecosClientes",
                schema: "projetoimpacta");
        }
    }
}
