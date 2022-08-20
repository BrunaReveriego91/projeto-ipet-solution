using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet.WebAPI.Migrations
{
    public partial class AcertoTabelaUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeCompleto",
                schema: "projetoimpacta",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "DataNascimento",
                schema: "projetoimpacta",
                table: "Usuarios",
                newName: "Data_Cadastro");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                schema: "projetoimpacta",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "projetoimpacta",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                schema: "projetoimpacta",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Password",
                schema: "projetoimpacta",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "Data_Cadastro",
                schema: "projetoimpacta",
                table: "Usuarios",
                newName: "DataNascimento");

            migrationBuilder.AddColumn<string>(
                name: "NomeCompleto",
                schema: "projetoimpacta",
                table: "Usuarios",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }
    }
}
