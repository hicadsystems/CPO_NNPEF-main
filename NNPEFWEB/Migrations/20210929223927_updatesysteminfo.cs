using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class updatesysteminfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfficersFormNo",
                table: "ef_systeminfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RatingsFormNo",
                table: "ef_systeminfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingFormNo",
                table: "ef_systeminfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficersFormNo",
                table: "ef_systeminfos");

            migrationBuilder.DropColumn(
                name: "RatingsFormNo",
                table: "ef_systeminfos");

            migrationBuilder.DropColumn(
                name: "TrainingFormNo",
                table: "ef_systeminfos");
        }
    }
}
