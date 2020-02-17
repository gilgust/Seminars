using Microsoft.EntityFrameworkCore.Migrations;

namespace Seminars.Migrations
{
    public partial class add_seminar_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarRole_AspNetRoles_RoleId",
                table: "SeminarRole");

            migrationBuilder.DropForeignKey(
                name: "FK_SeminarRole_Seminars_SeminarId",
                table: "SeminarRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeminarRole",
                table: "SeminarRole");

            migrationBuilder.RenameTable(
                name: "SeminarRole",
                newName: "SeminarRoles");

            migrationBuilder.RenameIndex(
                name: "IX_SeminarRole_SeminarId",
                table: "SeminarRoles",
                newName: "IX_SeminarRoles_SeminarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeminarRoles",
                table: "SeminarRoles",
                columns: new[] { "RoleId", "SeminarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarRoles_AspNetRoles_RoleId",
                table: "SeminarRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarRoles_Seminars_SeminarId",
                table: "SeminarRoles",
                column: "SeminarId",
                principalTable: "Seminars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarRoles_AspNetRoles_RoleId",
                table: "SeminarRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_SeminarRoles_Seminars_SeminarId",
                table: "SeminarRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeminarRoles",
                table: "SeminarRoles");

            migrationBuilder.RenameTable(
                name: "SeminarRoles",
                newName: "SeminarRole");

            migrationBuilder.RenameIndex(
                name: "IX_SeminarRoles_SeminarId",
                table: "SeminarRole",
                newName: "IX_SeminarRole_SeminarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeminarRole",
                table: "SeminarRole",
                columns: new[] { "RoleId", "SeminarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarRole_AspNetRoles_RoleId",
                table: "SeminarRole",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarRole_Seminars_SeminarId",
                table: "SeminarRole",
                column: "SeminarId",
                principalTable: "Seminars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
