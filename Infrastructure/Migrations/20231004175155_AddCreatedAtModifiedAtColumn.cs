using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddCreatedAtModifiedAtColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "WalletCategory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "WalletCategory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Transaction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Transaction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Payment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Payment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "LevelProgress",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "LevelProgress",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Level",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Level",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GameServer",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "GameServer",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Game",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Game",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Client",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Client",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CharacterType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "CharacterType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CharacterAttribute",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "CharacterAttribute",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CharacterAsset",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "CharacterAsset",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Character",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Character",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AttributeGroup",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "AttributeGroup",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AssetType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "AssetType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AssetAttribute",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "AssetAttribute",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Asset",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Asset",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ActivityType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "ActivityType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Activity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Activity",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "WalletCategory");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "WalletCategory");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "LevelProgress");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "LevelProgress");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GameServer");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "GameServer");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CharacterType");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "CharacterType");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CharacterAttribute");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "CharacterAttribute");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CharacterAsset");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "CharacterAsset");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AttributeGroup");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "AttributeGroup");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AssetType");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "AssetType");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AssetAttribute");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "AssetAttribute");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ActivityType");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "ActivityType");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Activity");
        }
    }
}
