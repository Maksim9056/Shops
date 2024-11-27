using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money_Account",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "Count_product",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count_product",
                table: "Orders");

            migrationBuilder.AddColumn<long>(
                name: "Money_Account",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
