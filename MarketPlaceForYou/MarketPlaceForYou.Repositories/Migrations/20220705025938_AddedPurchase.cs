using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlaceForYou.Repositories.Migrations
{
    public partial class AddedPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerID",
                table: "Listings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Purchased",
                table: "Listings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerID",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Purchased",
                table: "Listings");
        }
    }
}
