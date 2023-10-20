using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class UpdateWalletAndPaymentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Payment_PaymentId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_PaymentId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Wallet");

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Payment_WalletId",
                table: "Payment",
                column: "WalletId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Wallet_WalletId",
                table: "Payment",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Wallet_WalletId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_WalletId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Payment");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "Wallet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_PaymentId",
                table: "Wallet",
                column: "PaymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_Payment_PaymentId",
                table: "Wallet",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id");
        }
    }
}
