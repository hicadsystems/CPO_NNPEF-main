using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class addtoinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressofAcommodation",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nok_phone12",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nok_phone22",
                table: "ef_personalInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressofAcommodation",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "nok_phone12",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "nok_phone22",
                table: "ef_personalInfos");
        }
    }
}
