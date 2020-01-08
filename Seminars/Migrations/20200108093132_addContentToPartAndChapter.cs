using Microsoft.EntityFrameworkCore.Migrations;

namespace Seminars.Migrations
{
    public partial class addContentToPartAndChapter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "SeminarParts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "SeminarChapter",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "SeminarParts");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "SeminarChapter");
        }
    }
}
