using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class addshiplogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ef_shiplogins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rank = table.Column<string>(nullable: true),
                    payClass = table.Column<string>(nullable: true),
                    surName = table.Column<string>(nullable: true),
                    otheName = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    userName = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    confirmPassword = table.Column<bool>(nullable: false),
                    dateCreated = table.Column<DateTime>(nullable: true),
                    loginDate = table.Column<DateTime>(nullable: true),
                    expireDate = table.Column<DateTime>(nullable: true),
                    Appointment = table.Column<string>(nullable: true),
                    ship = table.Column<string>(nullable: true),
                    command = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_shiplogins", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ef_shiplogins");
        }
    }
}
