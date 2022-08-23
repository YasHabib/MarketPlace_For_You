using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlaceForYou.Repositories.Migrations
{
    public partial class SetRelationBetweenListingAndUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ListingId",
                table: "Uploads",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Uploads_ListingId",
                table: "Uploads",
                column: "ListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Uploads_Listings_ListingId",
                table: "Uploads",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uploads_Listings_ListingId",
                table: "Uploads");

            migrationBuilder.DropIndex(
                name: "IX_Uploads_ListingId",
                table: "Uploads");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "Uploads");
        }
    }
}
