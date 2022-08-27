using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pet.WebAPI.Migrations
{
    public partial class AcertoNomeWhatsApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WhatApp",
                schema: "projetoimpacta",
                table: "Prestadores",
                newName: "WhatsApp");

            migrationBuilder.RenameColumn(
                name: "WhatApp",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                newName: "WhatsApp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WhatsApp",
                schema: "projetoimpacta",
                table: "Prestadores",
                newName: "WhatApp");

            migrationBuilder.RenameColumn(
                name: "WhatsApp",
                schema: "projetoimpacta",
                table: "EnderecosPrestadores",
                newName: "WhatApp");
        }
    }
}
