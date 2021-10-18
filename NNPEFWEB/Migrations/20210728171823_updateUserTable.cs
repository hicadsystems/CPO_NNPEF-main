using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class updateUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Appointment",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Command",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rank",
                table: "User",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    Command = table.Column<string>(nullable: false),
                    Rank = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    ConfirmPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserViewModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserViewModel");

            migrationBuilder.DropColumn(
                name: "Appointment",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Command",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "User");
        }
    }
}
