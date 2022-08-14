using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlaceForYou.Repositories.Migrations
{
    public partial class RemovedTotalPurchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveListings",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Purchases",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalPurchase",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalSold",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActiveListings",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Purchases",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPurchase",
                table: "Users",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSold",
                table: "Users",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
