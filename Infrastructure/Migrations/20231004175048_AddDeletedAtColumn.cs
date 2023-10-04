using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddDeletedAtColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "WalletCategory");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "User");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "LevelProgress");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "GameServer");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "CharacterType");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "CharacterAttribute");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "CharacterAsset");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "AttributeGroup");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "AssetType");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "AssetAttribute");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "ActivityType");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Activity");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "WalletCategory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Transaction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Payment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "LevelProgress",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Level",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "GameServer",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Game",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Client",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "CharacterType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "CharacterAttribute",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "CharacterAsset",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Character",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AttributeGroup",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AssetType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AssetAttribute",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Asset",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ActivityType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Activity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletCategory_DeletedAt",
                table: "WalletCategory",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_DeletedAt",
                table: "Wallet",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_User_DeletedAt",
                table: "User",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_DeletedAt",
                table: "Transaction",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_DeletedAt",
                table: "Payment",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_LevelProgress_DeletedAt",
                table: "LevelProgress",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Level_DeletedAt",
                table: "Level",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_GameServer_DeletedAt",
                table: "GameServer",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Game_DeletedAt",
                table: "Game",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Client_DeletedAt",
                table: "Client",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterType_DeletedAt",
                table: "CharacterType",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterAttribute_DeletedAt",
                table: "CharacterAttribute",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterAsset_DeletedAt",
                table: "CharacterAsset",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Character_DeletedAt",
                table: "Character",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeGroup_DeletedAt",
                table: "AttributeGroup",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AssetType_DeletedAt",
                table: "AssetType",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAttribute_DeletedAt",
                table: "AssetAttribute",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_DeletedAt",
                table: "Asset",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityType_DeletedAt",
                table: "ActivityType",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_DeletedAt",
                table: "Activity",
                column: "DeletedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WalletCategory_DeletedAt",
                table: "WalletCategory");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_DeletedAt",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_User_DeletedAt",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_DeletedAt",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Payment_DeletedAt",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_LevelProgress_DeletedAt",
                table: "LevelProgress");

            migrationBuilder.DropIndex(
                name: "IX_Level_DeletedAt",
                table: "Level");

            migrationBuilder.DropIndex(
                name: "IX_GameServer_DeletedAt",
                table: "GameServer");

            migrationBuilder.DropIndex(
                name: "IX_Game_DeletedAt",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Client_DeletedAt",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_CharacterType_DeletedAt",
                table: "CharacterType");

            migrationBuilder.DropIndex(
                name: "IX_CharacterAttribute_DeletedAt",
                table: "CharacterAttribute");

            migrationBuilder.DropIndex(
                name: "IX_CharacterAsset_DeletedAt",
                table: "CharacterAsset");

            migrationBuilder.DropIndex(
                name: "IX_Character_DeletedAt",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_AttributeGroup_DeletedAt",
                table: "AttributeGroup");

            migrationBuilder.DropIndex(
                name: "IX_AssetType_DeletedAt",
                table: "AssetType");

            migrationBuilder.DropIndex(
                name: "IX_AssetAttribute_DeletedAt",
                table: "AssetAttribute");

            migrationBuilder.DropIndex(
                name: "IX_Asset_DeletedAt",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_ActivityType_DeletedAt",
                table: "ActivityType");

            migrationBuilder.DropIndex(
                name: "IX_Activity_DeletedAt",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "WalletCategory");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "LevelProgress");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "GameServer");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CharacterType");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CharacterAttribute");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CharacterAsset");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AttributeGroup");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AssetType");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AssetAttribute");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ActivityType");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Activity");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "WalletCategory",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Wallet",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "User",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Transaction",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Payment",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "LevelProgress",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Level",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "GameServer",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Game",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Client",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "CharacterType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "CharacterAttribute",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "CharacterAsset",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Character",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "AttributeGroup",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "AssetType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "AssetAttribute",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Asset",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "ActivityType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Activity",
                type: "bit",
                nullable: true);
        }
    }
}
