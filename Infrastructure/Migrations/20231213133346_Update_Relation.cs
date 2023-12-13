using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class Update_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LevelProgress_LevelId",
                table: "LevelProgress");

            migrationBuilder.CreateIndex(
                name: "IX_LevelProgress_LevelId",
                table: "LevelProgress",
                column: "LevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LevelProgress_LevelId",
                table: "LevelProgress");

            migrationBuilder.CreateIndex(
                name: "IX_LevelProgress_LevelId",
                table: "LevelProgress",
                column: "LevelId",
                unique: true);
        }
    }
}
