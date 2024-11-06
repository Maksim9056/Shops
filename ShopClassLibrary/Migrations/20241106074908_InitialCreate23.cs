using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Money_Account",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "Idproduct",
                table: "Orders",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money_Account",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "Idproduct",
                table: "Orders",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
