using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class addtopersonelinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "division",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "qualification",
                table: "ef_personalInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "division",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "qualification",
                table: "ef_personalInfos");
        }
    }
}
