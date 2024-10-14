using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id_RightsId",
                table: "RightsUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image_Category",
                table: "Category",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_RightsUsers_Id_RightsId",
                table: "RightsUsers",
                column: "Id_RightsId");

            migrationBuilder.AddForeignKey(
                name: "FK_RightsUsers_Rights_Id_RightsId",
                table: "RightsUsers",
                column: "Id_RightsId",
                principalTable: "Rights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RightsUsers_Rights_Id_RightsId",
                table: "RightsUsers");

            migrationBuilder.DropIndex(
                name: "IX_RightsUsers_Id_RightsId",
                table: "RightsUsers");

            migrationBuilder.DropColumn(
                name: "Id_RightsId",
                table: "RightsUsers");

            migrationBuilder.DropColumn(
                name: "Image_Category",
                table: "Category");
        }
    }
}
