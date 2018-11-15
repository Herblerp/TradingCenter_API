using Microsoft.EntityFrameworkCore.Migrations;

namespace Tradingcenter.Data.Migrations
{
    public partial class updateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Portfolios_PortfolioId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "PortfolioId",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "OrderPortolios",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    PortfolioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPortolios", x => new { x.OrderId, x.PortfolioId });
                    table.ForeignKey(
                        name: "FK_OrderPortolios_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPortolios_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "PortfolioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPortolios_PortfolioId",
                table: "OrderPortolios",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Portfolios_PortfolioId",
                table: "Orders",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "PortfolioId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Portfolios_PortfolioId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderPortolios");

            migrationBuilder.AlterColumn<int>(
                name: "PortfolioId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Portfolios_PortfolioId",
                table: "Orders",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "PortfolioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
