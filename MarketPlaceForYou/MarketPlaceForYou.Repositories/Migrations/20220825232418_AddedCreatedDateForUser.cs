using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlaceForYou.Repositories.Migrations
{
    public partial class AddedCreatedDateForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SearchInputs_Users_UserId",
                table: "SearchInputs");

            migrationBuilder.DropIndex(
                name: "IX_SearchInputs_UserId",
                table: "SearchInputs");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Listings_BuyerID",
                table: "Listings",
                column: "BuyerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Users_BuyerID",
                table: "Listings",
                column: "BuyerID",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Users_BuyerID",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_BuyerID",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_SearchInputs_UserId",
                table: "SearchInputs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SearchInputs_Users_UserId",
                table: "SearchInputs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
