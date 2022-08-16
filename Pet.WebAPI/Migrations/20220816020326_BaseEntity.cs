using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet.WebAPI.Migrations
{
    public partial class BaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Data_Cadastro",
                schema: "projetoimpacta",
                table: "ServicosPrestador",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Data_Cadastro",
                schema: "projetoimpacta",
                table: "Servicos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data_Cadastro",
                schema: "projetoimpacta",
                table: "ServicosPrestador");

            migrationBuilder.DropColumn(
                name: "Data_Cadastro",
                schema: "projetoimpacta",
                table: "Servicos");

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
        }
    }
}
