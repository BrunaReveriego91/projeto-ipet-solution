using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet.WebAPI.Migrations
{
    public partial class CampoServicosAtivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCadastro",
                schema: "projetoimpacta",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "NomeCompleto",
                schema: "projetoimpacta",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Senha",
                schema: "projetoimpacta",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "projetoimpacta",
                table: "Usuarios",
                newName: "EMail");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data_Cadastro",
                schema: "projetoimpacta",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.AddColumn<DateTime>(
                name: "Data_Cadastro",
                schema: "projetoimpacta",
                table: "Prestadores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                schema: "projetoimpacta",
                table: "Prestadores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "WhatApp",
                schema: "projetoimpacta",
                table: "Prestadores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Data_Cadastro",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "WhatApp",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ServicosPrestador",
                schema: "projetoimpacta",
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
                        principalSchema: "projetoimpacta",
                        principalTable: "Prestadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosPrestadores",
                schema: "projetoimpacta",
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

            migrationBuilder.DropTable(
                name: "UsuariosPrestadores",
                schema: "projetoimpacta");

            migrationBuilder.DropColumn(
                name: "Data_Cadastro",
                schema: "projetoimpacta",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Nome",
                schema: "projetoimpacta",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Password",
                schema: "projetoimpacta",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Data_Cadastro",
                schema: "projetoimpacta",
                table: "Prestadores");

            migrationBuilder.DropColumn(
                name: "Telefone",
                schema: "projetoimpacta",
                table: "Prestadores");

            migrationBuilder.DropColumn(
                name: "WhatApp",
                schema: "projetoimpacta",
                table: "Prestadores");

            migrationBuilder.DropColumn(
                name: "Data_Cadastro",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores");

            migrationBuilder.DropColumn(
                name: "Telefone",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores");

            migrationBuilder.DropColumn(
                name: "WhatApp",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores");

            migrationBuilder.RenameColumn(
                name: "EMail",
                schema: "projetoimpacta",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "DataCadastro",
                schema: "projetoimpacta",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomeCompleto",
                schema: "projetoimpacta",
                table: "Usuarios",
                type: "nvarchar(256)",
                maxLength: 256,
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
        }
    }
}
