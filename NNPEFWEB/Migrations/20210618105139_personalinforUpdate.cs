using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class personalinforUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "AltNokPassport",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "NokPassport",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Passport",
                table: "ef_personalInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltNokPassport",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "NokPassport",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "Passport",
                table: "ef_personalInfos");
        }
    }
}
