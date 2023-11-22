using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class UpdateGameUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "GameUserEntity",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GameUserEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "GameUserEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "GameUserEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameUserEntity",
                table: "GameUserEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GameUserEntity_DeletedAt",
                table: "GameUserEntity",
                column: "DeletedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GameUserEntity",
                table: "GameUserEntity");

            migrationBuilder.DropIndex(
                name: "IX_GameUserEntity_DeletedAt",
                table: "GameUserEntity");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GameUserEntity");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GameUserEntity");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "GameUserEntity");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "GameUserEntity");
        }
    }
}
