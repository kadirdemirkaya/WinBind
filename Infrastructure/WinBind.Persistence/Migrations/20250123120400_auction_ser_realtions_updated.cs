using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinBind.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class auction_ser_realtions_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AspNetUsers_AppUserId",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Auctions",
                newName: "OfferAppUserId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Auctions",
                newName: "AuctionAppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_AppUserId",
                table: "Auctions",
                newName: "IX_Auctions_AuctionAppUserId");

            migrationBuilder.AddColumn<decimal>(
                name: "LastPrice",
                table: "Auctions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_OfferAppUserId",
                table: "Auctions",
                column: "OfferAppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AspNetUsers_AuctionAppUserId",
                table: "Auctions",
                column: "AuctionAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AspNetUsers_OfferAppUserId",
                table: "Auctions",
                column: "OfferAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AspNetUsers_AuctionAppUserId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AspNetUsers_OfferAppUserId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_OfferAppUserId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "LastPrice",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "OfferAppUserId",
                table: "Auctions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AuctionAppUserId",
                table: "Auctions",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_AuctionAppUserId",
                table: "Auctions",
                newName: "IX_Auctions_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AspNetUsers_AppUserId",
                table: "Auctions",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
