using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class addcontrol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ef_control",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ship = table.Column<string>(nullable: true),
                    startdate = table.Column<DateTime>(nullable: false),
                    enddate = table.Column<DateTime>(nullable: false),
                    status = table.Column<bool>(nullable: false),
                    createdby = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_control", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ef_control");
        }
    }
}
