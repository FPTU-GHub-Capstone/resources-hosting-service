using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class AddWURU : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MonthlyRequests",
                table: "Game",
                newName: "MonthlyWriteUnits");

            migrationBuilder.AddColumn<int>(
                name: "MonthlyReadUnits",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyReadUnits",
                table: "Game");

            migrationBuilder.RenameColumn(
                name: "MonthlyWriteUnits",
                table: "Game",
                newName: "MonthlyRequests");
        }
    }
}
