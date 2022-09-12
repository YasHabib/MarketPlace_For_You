using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlaceForYou.Repositories.Migrations
{
    public partial class AddedListingSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SearchInputs_Users_UserId",
                table: "SearchInputs");

            migrationBuilder.DropIndex(
                name: "IX_SearchInputs_UserId",
                table: "SearchInputs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Listings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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
                name: "IsDeleted",
                table: "Listings");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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
