using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet.WebAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Clientes",
                schema: "dbo",
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
                schema: "dbo",
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
                name: "Generos",
                schema: "dbo",
                columns: table => new
                {
                    GeneroId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.GeneroId);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                schema: "dbo",
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

            migrationBuilder.CreateTable(
                name: "Prestadores",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CPF_CNPJ = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatsApp = table.Column<bool>(type: "bit", nullable: false),
                    Data_Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servicos",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Data_Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TamanhosPet",
                schema: "dbo",
                columns: table => new
                {
                    TamanhoPetId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TamanhosPet", x => x.TamanhoPetId);
                });

            migrationBuilder.CreateTable(
                name: "TipoPet",
                schema: "dbo",
                columns: table => new
                {
                    TipoPetId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPet", x => x.TipoPetId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosPrestadores",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    PrestadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosPrestadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnderecosPrestadores",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrestadorId = table.Column<int>(type: "int", nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Referencia = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    SemNumero = table.Column<bool>(type: "bit", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UF = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatsApp = table.Column<bool>(type: "bit", nullable: false),
                    Data_Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecosPrestadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnderecosPrestadores_Prestadores_PrestadorId",
                        column: x => x.PrestadorId,
                        principalSchema: "dbo",
                        principalTable: "Prestadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicosPrestador",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrestadorId = table.Column<int>(type: "int", nullable: false),
                    ServicoId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Data_Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicosPrestador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicosPrestador_Prestadores_PrestadorId",
                        column: x => x.PrestadorId,
                        principalSchema: "dbo",
                        principalTable: "Prestadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Generos",
                columns: new[] { "GeneroId", "Descricao" },
                values: new object[,]
                {
                    { 0, "Feminino" },
                    { 1, "Masculino" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TamanhosPet",
                columns: new[] { "TamanhoPetId", "Descricao" },
                values: new object[,]
                {
                    { 0, "Mini" },
                    { 1, "Pequeno" },
                    { 2, "Medio" },
                    { 3, "Grande" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TipoPet",
                columns: new[] { "TipoPetId", "Descricao" },
                values: new object[,]
                {
                    { 0, "Canino" },
                    { 1, "Felino" },
                    { 2, "Roedor" },
                    { 3, "Reptil" },
                    { 4, "Ave" },
                    { 5, "Peixe" },
                    { 6, "Equino" },
                    { 7, "Outros" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnderecosPrestadores_PrestadorId",
                schema: "dbo",
                table: "EnderecosPrestadores",
                column: "PrestadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicosPrestador_PrestadorId",
                schema: "dbo",
                table: "ServicosPrestador",
                column: "PrestadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EnderecosClientes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EnderecosPrestadores",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Generos",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Pets",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Servicos",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ServicosPrestador",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TamanhosPet",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TipoPet",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UsuariosPrestadores",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Prestadores",
                schema: "dbo");
        }
    }
}
