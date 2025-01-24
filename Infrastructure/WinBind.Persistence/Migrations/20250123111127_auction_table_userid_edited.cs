using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinBind.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class auction_table_userid_edited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Auctions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Auctions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_AppUserId",
                table: "Auctions",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AspNetUsers_AppUserId",
                table: "Auctions",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AspNetUsers_AppUserId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_AppUserId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Auctions");
        }
    }
}
