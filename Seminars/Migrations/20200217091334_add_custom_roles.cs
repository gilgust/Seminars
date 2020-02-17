using Microsoft.EntityFrameworkCore.Migrations;

namespace Seminars.Migrations
{
    public partial class add_custom_roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SeminarRole",
                columns: table => new
                {
                    SeminarId = table.Column<int>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeminarRole", x => new { x.RoleId, x.SeminarId });
                    table.ForeignKey(
                        name: "FK_SeminarRole_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeminarRole_Seminars_SeminarId",
                        column: x => x.SeminarId,
                        principalTable: "Seminars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeminarRole_SeminarId",
                table: "SeminarRole",
                column: "SeminarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeminarRole");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<int>(
                name: "SeminarId",
                table: "AspNetRoles",
                type: "int",
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
    }
}
