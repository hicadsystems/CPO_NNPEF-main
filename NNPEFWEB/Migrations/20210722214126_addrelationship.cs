using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class addrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ef_relationships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_relationships", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ef_relationships",
                columns: new[] { "Id", "description" },
                values: new object[,]
                {
                    { 1, "Mother" },
                    { 2, "Father" },
                    { 3, "son" },
                    { 4, "Daughter" },
                    { 5, "Brother" },
                    { 6, "Sister" },
                    { 7, "Wife" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ef_relationships");
        }
    }
}
