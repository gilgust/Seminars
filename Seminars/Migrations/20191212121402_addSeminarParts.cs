using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Seminars.Migrations
{
    public partial class addSeminarParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeminarParts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SeminarId = table.Column<int>(nullable: false),
                    ParentPartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeminarParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeminarParts_SeminarParts_ParentPartId",
                        column: x => x.ParentPartId,
                        principalTable: "SeminarParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeminarParts_Seminars_SeminarId",
                        column: x => x.SeminarId,
                        principalTable: "Seminars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeminarParts_ParentPartId",
                table: "SeminarParts",
                column: "ParentPartId");

            migrationBuilder.CreateIndex(
                name: "IX_SeminarParts_SeminarId",
                table: "SeminarParts",
                column: "SeminarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeminarParts");
        }
    }
}
