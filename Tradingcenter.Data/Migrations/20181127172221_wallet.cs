using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tradingcenter.Data.Migrations
{
    public partial class wallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExchangeKeyId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    WalletId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExchangeKeyId = table.Column<int>(nullable: false),
                    ExchangeTransactionId = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    TransactionType = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Walletbalance = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.WalletId);
                    table.ForeignKey(
                        name: "FK_Transactions_ExchangeKeys_ExchangeKeyId",
                        column: x => x.ExchangeKeyId,
                        principalTable: "ExchangeKeys",
                        principalColumn: "ExchangeKeyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ExchangeKeyId",
                table: "Orders",
                column: "ExchangeKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ExchangeKeyId",
                table: "Transactions",
                column: "ExchangeKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ExchangeKeys_ExchangeKeyId",
                table: "Orders",
                column: "ExchangeKeyId",
                principalTable: "ExchangeKeys",
                principalColumn: "ExchangeKeyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ExchangeKeys_ExchangeKeyId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ExchangeKeyId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ExchangeKeyId",
                table: "Orders");
        }
    }
}
