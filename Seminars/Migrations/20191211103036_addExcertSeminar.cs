using Microsoft.EntityFrameworkCore.Migrations;

namespace Seminars.Migrations
{
    public partial class addExcertSeminar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Excerpt",
                table: "Seminars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excerpt",
                table: "Seminars");
        }
    }
}
