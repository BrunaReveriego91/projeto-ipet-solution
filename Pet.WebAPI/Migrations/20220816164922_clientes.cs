using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet.WebAPI.Migrations
{
    public partial class clientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "id",
                schema: "projetoimpacta",
                table: "tbPet");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                schema: "projetoimpacta",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "birthday",
                schema: "projetoimpacta",
                table: "tbPet");

            migrationBuilder.DropColumn(
                name: "breed",
                schema: "projetoimpacta",
                table: "tbPet");

            migrationBuilder.DropColumn(
                name: "color",
                schema: "projetoimpacta",
                table: "tbPet");

            migrationBuilder.DropColumn(
                name: "gender",
                schema: "projetoimpacta",
                table: "tbPet");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "projetoimpacta",
                table: "tbPet");

            migrationBuilder.DropColumn(
                name: "owner",
                schema: "projetoimpacta",
                table: "tbPet");

            migrationBuilder.DropColumn(
                name: "type",
                schema: "projetoimpacta",
                table: "tbPet");

            migrationBuilder.RenameTable(
                name: "tbPet",
                schema: "projetoimpacta",
                newName: "Pets",
                newSchema: "projetoimpacta");

            migrationBuilder.RenameColumn(
                name: "EMail",
                schema: "projetoimpacta",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "projetoimpacta",
                table: "Pets",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "DataCadastro",
                schema: "projetoimpacta",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                schema: "projetoimpacta",
                table: "Usuarios",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Referencia",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "Aniversario",
                schema: "projetoimpacta",
                table: "Pets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cor",
                schema: "projetoimpacta",
                table: "Pets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Genero",
                schema: "projetoimpacta",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                schema: "projetoimpacta",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NomeCompleto",
                schema: "projetoimpacta",
                table: "Pets",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Peso",
                schema: "projetoimpacta",
                table: "Pets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Raca",
                schema: "projetoimpacta",
                table: "Pets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TamanhoPet",
                schema: "projetoimpacta",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoPet",
                schema: "projetoimpacta",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pets",
                schema: "projetoimpacta",
                table: "Pets",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Clientes",
                schema: "projetoimpacta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Aniversario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Telefone1 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    WhatsApp = table.Column<bool>(type: "bit", nullable: false),
                    Telefone2 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "projetoimpacta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pets",
                schema: "projetoimpacta",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                schema: "projetoimpacta",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Senha",
                schema: "projetoimpacta",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Aniversario",
                schema: "projetoimpacta",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Cor",
                schema: "projetoimpacta",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Genero",
                schema: "projetoimpacta",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                schema: "projetoimpacta",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "NomeCompleto",
                schema: "projetoimpacta",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Peso",
                schema: "projetoimpacta",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Raca",
                schema: "projetoimpacta",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "TamanhoPet",
                schema: "projetoimpacta",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "TipoPet",
                schema: "projetoimpacta",
                table: "Pets");

            migrationBuilder.RenameTable(
                name: "Pets",
                schema: "projetoimpacta",
                newName: "tbPet",
                newSchema: "projetoimpacta");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "projetoimpacta",
                table: "Usuarios",
                newName: "EMail");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "projetoimpacta",
                table: "tbPet",
                newName: "id");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                schema: "projetoimpacta",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Referencia",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "birthday",
                schema: "projetoimpacta",
                table: "tbPet",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "breed",
                schema: "projetoimpacta",
                table: "tbPet",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "color",
                schema: "projetoimpacta",
                table: "tbPet",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "gender",
                schema: "projetoimpacta",
                table: "tbPet",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "projetoimpacta",
                table: "tbPet",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "owner",
                schema: "projetoimpacta",
                table: "tbPet",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "type",
                schema: "projetoimpacta",
                table: "tbPet",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "id",
                schema: "projetoimpacta",
                table: "tbPet",
                column: "id");
        }
    }
}
