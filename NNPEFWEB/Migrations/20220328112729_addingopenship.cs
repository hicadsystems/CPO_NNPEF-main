using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class addingopenship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "openship",
                table: "ef_shiplogins",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "openship",
                table: "ef_PersonnelLogins",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "openship",
                table: "ef_shiplogins");

            migrationBuilder.DropColumn(
                name: "openship",
                table: "ef_PersonnelLogins");
        }
    }
}
