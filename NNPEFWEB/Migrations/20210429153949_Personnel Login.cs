using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class PersonnelLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ef_PersonnelLogins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    svcNo = table.Column<string>(nullable: true),
                    rank = table.Column<string>(nullable: true),
                    payClass = table.Column<string>(nullable: true),
                    surName = table.Column<string>(nullable: true),
                    otheName = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    userName = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    confirmPassword = table.Column<bool>(nullable: false),
                    dateCreated = table.Column<DateTime>(nullable: false),
                    loginDate = table.Column<DateTime>(nullable: false),
                    expireDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_PersonnelLogins", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ef_PersonnelLogins");
        }
    }
}
