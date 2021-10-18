using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class addtopersonelinfo3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Anyother_LoanYear",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FGSHLS_loanYear",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NHFcodeYear",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NNMFBL_loanYear",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NNNCS_loanYear",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NSITFcodeYear",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PPCFS_loanYear",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "car_loanYear",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "welfare_loanYear",
                table: "ef_personalInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anyother_LoanYear",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "FGSHLS_loanYear",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "NHFcodeYear",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "NNMFBL_loanYear",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "NNNCS_loanYear",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "NSITFcodeYear",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "PPCFS_loanYear",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "car_loanYear",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "welfare_loanYear",
                table: "ef_personalInfos");
        }
    }
}
