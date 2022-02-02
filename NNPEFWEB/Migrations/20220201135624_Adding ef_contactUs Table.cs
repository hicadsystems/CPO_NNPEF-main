using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class Addingef_contactUsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropIndex(
            //    name: "IX_AspNetUserRoles_RoleId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropIndex(
            //    name: "IX_AspNetUserRoles_UserId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropColumn(
            //    name: "RoleId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropColumn(
            //    name: "UserId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.AddColumn<int>(
            //    name: "rankId",
            //    table: "ef_personalInfos",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ef_contactUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonName = table.Column<string>(nullable: true),
                    Ship = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Response = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_contactUs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ef_contactUs");

            //migrationBuilder.DropColumn(
            //    name: "rankId",
            //    table: "ef_personalInfos");

            //migrationBuilder.AddColumn<int>(
            //    name: "RoleId1",
            //    table: "AspNetUserRoles",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "UserId1",
            //    table: "AspNetUserRoles",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_RoleId1",
            //    table: "AspNetUserRoles",
            //    column: "RoleId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_UserId1",
            //    table: "AspNetUserRoles",
            //    column: "UserId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
            //    table: "AspNetUserRoles",
            //    column: "RoleId1",
            //    principalTable: "AspNetRoles",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
            //    table: "AspNetUserRoles",
            //    column: "UserId1",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
