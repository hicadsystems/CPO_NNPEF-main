using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class addtopersonnellogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Appointment",
                table: "ef_PersonnelLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ship",
                table: "ef_PersonnelLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "spec",
                table: "ef_PersonnelLogins",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Appointment",
                table: "ef_PersonnelLogins");

            migrationBuilder.DropColumn(
                name: "ship",
                table: "ef_PersonnelLogins");

            migrationBuilder.DropColumn(
                name: "spec",
                table: "ef_PersonnelLogins");
        }
    }
}
