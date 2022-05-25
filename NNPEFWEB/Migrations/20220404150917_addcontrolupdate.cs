using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class addcontrolupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "processingyear",
                table: "ef_control",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "processingyear",
                table: "ef_control");
        }
    }
}
