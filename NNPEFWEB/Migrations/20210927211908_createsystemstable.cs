using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class createsystemstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteStatus",
                table: "User");

            migrationBuilder.CreateTable(
                name: "ef_systeminfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comp_code = table.Column<string>(nullable: true),
                    comp_name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    SiteStatus = table.Column<int>(nullable: false),
                    opendate = table.Column<DateTime>(nullable: false),
                    cloasedate = table.Column<DateTime>(nullable: false),
                    hrlink = table.Column<string>(nullable: true),
                    town = table.Column<string>(nullable: true),
                    lg = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    box = table.Column<string>(nullable: true),
                    tel = table.Column<string>(nullable: true),
                    serveraddr = table.Column<string>(nullable: true),
                    serverport = table.Column<string>(nullable: true),
                    email_pword = table.Column<string>(nullable: true),
                    company_image = table.Column<byte[]>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: false),
                    createdby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_systeminfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ef_systeminfos");

            migrationBuilder.AddColumn<int>(
                name: "SiteStatus",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
