using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class UpdateAttGrp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeGroupEntityGameEntity");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "AttributeGroup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AttributeGroup_GameId",
                table: "AttributeGroup",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeGroup_Game_GameId",
                table: "AttributeGroup",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeGroup_Game_GameId",
                table: "AttributeGroup");

            migrationBuilder.DropIndex(
                name: "IX_AttributeGroup_GameId",
                table: "AttributeGroup");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "AttributeGroup");

            migrationBuilder.CreateTable(
                name: "AttributeGroupEntityGameEntity",
                columns: table => new
                {
                    AttributeGroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeGroupEntityGameEntity", x => new { x.AttributeGroupsId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_AttributeGroupEntityGameEntity_AttributeGroup_AttributeGroupsId",
                        column: x => x.AttributeGroupsId,
                        principalTable: "AttributeGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeGroupEntityGameEntity_Game_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeGroupEntityGameEntity_GamesId",
                table: "AttributeGroupEntityGameEntity",
                column: "GamesId");
        }
    }
}
