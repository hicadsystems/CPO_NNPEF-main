using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class addfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Anyother_Loan",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PPCFS_loan",
                table: "ef_personalInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anyother_Loan",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "PPCFS_loan",
                table: "ef_personalInfos");
        }
    }
}
