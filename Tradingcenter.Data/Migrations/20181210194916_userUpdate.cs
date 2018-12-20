using Microsoft.EntityFrameworkCore.Migrations;

namespace Tradingcenter.Data.Migrations
{
    public partial class userUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VerificationKey",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerificationKey",
                table: "Users");
        }
    }
}
