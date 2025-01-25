using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinBind.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class auction_enum_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Auctions");

            migrationBuilder.AddColumn<int>(
                name: "AuctionStatus",
                table: "Auctions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuctionStatus",
                table: "Auctions");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Auctions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
