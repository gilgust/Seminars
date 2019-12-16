using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Seminars.Migrations
{
    public partial class addChapters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarParts_SeminarParts_ParentPartId",
                table: "SeminarParts");

            migrationBuilder.DropIndex(
                name: "IX_SeminarParts_ParentPartId",
                table: "SeminarParts");

            migrationBuilder.RenameColumn(
                name: "ParentPartId",
                table: "SeminarParts",
                newName: "Order");

            migrationBuilder.CreateTable(
                name: "SeminarChapter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    SeminarId = table.Column<int>(nullable: false),
                    ParentPartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeminarChapter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeminarChapter_SeminarParts_ParentPartId",
                        column: x => x.ParentPartId,
                        principalTable: "SeminarParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeminarChapter_ParentPartId",
                table: "SeminarChapter",
                column: "ParentPartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeminarChapter");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "SeminarParts",
                newName: "ParentPartId");

            migrationBuilder.CreateIndex(
                name: "IX_SeminarParts_ParentPartId",
                table: "SeminarParts",
                column: "ParentPartId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarParts_SeminarParts_ParentPartId",
                table: "SeminarParts",
                column: "ParentPartId",
                principalTable: "SeminarParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
