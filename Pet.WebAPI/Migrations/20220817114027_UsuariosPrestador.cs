using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet.WebAPI.Migrations
{
    public partial class UsuariosPrestador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicosPrestador_Prestadores_PrestadorId",
                schema: "projetoimpacta",
                table: "ServicosPrestador");

            migrationBuilder.DropTable(
                name: "Servicos",
                schema: "projetoimpacta");

            migrationBuilder.DropTable(
                name: "tbPet",
                schema: "projetoimpacta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicosPrestador",
                schema: "projetoimpacta",
                table: "ServicosPrestador");

            migrationBuilder.RenameTable(
                name: "ServicosPrestador",
                schema: "projetoimpacta",
                newName: "ServicoPrestador",
                newSchema: "projetoimpacta");

            migrationBuilder.RenameIndex(
                name: "IX_ServicosPrestador_PrestadorId",
                schema: "projetoimpacta",
                table: "ServicoPrestador",
                newName: "IX_ServicoPrestador_PrestadorId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicoPrestador",
                schema: "projetoimpacta",
                table: "ServicoPrestador",
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

            migrationBuilder.CreateTable(
                name: "Pets",
                schema: "projetoimpacta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TipoPet = table.Column<int>(type: "int", nullable: false),
                    TamanhoPet = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<double>(type: "float", nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Aniversario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Raca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoPrestador_Prestadores_PrestadorId",
                schema: "projetoimpacta",
                table: "ServicoPrestador",
                column: "PrestadorId",
                principalSchema: "projetoimpacta",
                principalTable: "Prestadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicoPrestador_Prestadores_PrestadorId",
                schema: "projetoimpacta",
                table: "ServicoPrestador");

            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "projetoimpacta");

            migrationBuilder.DropTable(
                name: "EnderecosClientes",
                schema: "projetoimpacta");

            migrationBuilder.DropTable(
                name: "Pets",
                schema: "projetoimpacta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicoPrestador",
                schema: "projetoimpacta",
                table: "ServicoPrestador");

            migrationBuilder.RenameTable(
                name: "ServicoPrestador",
                schema: "projetoimpacta",
                newName: "ServicosPrestador",
                newSchema: "projetoimpacta");

            migrationBuilder.RenameIndex(
                name: "IX_ServicoPrestador_PrestadorId",
                schema: "projetoimpacta",
                table: "ServicosPrestador",
                newName: "IX_ServicosPrestador_PrestadorId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicosPrestador",
                schema: "projetoimpacta",
                table: "ServicosPrestador",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Servicos",
                schema: "projetoimpacta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data_Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbPet",
                schema: "projetoimpacta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    birthday = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    breed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    gender = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    owner = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ServicosPrestador_Prestadores_PrestadorId",
                schema: "projetoimpacta",
                table: "ServicosPrestador",
                column: "PrestadorId",
                principalSchema: "projetoimpacta",
                principalTable: "Prestadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
