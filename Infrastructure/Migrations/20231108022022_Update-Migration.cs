using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class UpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_User_UserId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_UserId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Game");

            migrationBuilder.AddColumn<Guid>(
                name: "CharacterId",
                table: "Activity",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "GameEntityUserEntity",
                columns: table => new
                {
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameEntityUserEntity", x => new { x.GamesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GameEntityUserEntity_Game_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameEntityUserEntity_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_CharacterId",
                table: "Activity",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_GameEntityUserEntity_UsersId",
                table: "GameEntityUserEntity",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Character_CharacterId",
                table: "Activity",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Character_CharacterId",
                table: "Activity");

            migrationBuilder.DropTable(
                name: "GameEntityUserEntity");

            migrationBuilder.DropIndex(
                name: "IX_Activity_CharacterId",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Activity");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Game",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Game_UserId",
                table: "Game",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_User_UserId",
                table: "Game",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
