using Microsoft.EntityFrameworkCore.Migrations;

namespace Seminars.Migrations
{
    public partial class add_roles_to_seminar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeminarId",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_SeminarId",
                table: "AspNetRoles",
                column: "SeminarId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Seminars_SeminarId",
                table: "AspNetRoles",
                column: "SeminarId",
                principalTable: "Seminars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Seminars_SeminarId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_SeminarId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "SeminarId",
                table: "AspNetRoles");
        }
    }
}
