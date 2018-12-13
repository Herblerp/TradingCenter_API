using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tradingcenter.Data.Migrations
{
    public partial class soldPerMonth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PurchasedOn",
                table: "PurchasedPortfolios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasedOn",
                table: "PurchasedPortfolios");
        }
    }
}
