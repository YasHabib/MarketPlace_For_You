using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlaceForYou.Repositories.Migrations
{
    public partial class AddIsDeletedForListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Listings",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Listings");
        }
    }
}
