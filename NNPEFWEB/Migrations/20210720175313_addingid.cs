using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class addingid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ef_personalInfos",
                table: "ef_personalInfos");

            migrationBuilder.AlterColumn<string>(
                name: "serviceNumber",
                table: "ef_personalInfos",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ef_personalInfos",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ef_personalInfos",
                table: "ef_personalInfos",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ef_personalInfos",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ef_personalInfos");

            migrationBuilder.AlterColumn<string>(
                name: "serviceNumber",
                table: "ef_personalInfos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ef_personalInfos",
                table: "ef_personalInfos",
                column: "serviceNumber");
        }
    }
}
