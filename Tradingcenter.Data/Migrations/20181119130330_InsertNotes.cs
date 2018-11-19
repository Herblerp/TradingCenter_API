using Microsoft.EntityFrameworkCore.Migrations;

namespace Tradingcenter.Data.Migrations
{
    public partial class InsertNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Portfolios",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Portfolios");

            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PortfolioId",
                table: "Orders",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Portfolios_PortfolioId",
                table: "Orders",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "PortfolioId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
