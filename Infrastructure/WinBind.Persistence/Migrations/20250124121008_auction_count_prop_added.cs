using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinBind.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class auction_count_prop_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Auctions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Auctions");
        }
    }
}
